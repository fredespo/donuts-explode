
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace AlmostEngine
{
    public static class SymbolsUtils
    {

        public static void ToggleDefineSymbol(string symbol, bool enable)
        {
            ToggleDefineSymbol(GetCurrentTargetGroup(), symbol, enable);
        }

        public static bool IsSymbolDefined(BuildTargetGroup targetGroup, string symbol)
        {
            var definesList = GetDefines(targetGroup);
            return definesList.Contains(symbol);
        }

        public static bool IsSymbolDefined(string symbol)
        {
            return IsSymbolDefined(GetCurrentTargetGroup(), symbol);
        }

        public static BuildTargetGroup GetCurrentTargetGroup()
        {
            return BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
        }

        public static void ToggleDefineSymbol(BuildTargetGroup targetGroup, string symbol, bool enable)
        {
            if (enable)
            {
                AddDefine(targetGroup, symbol);
            }
            else
            {
                RemoveDefine(targetGroup, symbol);
            }
            Debug.Log(targetGroup.ToString() + " define symbols: " + UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup));
        }


        /// <summary>
        /// Add a custom define
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="buildTargetGroup"></param>
        public static void AddDefine(BuildTargetGroup target, string symbol)
        {
            var definesList = GetDefines(target);
            if (!definesList.Contains(symbol))
            {
                definesList.Add(symbol);
                SetDefines(target, definesList);
            }
        }

        /// <summary>
        /// Remove a custom define
        /// </summary>
        /// <param name="_define"></param>
        /// <param name="_buildTargetGroup"></param>
        public static void RemoveDefine(BuildTargetGroup target, string symbol)
        {
            var definesList = GetDefines(target);
            if (definesList.Contains(symbol))
            {
                definesList.Remove(symbol);
                SetDefines(target, definesList);
            }
        }

        public static List<string> GetDefines(BuildTargetGroup buildTargetGroup)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            return defines.Split(';').ToList();
        }

        public static void SetDefines(BuildTargetGroup buildTargetGroup, List<string> definesList)
        {
            var defines = string.Join(";", definesList.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
        }
    }
}