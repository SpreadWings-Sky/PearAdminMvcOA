using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace PearAdminMvcOA.Tools
{
    //公共业务逻辑层
    public class CommonBLL
    {
        /// <summary>
        /// 哈希加密一个字符串
        /// </summary>
          /// <param name="Security"></param>
        /// <returns></returns>
        public static string HashEncoding(string Security)
        {
            byte[] Value;
            UnicodeEncoding Code = new UnicodeEncoding();
            byte[] Message = Code.GetBytes(Security);
            SHA512Managed Arithmetic = new SHA512Managed();
            Value = Arithmetic.ComputeHash(Message);
            Security = "";
            foreach (byte o in Value)
            {
                Security += (int)o + "O";
            }
            return Security;
        }

        ///MD5加密
        public static string Md5Encoding(string passText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(passText);
            byte[] targetData = md5.ComputeHash(fromData);
            //加密后的字符串
            string byteStr = "";
            for (int i = 0; i < targetData.Length; i++)
            {
                byteStr += targetData[i].ToString("x2");
            }
            return byteStr;
        }
    }
}