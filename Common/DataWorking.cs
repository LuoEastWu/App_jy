using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class DataWorking
    {

        /// <summary>
        /// 将对象序列号为Json字符串
        /// </summary>
        /// <param name="Obj">要序列话的对象</param>
        /// <returns>String</returns>
        public static String ObjToJson(object Obj)
        {

            return JsonConvert.SerializeObject(Obj);
        }


        /// <summary>
        /// 序列号Json文本为T数据类型
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="JsonText">Json字符串</param>
        /// <returns>T数据类型</returns>
        public static T JsonToObject<T>(string JsonText)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonText);  //将json数据转化为对象类型并赋值给list;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }








        /// <summary>
        /// Api专用的加密方式
        /// </summary>
        /// <param name="FuncName"></param>
        /// <param name="JsonText"></param>
        /// <returns></returns>
        public static NameValueCollection GetNameValueCollection(string JsonText, string Cusid, string Keytext, string FunctionName)
        {
            NameValueCollection nvc = new NameValueCollection();
            string s = GetMD5Hash(JsonText + Keytext);
            nvc.Add("JsonData", JsonText);
            nvc.Add("CusID", Cusid);
            nvc.Add("KeyMd5", GetMD5Hash(JsonText + Keytext));
            nvc.Add("FunctionName", FunctionName);
            return nvc;
        }

        /// <summary>
        /// 获取字符串MD5值
        /// </summary>
        /// <param name="input">要转换的文本</param>
        /// <returns>Md5值</returns>
        public static string GetMD5Hash(String input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString().ToLower();
        }


    }
}
