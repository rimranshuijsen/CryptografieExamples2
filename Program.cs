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
        public static class Menukeuze
        {
            public const string Md5 = "Md5";
            public const string Sha256 = "Sha256";
            public const string Salt = "Salt";
            public const string All = "All";
        }
        static void Main(string[] args)
        {
            bool letsgoagain = true;
            while (letsgoagain == true)
            {
                Console.Clear();
                PrintMenu("");
                var menukeuze = Console.ReadLine();
                Console.WriteLine("Wat wil je als input verwerken?");
                string textToProcess = Console.ReadLine();

                switch (menukeuze)
                { 
                    case Menukeuze.Md5:
                        ShowMd5ExampleOutput(textToProcess);
                        break;
                    case Menukeuze.Sha256:
                        Sha256ExampleOutput(textToProcess);
                        break;
                    case Menukeuze.Salt:
                        SaltExample(textToProcess);
                        break;
                    case Menukeuze.All:
                        ShowMd5ExampleOutput(textToProcess);
                        Sha256ExampleOutput(textToProcess);
                        SaltExample(textToProcess);
                        break;
                    default:
                        Console.Clear();
                        PrintMenu("Maak aub een geldige keuze");
                        break;
                }
                Console.WriteLine("\n nog een keer (Y/N)?");
                var again = Console.ReadLine();
                if (!again.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                {
                        letsgoagain = false;
                }
            }

        }

        private static void PrintMenu(string errormessage)
        {
            if (!String.IsNullOrEmpty(errormessage))
            {
                Console.WriteLine(errormessage);
                Console.WriteLine("");
            }
            Console.WriteLine("Voor Md5 voorbeeld type:" + Menukeuze.Md5);
            Console.WriteLine("Voor Sha256 voorbeeld type:"+ Menukeuze.Sha256);
            Console.WriteLine("Voor Salt voorbeeld type:" + Menukeuze.Salt);
            Console.WriteLine("Voor alle voorbeelden type:" + Menukeuze.All);
        }

        static void Sha256ExampleOutput(string toProcess)
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
        static void ShowMd5ExampleOutput(string textToProcess)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                string hash = Md5Toolkit.GetMd5Hash(md5Hash, textToProcess);

                Console.WriteLine("The MD5 hash of " + textToProcess + " is: " + hash + ".");
                /*
                if (Md5Toolkit.VerifyMd5Hash(md5Hash, textToProcess, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
                */
            }
        }

        static void SaltExample(string textToProcess)
        {
            string salt = CreateSalt(12);
            byte[] saltedValue = Encoding.UTF8.GetBytes(textToProcess + salt);
            var hashedWithSalt = new SHA256Managed().ComputeHash(saltedValue);
            var hashedWithoutSalt = new SHA256Managed().ComputeHash(Encoding.Unicode.GetBytes(textToProcess));

            Console.WriteLine("The salt is " +salt);
            Console.WriteLine("The SHA256Managed salted hash of " + textToProcess + " is: " + Convert.ToBase64String(hashedWithSalt) + ".");
            Console.WriteLine("The SHA256Managed hash of " + textToProcess + " is: " + Convert.ToBase64String(hashedWithoutSalt) + ".");

        }

        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}
