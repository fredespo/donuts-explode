using System.Collections.Generic;
using UnityEngine;

namespace Gley.EasyIAP.Internal
{
    /// <summary>
    /// Used to save user settings made from Settings Window
    /// </summary>
    public class EasyIAPData : ScriptableObject
    {
        public bool debug;
        public bool useReceiptValidation;
        public bool usePlaymaker;
        public bool useUVS;
        public bool useForGooglePlay;
        public bool useForAmazon;
        public bool useForIos;
        public bool useForMac;
        public bool useForWindows;
        public List<StoreProduct> shopProducts = new List<StoreProduct>();
    }
}
