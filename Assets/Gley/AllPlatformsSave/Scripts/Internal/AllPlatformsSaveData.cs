namespace Gley.AllPlatformsSave.Internal
{
    using System.Collections.Generic;
    using UnityEngine;

    public class AllPlatformsSaveData : ScriptableObject
    {
        public List<SupportedSaveMethods> saveMethod = new List<SupportedSaveMethods>();
        public List<SupportedBuildTargetGroup> buildTargetGroup = new List<SupportedBuildTargetGroup>();
        public List<JsonSerializationMethods> jsonSerializationMethods = new List<JsonSerializationMethods>();
    }
}
