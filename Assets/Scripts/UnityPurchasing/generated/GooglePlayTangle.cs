// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ePv1+sp4+/D4ePv7+nc7KUKq8EA6MmEXQrnnjE7nEY4jCK3PVC/jRd0M9HX9ZOAOpnWGddDVq9zE0wFXl1mIREfYTND+4EWC5/UwlRGD+USBHyLdu60eCf3n0fN/PrLwtdPLE+bkle1xbvmcM12Hs/TkCyxyzh5xESDMb/bg2MP6GJeY6wmP3TFwZbn2jdGQ+ZCHBIFMIKD0K13Rtb2WvdXI+wx5Syjr5M+ZFXDkUnwzK0i2k5DxyactMHYHuEZhtYs36uUTBjfJETj6gkuEYi/X4j/WX7pdoHEn/7ayc5TKPC0DeYZ8Ra/7kNU4KDZeynj72Mr3/PPQfLJ8Dff7+/v/+vlwHtPkUFtyp78H/upyn0o1mJvGkh2blemosZxSmfj5+/r7");
        private static int[] order = new int[] { 1,13,3,6,7,6,7,11,9,11,11,12,13,13,14 };
        private static int key = 250;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
