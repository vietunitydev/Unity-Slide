// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("BrQ3FAY7MD8csH6wwTs3NzczNjXyn9zrK1T/WcXCC52sOilX95t24E4lwrPzuO2SNXdaWH66K0ttXVgkbdQGNvMhaumVv2WC2Mr3tHv5cru8nTuQW9amvXKFzb4Z3fagTMUExYQf2w6ZBTmB6IjgOz6gNuTP7rZXtQryUkAaMdYOfZzL7fbw6eCFNBVhqmbCTdU9hzG57TXoX53YnyBgLaSX9FzXcjT4my3hN6v+zmyPcKuXtDc5Nga0Nzw0tDc3NpzKMgB321pT9x1I4KFPwcIv9xdjUmCxxzplQSt5Hy9j7i4MjSXrV3q6SU1XW7QUYC4pgCVtINxlrql7qaQQQLTJbKwoJ7i5S8ij3Zic9V8AHDhPgcGW8jYU+KzXKp/QyzQ1NzY3");
        private static int[] order = new int[] { 0,4,3,13,8,6,11,10,12,12,11,11,12,13,14 };
        private static int key = 54;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
