// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("NLe5toY0t7y0NLe3thxKsoD3W9o1inLSwJqxVo79HEttdnBpYAW0lainODnLSCNdGBx134CcuM8BQRZyq/mfr+NurowNpWvX+jrJzdfbNJQ8HbsQ21YmPfIFTT6ZXXYgzEWERSQXdNxX8rR4G61htyt+TuwP8CsX7VSGtnOh6mkVP+UCWEp3NPt58juGNLeUhruwv5ww/jBBu7e3t7O2tdN3nchgIc9BQq93l+PS4DFHuuXB4K6pAKXtoFzlLin7KSSQwDRJ7CzhKuZCzVW9B7E5bbVo3x1YH6DgrXIfXGur1H/ZRUKLHSy6qdd3G/ZgzqVCM3M4bRK199rY/jqry+3d2KQEn1uOGYW5AWgIYLu+ILZkT24217aUeCxXqh9QS7S1t7a3");
        private static int[] order = new int[] { 1,11,2,7,8,12,13,11,12,9,10,12,12,13,14 };
        private static int key = 182;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
