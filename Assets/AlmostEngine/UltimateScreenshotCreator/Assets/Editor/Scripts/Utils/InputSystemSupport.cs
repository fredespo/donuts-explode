using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


namespace AlmostEngine.Screenshot
{
    public static class InputSystemSupport
    {
        public static void ToggleInputSystemSupport(bool enable = true)
        {
            // Assembly refs
            var almostEngineAssemblies = AssemblyUtils.FindAllAsmdefs("AlmostEngine");
            var inputSystemAssemblies = AssemblyUtils.FindAllAsmdefs("InputSystem");
            var inputSystemAssemblyName = inputSystemAssemblies.Count > 0 ? inputSystemAssemblies[0] : "";
            foreach (var almostAssemblyPath in almostEngineAssemblies)
            {
                AssemblyUtils.ToggleAssemblyReference(almostAssemblyPath, inputSystemAssemblyName, enable);
            }
            // Define symbol
            if (enable && !AssemblyUtils.HasReference("AlmostEngine", "InputSystem"))
            {
                Debug.Log("Impossible to add InputSystem assembly reference to InputSystem to AlmostEngine.");
            }
            SymbolsUtils.ToggleDefineSymbol("USC_INPUT_SYSTEM", enable && AssemblyUtils.HasReference("AlmostEngine", "InputSystem"));
        }

    }
}