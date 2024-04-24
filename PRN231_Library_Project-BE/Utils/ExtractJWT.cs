using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.Text;

namespace PRN231_Library_Project.Utils
{
    public class ExtractJWT
    {
        public static string PayloadJWTExtraction(string token, string extraction)
        {
            token = token.Replace("Bearer", "");
            string[] chunks = token.Split('.');

            string payload = Encoding.UTF8.GetString(Base64UrlDecode(chunks[1]));

            string[] entries = payload.Split(",");
            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (string entry in entries)
            {
                string[] keyValue = entry.Split(":");
                if (keyValue[0].Equals(extraction))
                {
                    int remove = 1;
                    if (keyValue[1].EndsWith("}"))
                    {
                        remove = 2;
                    }
                    keyValue[1] = keyValue[1].Substring(0, keyValue[1].Length - remove);
                    keyValue[1] = keyValue[1].Substring(1);

                    map[keyValue[0]] = keyValue[1];
                }
            }
            if (map.ContainsKey(extraction))
            {
                return map[extraction];
            }
            return null;
        }

        public static byte[] Base64UrlDecode(string input)
        {
            string base64 = input.Replace('-', '+').Replace('_', '/');
            while (base64.Length % 4 != 0)
            {
                base64 += "=";
            }
            return Convert.FromBase64String(base64);
        }
    }
}