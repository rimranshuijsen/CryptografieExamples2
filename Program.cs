using CryptografieExamples.Md5Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptografieExamples
{
    class Program
    {
        static void Main(string[] args)
        {

            string textToProcess = "Hello World!";

        }

        void Sha256ExampleOutput(string toProcess)
        {
            // Initialize a SHA256 hash object.
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] hashValue = mySHA256.ComputeHash(Encoding.Unicode.GetBytes(toProcess));
                // Write the name and hash value of the file to the console.
                Console.Write($"{toProcess}: ");
                PrintByteArray(hashValue);
            }
        }

        // Display the byte array in a readable format.
        public static void PrintByteArray(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]:X2}");
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }
        void ShowMd5ExampleOutput(string textToProcess)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                string hash = Md5Toolkit.GetMd5Hash(md5Hash, textToProcess);

                Console.WriteLine("The MD5 hash of " + textToProcess + " is: " + hash + ".");

                Console.WriteLine("Verifying the hash...");

                if (Md5Toolkit.VerifyMd5Hash(md5Hash, textToProcess, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
            }
        }

        void SaltExample(string textToProcess)
        {
            string salt = CreateSalt(12);
            byte[] saltedValue = Encoding.UTF8.GetBytes(textToProcess + salt);
            var hashedWithSalt = new SHA256Managed().ComputeHash(saltedValue);
            var hashedWithoutSalt = new SHA256Managed().ComputeHash(Encoding.Unicode.GetBytes(textToProcess));

            Console.WriteLine("The salt is " +salt);
            Console.WriteLine("The SHA256Managed salted hash of " + textToProcess + " is: " + hashedWithSalt + ".");
            Console.WriteLine("The SHA256Managed hash of " + textToProcess + " is: " + hashedWithoutSalt + ".");

        }

        public string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}
