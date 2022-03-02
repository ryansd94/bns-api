using BNS.Utilities.Constant;
using Newtonsoft.Json;
using System;
using System.IO;
using static BNS.Utilities.Enums;

namespace BNS.Utilities
{
    public static class Ultility
    {

        public static String MD5Encrypt(String plainText)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider crypt = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(plainText);
            bs = crypt.ComputeHash(bs); System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        public static string GetCacheKey(EDataType type, Guid shopIndex, Guid? branchIndex, Guid? areaIndex = null)
        {
            string pGuid = type.ToString() + "_" + shopIndex;
            if (branchIndex != null && branchIndex.HasValue)
                pGuid += "_" + branchIndex;
            if (areaIndex != null && areaIndex.HasValue)
                pGuid += "_" + areaIndex;
            return pGuid;
        }
        public static string GetPath(Guid ShopIndex, EUploadType type)
        {
            var result = "";
            string directory = $"/{Constants.__PATH_UPLOAD_FILE}/{ShopIndex }/{type.ToString()}/";
            var webRootFolder = "wwwroot";
            result = $"{webRootFolder}/{directory}";
            return result;
        }
        public static string GetPath(string host, Guid ShopIndex, EUploadType type)
        {
            var result = "";
            string directory = $"/{Constants.__PATH_UPLOAD_FILE}/{ShopIndex }/{type.ToString()}/";
            var hostFile = $"{directory}";
            result = $"{host}{hostFile}";
            return result;
        }

        public static void GetJsonFile<T>(ref T model, string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                try
                {
                    model = JsonConvert.DeserializeObject<T>(json);
                }
                catch { }
            }
        }

       
    }
}
