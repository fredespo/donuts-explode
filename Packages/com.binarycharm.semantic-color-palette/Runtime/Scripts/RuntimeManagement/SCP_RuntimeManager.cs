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
using System.Linq;

using BinaryCharm.Common.StateManagement;
using BinaryCharm.SemanticColorPalette.Colorers;

namespace BinaryCharm.SemanticColorPalette.RuntimeManagement {

    /// <summary>
    /// Static class keeping track of all the providers/colorers.
    /// Useful to easily enable/disable the system at runtime.
    /// </summary>
    public static class SCP_RuntimeManager {

        #region State Tracking ------------------------------------------------

        /// <summary>
        /// State tracking struct. Keeps track of the last change on a 
        /// provider.
        /// </summary>
        private static MutableStateTracker s_stateTracker;

        /// <summary>
        /// Returns a reference to the state tracking struct.
        /// </summary>
        /// 
        /// <remarks>
        /// Should never be used in everyday application code.
        /// </remarks>
        /// 
        /// <returns>A reference to the state tracking struct.</returns>
        public static ref MutableStateTracker GetStateTracker() {
            return ref s_stateTracker;
        }

        #endregion ------------------------------------------------------------


        #region Providers -----------------------------------------------------

        private static HashSet<SCP_PaletteProvider> s_rProviders = new HashSet<SCP_PaletteProvider>();
        private static HashSet<SCP_PaletteProvider> s_rEnabledProviders = new HashSet<SCP_PaletteProvider>();
        public static event Action ProvidersChanged;

        /// <summary>
        /// Returns an array of all the currently enabled providers.
        /// </summary>
        /// <returns>An array of the currently enabled providers.</returns>
        public static SCP_PaletteProvider[] GetEnabledProviders() {
            return s_rEnabledProviders.ToArray();
        }

        internal static void enableProvider(SCP_PaletteProvider rProvider) {
            if (IsSystemRunning()) s_rEnabledProviders.Add(rProvider);
            onProvidersChanged();
        }

        internal static void disableProvider(SCP_PaletteProvider rProvider) {
            s_rEnabledProviders.Remove(rProvider);
            onProvidersChanged();
        }

        internal static void addProvider(SCP_PaletteProvider rProvider) {
            s_rProviders.Add(rProvider);
            rProvider.enabled = s_bSystemRunning;
            onProvidersChanged();
        }

        internal static void remProvider(SCP_PaletteProvider rProvider) {
            s_rProviders.Remove(rProvider);
            onProvidersChanged();
        }

        private static void onProvidersChanged() {
            s_stateTracker.SetChanged();
            if (ProvidersChanged != null) ProvidersChanged();
        }

        private static void setProvidersEnabled(bool bEnabled) {
            foreach (SCP_PaletteProvider rItem in s_rProviders) {
                rItem.enabled = bEnabled;
            }
        }

        #endregion ------------------------------------------------------------


        #region Colorers ------------------------------------------------------

        private static HashSet<SCP_AColorerBase> s_rColorers = new HashSet<SCP_AColorerBase>();

        internal static void addColorer(SCP_AColorerBase rColorer) {
            s_rColorers.Add(rColorer);
            rColorer.enabled = s_bSystemRunning;
        }

        internal static void remColorer(SCP_AColorerBase rColorer) {
            s_rColorers.Remove(rColorer);
        }

        private static void setColorersEnabled(bool bEnabled) {
            foreach (SCP_AColorerBase rItem in s_rColorers) {
                rItem.enabled = bEnabled;
            }
        }

        #endregion ------------------------------------------------------------


        #region Subsystem Management ------------------------------------------

        private static bool s_bSystemRunning = true;

        /// <summary>
        /// Returns the state of
        /// </summary>
        /// <returns>true if the system is enabled, false if not</returns>
        public static bool IsSystemRunning() {
            return s_bSystemRunning;
        }

        /// <summary>
        /// Enables or disables the `Semantic Color Palette` system, which 
        /// basically means setting all the `Colorers` and all the `Providers`
        /// behaviours enabled or not.
        /// </summary>
        /// <param name="bVal">true for enabling, false for disabling</param>
        public static void SetSystemRunning(bool bVal) {
            s_bSystemRunning = bVal;
            setColorersEnabled(bVal);
            setProvidersEnabled(bVal);
        }

        #endregion ------------------------------------------------------------

    }

}
