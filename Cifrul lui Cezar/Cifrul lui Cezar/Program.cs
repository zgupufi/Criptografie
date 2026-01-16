
using System;
using System.Text;

namespace Cifrul_lui_Cezar
{
    public class CaesarCipher
    {
        private const int AlfabetSize = 26;

        public string Encrypt(string plainText, int shift = 3)
        {
            return Transform(plainText, shift);
        }

        public string Decrypt(string cipherText, int shift = 3)
        {
            return Transform(cipherText, -shift);
        }

        public void BruteForceAnalysis(string cipherText)
        {
            for (int key = 1; key < AlfabetSize; key++)
            {
                string attempt = Decrypt(cipherText, key);
                Console.WriteLine($"Cheie {key:D2}: {attempt}");
            }
        }


        private string Transform(string input, int shift)
        {
            if (string.IsNullOrEmpty(input)) return input;

            StringBuilder result = new StringBuilder();

            foreach (char character in input)
            {
                if (char.IsLetter(character))
                {
                    char offset = char.IsUpper(character) ? 'A' : 'a';

                    int transformedPos = (character - offset + shift) % AlfabetSize;

                    if (transformedPos < 0)
                        transformedPos += AlfabetSize;

                    result.Append((char)(transformedPos + offset));
                }
                else
                {
                    result.Append(character);
                }
            }

            return result.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            CaesarCipher cipher = new CaesarCipher();

            string originalText = Console.ReadLine();

            string encrypted = cipher.Encrypt(originalText, 3);
            Console.WriteLine($" {encrypted}");

            string decrypted = cipher.Decrypt(encrypted, 3);
            Console.WriteLine($"{decrypted}");

            cipher.BruteForceAnalysis(encrypted);

        }
    }
}