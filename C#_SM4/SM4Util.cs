using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// SM4 C#版本加解密库
    /// </summary>
    class SM4Util
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="key">key</param>
        /// <returns>密文</returns>
        public static String encrypt(String text, String key) 
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            // key -> byte
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] MK = new byte[16];
            for (int i = 0; i < keyBytes.Length; i++)
            {
                MK[i] = keyBytes[i];
            }

            // 输入
            int offset = 16 - textBytes.Length % 16;
            Console.WriteLine("offset: " + offset);

            int length = textBytes.Length + offset;
            Console.WriteLine("length: " + length);

            byte[] PlainText = new byte[length];
            for (int i = 0; i < textBytes.Length; i++)
            {
                PlainText[i] = textBytes[i];
            }

            // pkcs7 padding
            for (int i = textBytes.Length; i < length; i++)
            {
                PlainText[i] = (byte)offset;
            }

            //输出
            byte[] CipherText = new byte[length];
            byte[] CipherTemp = new byte[16];
            byte[] PlainTemp = new byte[16];

            for (int i = 0; i < length; i += 16)
            {
                for (int j = 0; j < 16; j++)
                {
                    PlainTemp[j] = PlainText[i+j];
                }
                
                SM4.SM4_Encrypt(MK, PlainTemp, CipherTemp);

                for (int j = 0; j < 16; j++)
                {
                    CipherText[i+j] = CipherTemp[j];
                }
            }

            return DataUtil.byteArrayToHexStr(CipherText);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">密文</param>
        /// <param name="key">key</param>
        /// <returns>明文</returns>
        public static String decrypt(String text, String key)
        {
            // key -> byte
            byte[] MK = new byte[16];
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            for (int i = 0; i < keyBytes.Length; i++)
            {
                MK[i] = keyBytes[i];
            }

            //输入
            byte[] CipherText = DataUtil.hexStringToByteArray(text);
            byte[] CipherTemp = new byte[16];

            //输出
            int length = CipherText.Length;
            byte[] PlainText = new byte[length];
            byte[] PlainTemp = new byte[16];

            for (int i = 0; i < length; i += 16)
            {
                for (int j = 0; j < 16; j++)
                {
                    CipherTemp[j] = CipherText[i+j];
                }
                
                SM4.SM4_Decrypt(MK, CipherTemp, PlainTemp);

                for (int j = 0; j < 16; j++)
                {
                    PlainText[i+j] = PlainTemp[j];
                }
            }

            int last = (int)PlainText[length-1];
            Console.WriteLine("last: " + last);

            int RealLength = length - last;
            byte[] RealText = new byte[RealLength];


            for (int i = 0; i < RealLength; i++)
            {
                RealText[i] = PlainText[i];
            }

            return DataUtil.byteToString(RealText);
        }
    }
}
