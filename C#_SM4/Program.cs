using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            String text = "0123456789abcdefgh哈哈哈";
            String key = "0123456789abcdef";
            Console.WriteLine("[明文]: " + text);
            Console.WriteLine("[密钥]: " + key);

            String result1 = SM4Util.encrypt(text, key);
            Console.WriteLine("[加密]: " + result1);

            String result2 = SM4Util.decrypt(result1, key);
            Console.WriteLine("[解密]: " + result2);

            Console.ReadKey();

        }
    }
}
