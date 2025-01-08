// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("s45PUm+DJHnz4ASLOtQ0Bp485vxluyOKUQoQnIBHzMXlF02aDwCTdHm1N2H2PpuO1Tb/zasEQCIlqSfbHz60f+kcKdROb53ewA/oHcH0YfVkWDkEMHRrpXaf7/0h3hQnemBvzf8qZ22fn6tcZ+DWi3yQx+sbzmqzJMtWcHb0invEFxr4bh7Y/TDW3yB7+Pb5yXv48/t7+Pj5a4rUUNITh8l7+NvJ9P/w03+xfw70+Pj4/Pn6pt7lBqTmqvqw+0k12oKpA5ne0MReQDMqRnFeN8rIRM78nubFL7hNmz+wY/418WKd+QsMiQWDPSVx7gKkRwv64huV2D2eJlWH9TaYI6DO0r6H7s1jBgjuIoiQv3iCVXdEqhZJ5fP1+d2aCoYvjvv6+Pn4");
        private static int[] order = new int[] { 4,7,10,9,6,12,13,7,13,11,10,12,12,13,14 };
        private static int key = 249;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
