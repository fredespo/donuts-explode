using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


namespace AlmostEngine.Screenshot
{
    public static class HDRPSupport
    {
        public static void ToggleHDRPSupport(bool enable = true)
        {
            // Assembly refs
            var assemblyName = "AlmostEngine";
            var assemblyRefName = "Unity.RenderPipelines.HighDefinition.Runtime";
            var almostEngineAssemblyPath = AssemblyUtils.FindAsmdef(assemblyName);
            var hdrpAssemblyPath = AssemblyUtils.FindAsmdef(assemblyRefName);
            if (almostEngineAssemblyPath != "" && hdrpAssemblyPath != "")
            {
                AssemblyUtils.ToggleAssemblyReference(almostEngineAssemblyPath, hdrpAssemblyPath, enable);
            }
            // Define symbol
            if (enable && !AssemblyUtils.HasReference(assemblyName, assemblyRefName))
            {
                Debug.Log("Impossible to add " + assemblyRefName + " assembly reference to " + assemblyName);
            }
            SymbolsUtils.ToggleDefineSymbol("USC_HDRP", enable && AssemblyUtils.HasReference(assemblyName, assemblyRefName));
        }

    }
}