using System;
using System.Text;

namespace Cifrul__n
{
    public class CaesarCipher
    {
        private const int AlfabetSize = 26;

        public string Encrypt(string plainText, int n)
        {
            return Transform(plainText, n);
        }


        public string Decrypt(string cipherText, int n)
        {
            return Transform(cipherText, -n);
        }

        public void Cryptanalysis(string cipherText)
        {
            for (int key = 0; key < AlfabetSize; key++)
            {
                string result = Decrypt(cipherText, key);
                Console.WriteLine($"Cheia n={key:D2}: {result}");
            }
        }

        private string Transform(string input, int n)
        {
            if (string.IsNullOrEmpty(input)) return input;

            StringBuilder sb = new StringBuilder();

            int shift = n % AlfabetSize;

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';

                    int pos = (c - offset + shift) % AlfabetSize;

                    if (pos < 0) pos += AlfabetSize;

                    sb.Append((char)(pos + offset));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            CaesarCipher caesar = new CaesarCipher();

            string plaintext = "HELLO";
            int n = 5;

            string encrypted = caesar.Encrypt(plaintext, n);
            Console.WriteLine($"Plaintext: {plaintext}");
            Console.WriteLine($"Cheie (n): {n}");
            Console.WriteLine($"Ciphertext: {encrypted}"); // Rezultat: MJQQT

            string decrypted = caesar.Decrypt(encrypted, n);
            Console.WriteLine($"Decriptare corectă: {decrypted}");

            caesar.Cryptanalysis("MJQQT");

            Console.ReadKey();
        }
    }
}