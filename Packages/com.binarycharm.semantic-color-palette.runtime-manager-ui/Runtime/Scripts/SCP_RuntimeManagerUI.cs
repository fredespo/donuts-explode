/*
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             Copyright (C) 2022 Binary Charm - All Rights Reserved
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@                        @@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@                           @@@@@@@@@@@@@@@@@@
             @@@@@@@@@   @@@@@@@@@@@  @@@@@        @@@@@@@@@@@@@@@
             @@@@@@@@@@@  @@@@@@@@@  @@@@@@@@@@       (@@@@@@@@@@@
             @@@@@@@@@@@@  @@@@@@@@& @@@@@@@@@@ @@@@     @@@@@@@@@
             @@@@@@@@@@@@@ @@@@@@@@@@ *@@@@@@@ @@@@@@@@@*   @@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*/

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using HSVPicker;
using SimpleFileBrowser;

using BinaryCharm.Common.Extensions;
using BinaryCharm.Common.StateManagement;
using BinaryCharm.SemanticColorPalette.Utils;
using BinaryCharm.UI;

namespace BinaryCharm.SemanticColorPalette.RuntimeManagement {

    public class SCP_RuntimeManagerUI : MonoBehaviour {

        [SerializeField] private TMP_Dropdown m_rPaletteProviderDD;
        [SerializeField] private TMP_Dropdown m_rPaletteDD;
        [SerializeField] private Button m_rSetActiveBtn;
        [SerializeField] private Button m_rImportBtn;
        [SerializeField] private Button m_rExportBtn;
        [SerializeField] private TMP_Text m_rWebBuildNotice;
        [SerializeField] private GameObject m_rPaletteElemPrefab;
        [SerializeField] private RectTransform m_rPaletteElemsScrollViewContent;
        [SerializeField] private ColorPicker m_rColorPicker;
        [SerializeField] private GameObject m_rWaitingPanel;
        [SerializeField] private GameObject m_rOutputPanel;
        [SerializeField] private Button m_rOutputPanelOkBtn;
        [SerializeField] private TMP_Text m_rOutputPanelText;


        private MinimizableWindowBhv m_rWin;
        private List<SCP_RuntimeManagerUI_PaletteElem> m_rElems = new List<SCP_RuntimeManagerUI_PaletteElem>();

        private UiState m_rUiState = new UiState();
        private MutableStateTracker m_stateTracker;

        private void Awake() {
            m_rWin = GetComponent<MinimizableWindowBhv>();
            m_rWin.setWinTitle("Semantic Color Palette Runtime Manager");

            m_rWebBuildNotice.gameObject.SetActive(
                Application.platform == RuntimePlatform.WebGLPlayer
            );

            m_rPaletteProviderDD.onValueChanged.AddListener(m_rUiState.setProvider);
            m_rPaletteDD.onValueChanged.AddListener(m_rUiState.setPalette);
            m_rSetActiveBtn.onClick.AddListener(m_rUiState.activateSelectedPalette);
            m_rExportBtn.onClick.AddListener(m_rUiState.exportSelectedPalette);
            m_rImportBtn.onClick.AddListener(m_rUiState.importOverSelectedPalette);
            m_rOutputPanelOkBtn.onClick.AddListener(m_rUiState.setOutputHidden);

            m_rWaitingPanel.SetActive(false);
        }

        private void Update() {
            //if (!isShown()) return; // TODO handle problems

            if (m_stateTracker.SetChangedIfDependencyChanged(m_rUiState.GetStateTracker())) {
                redrawUi(m_rUiState);
            };

            m_rUiState.update(m_rPaletteElemsScrollViewContent.anchoredPosition);
        }

        public bool IsShown() {
            return m_rWin.isWinVisible();
        }

        public void Show() {
            m_rWin.setWinState(MinimizableWindowBhv.eWinState.maximized);
            m_rWin.setWinVisible(true);
        }

        private void redrawUi(UiState s) {
            setupDropdown(m_rPaletteProviderDD, s.m_rProviderNames, s.m_iSelectedProviderIndex);
            setupDropdown(m_rPaletteDD, s.m_rPaletteNames, s.m_iSelectedPaletteIndex);

            m_rPaletteProviderDD.interactable = s.m_rProviderNames.Count > 1;
            m_rPaletteDD.interactable = s.m_rPaletteNames.Count > 1;

            bool bIsWebBuild = Application.platform == RuntimePlatform.WebGLPlayer;
            bool bIsPaletteAvailable = s.m_rSelectedPalette != null;
            m_rImportBtn.interactable = !bIsWebBuild && bIsPaletteAvailable;
            m_rExportBtn.interactable = !bIsWebBuild && bIsPaletteAvailable;

            m_rSetActiveBtn.interactable = bIsPaletteAvailable &&
                s.m_iSelectedPaletteIndex != s.m_rSelectedProvider.GetActivePaletteIndex();

            setupPaletteElems(s.m_rSelectedProvider, s.m_iSelectedPaletteIndex, 
                s.m_iColorIndex, s.m_vColorElemsScroll);

            setupPicker(s.m_selectedColor, s.m_iColorIndex);

            m_rWaitingPanel.SetActive(s.m_bSelectingPath);
            m_rOutputPanelText.text = s.m_sOutput;
            m_rOutputPanel.SetActive(s.m_bShowingOutput);
        }

        private void setupDropdown(TMP_Dropdown rDD, List<string> rOpts, int iSelected) {
            rDD.ClearOptions();
            rDD.AddOptions(rOpts);
            rDD.SetValueWithoutNotify(iSelected);
        }

        private void setupPaletteElems(SCP_PaletteProvider rProvider, 
                int iPaletteIndex,
                int? iSelectedColorIndex,
                Vector2? vAnchoredPos) {

            // cache to reuse palette elems
            Queue<GameObject> rCache = new Queue<GameObject>(m_rElems.Count);

            // clear palette elems (but cache them)
            foreach (SCP_RuntimeManagerUI_PaletteElem rElem in m_rElems) {
                rElem.OnSelected -= m_rUiState.setColor;
                rCache.Enqueue(rElem.gameObject);
            }
            m_rElems.Clear();
            m_rPaletteElemsScrollViewContent.sizeDelta = m_rPaletteElemsScrollViewContent.sizeDelta.withY(0f);

            // build
            SCP_Palette rPalette = rProvider == null ? null : rProvider.GetPaletteByIndex(iPaletteIndex);

            int iNumColors = rPalette == null ? 0 : rPalette.GetNumElems();
            for (int i = 0; i < iNumColors; ++i) {
                
                // spawn a palette elem (or get it from cache, if possible)
                GameObject rGO = rCache.Count > 0 ? rCache.Dequeue() : Instantiate(m_rPaletteElemPrefab);
                rGO.transform.SetParent(m_rPaletteElemsScrollViewContent, false);

                SCP_RuntimeManagerUI_PaletteElem rPPC = rGO.GetComponent<SCP_RuntimeManagerUI_PaletteElem>();
                rPPC.setup(i, rProvider, iPaletteIndex, 
                    iSelectedColorIndex.HasValue && iSelectedColorIndex.Value == i);

                m_rPaletteElemsScrollViewContent.sizeDelta += Vector2.zero.withY(rPPC.getHeight());

                rPPC.OnSelected += m_rUiState.setColor;

                m_rElems.Add(rPPC);
            }
            m_rPaletteElemsScrollViewContent.anchoredPosition = 
                vAnchoredPos.HasValue ? vAnchoredPos.Value : Vector2.zero;

            // destroy unused cached palette elements
            while (rCache.Count > 0) {
                GameObject rGO = rCache.Dequeue();
                Destroy(rGO);
            }
        }

        private void setupPicker(Color selectedColor, int? iSelectedColorIndex) {
            if (iSelectedColorIndex.HasValue) {
                m_rColorPicker.onValueChanged.RemoveListener(m_rUiState.adjustSelectedColor);
                m_rColorPicker.gameObject.SetActive(true);
                m_rColorPicker.CurrentColor = selectedColor;
                m_rColorPicker.onValueChanged.AddListener(m_rUiState.adjustSelectedColor);
            }
            else {
                m_rColorPicker.gameObject.SetActive(false);
            }
        }

        //private void OnEnable() {
        //    SCP_RuntimeManager.OnProvidersChanged += refresh;
        //}

        //private void OnDisable() {
        //    SCP_RuntimeManager.OnProvidersChanged -= refresh;
        //}

        private class UiState {
            public SCP_PaletteProvider[] m_rProviders;
            public SCP_PaletteProvider m_rSelectedProvider;
            public SCP_Palette m_rSelectedPalette;
            public Color m_selectedColor;

            public List<string> m_rProviderNames;
            public int m_iSelectedProviderIndex;

            public List<string> m_rPaletteNames;
            public int m_iSelectedPaletteIndex;

            public int? m_iColorIndex;
            public Vector2 m_vColorElemsScroll;


            public bool m_bSelectingPath;
            public bool m_bShowingOutput;
            public string m_sOutput;


            private MutableStateTracker m_stateTracker;

            public ref MutableStateTracker GetStateTracker() {
                return ref m_stateTracker;
            }

            public UiState() {

                m_rProviders = null;
                m_rSelectedProvider = null;
                m_rSelectedPalette = null;

                m_rProviderNames = new List<string>();
                m_rPaletteNames = new List<string>();

                m_iSelectedProviderIndex = 0;
                m_iSelectedPaletteIndex = 0;

                m_iColorIndex = null;
                m_vColorElemsScroll = Vector2.zero;

                m_bSelectingPath = false;
                m_bShowingOutput = false;
                m_sOutput = "";

                refresh();
            }

            private void refresh() {
                m_stateTracker.SetChanged();

                m_rProviderNames.Clear();
                m_rPaletteNames.Clear();

                // no providers, clear state and return
                if (m_rProviders == null || m_rProviders.Length == 0) {
                    m_rSelectedProvider = null;
                    m_rSelectedPalette = null;
                    m_iColorIndex = null;
                    m_iSelectedProviderIndex = 0;
                    m_iSelectedPaletteIndex = 0;
                    return;
                }

                // at least one provider available, but no selection
                // select default and re-enter refresh()
                if (m_rSelectedProvider == null) {
                    setProvider(0);
                    return;
                }

                // set data for the provider selection combo
                int iSelectedProvider = -1;
                for (int i = 0; i < m_rProviders.Length; ++i) {
                    if (m_rProviders[i] == m_rSelectedProvider) {
                        iSelectedProvider = i;
                    }
                    m_rProviderNames.Add(m_rProviders[i].gameObject.name);
                }
                m_iSelectedProviderIndex = iSelectedProvider == -1 ?
                    0 : // select first by default
                    iSelectedProvider
                ;
                SCP_PaletteProvider rNewProvider = m_rProviders[m_iSelectedProviderIndex];
                if (rNewProvider != m_rSelectedProvider) { // provider changed, clear palette selection
                    m_rSelectedProvider = rNewProvider;
                    m_rSelectedPalette = null;
                }

                // set data for the palette selection combo, taking care of selection
                int iNumPalettes = m_rSelectedProvider.GetNumPalettes();

                int iSelectedPaletteIndex = m_rSelectedProvider.GetActivePaletteIndex();
                for (int i = 0; i < iNumPalettes; ++i) {
                    SCP_Palette rPalette = m_rSelectedProvider.GetPaletteByIndex(i);
                    if (rPalette == m_rSelectedPalette) {
                        iSelectedPaletteIndex = i;
                    }
                    m_rPaletteNames.Add(rPalette == null ? "" : rPalette.GetName());
                }
                m_iSelectedPaletteIndex = iSelectedPaletteIndex == -1 ?
                     m_rSelectedProvider.GetActivePaletteIndex() : // select active by default
                     iSelectedPaletteIndex
                ;

                SCP_Palette rNewPalette = m_rSelectedProvider.GetPaletteByIndex(m_iSelectedPaletteIndex);
                if (rNewPalette != m_rSelectedPalette) { // palette changed, clear color selection
                    m_rSelectedPalette = rNewPalette;
                    m_iColorIndex = null;
                    m_vColorElemsScroll = Vector2.zero;
                }

                if (m_iColorIndex.HasValue) {
                    SCP_ColorId colorId = m_rSelectedPalette.GetColorIdByIndex(m_iColorIndex.Value);
                    m_selectedColor = m_rSelectedPalette.GetColor(colorId);
                }

            }

            public void setProvider(int iProviderId) {
                m_rSelectedProvider = m_rProviders[iProviderId];
                refresh();
            }

            public void setPalette(int iPaletteId) {
                m_rSelectedPalette = m_rSelectedProvider.GetPaletteByIndex(iPaletteId);
                refresh();
            }

            public void setColor(int iColorIndex) {
                m_iColorIndex = iColorIndex;
                refresh();
            }

            public void activateSelectedPalette() {
                m_rSelectedProvider.SetActivePaletteIndex(m_iSelectedPaletteIndex);
            }

            private void setWaiting(bool bWaiting) {
                m_bSelectingPath = bWaiting;
                refresh();
            }

            private void setOutputShown(string sOutput) {
                m_bShowingOutput = true;
                m_sOutput = sOutput;
                refresh();
            }

            public void setOutputHidden() {
                m_bShowingOutput = false;
                refresh();
            }

            public void exportSelectedPalette() {
                Debug.Log("exportSelectedPalette()");
                string sDefaultFilename = m_rSelectedPalette.GetName() + ".json";
                setWaiting(true);
                FileBrowser.ShowSaveDialog(
                    (string[] rPaths) => {
                        if (rPaths.Length != 1) return;
                        
                        string sSavePath = rPaths[0];
                        Debug.Log("save to " + sSavePath);
                        try {
                            SCP_Utils.SavePalette(m_rSelectedPalette, sSavePath);
                            setOutputShown("Palette export to " + sSavePath + " succesful.");
                        } catch (Exception rEx) {
                            setOutputShown(
                                "Palette export to " + sSavePath
                                + " failed: " + rEx.Message);
                        } finally {
                            setWaiting(false);
                        }
                    },
                    () => {
                        setWaiting(false);
                    },
                    FileBrowser.PickMode.Files,
                    false,
                    null,
                    sDefaultFilename
                );
            }

            public void importOverSelectedPalette() {
                Debug.Log("importOverSelectedPalette()");
                setWaiting(true);
                FileBrowser.ShowLoadDialog(
                    (string[] rPaths) => {
                        if (rPaths.Length != 1) return;

                        string sSavePath = rPaths[0];
                        try {
                            SCP_Palette rPalette = SCP_Utils.LoadPalette(sSavePath);
                            Dictionary<string, Color> rColorDefs = SCP_Utils.ExtractColorDefs(rPalette);
                            SCP_Utils.UpdateColorDefs(m_rSelectedPalette, rColorDefs);
                            //m_rSelectedPalette.updateData(rPalette.getData());
                            //setOutputShown("Import succesful");
                        }
                        catch (Exception rEx) {
                            setOutputShown("Import failed: " + rEx.Message);
                        }
                        finally {
                            setWaiting(false);
                        }
                    },
                    () => {
                        setWaiting(false);
                    },
                    FileBrowser.PickMode.Files,
                    false
                );
            }

            public void adjustSelectedColor(Color c) {
                if (m_iColorIndex.HasValue) {
                    SCP_ColorId id = m_rSelectedPalette.GetColorIdByIndex(m_iColorIndex.Value);
                    m_rSelectedPalette.SetColor(id, c);
                }
            }

            public void update(Vector2 vScroll) {
                m_vColorElemsScroll = vScroll;

                bool bMustRefresh = false;

                if (m_stateTracker.SetChangedIfDependencyChanged(SCP_RuntimeManager.GetStateTracker())) {
                    m_rProviders = SCP_RuntimeManager.GetEnabledProviders();
                    bMustRefresh = true;
                }

                if (m_rProviders != null) {
                    foreach (SCP_PaletteProvider rPP in m_rProviders) {
                        if (m_stateTracker.SetChangedIfDependencyChanged(rPP.GetStateTracker())) {
                            bMustRefresh = true;
                            break;
                        }
                    }
                }

                if (bMustRefresh) {
                    refresh();
                }
            }
        }
    }

}
