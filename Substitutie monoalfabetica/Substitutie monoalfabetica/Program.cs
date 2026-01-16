using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Substitutie_monoalfabetica
{
    public class MonoalphabeticCipher
    {
        private const string Alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string Encrypt(string plainText, string key)
        {
            return Transform(plainText, Alfabet, key.ToUpper());
        }

        public string Decrypt(string cipherText, string key)
        {
            return Transform(cipherText, key.ToUpper(), Alfabet);
        }

        private string Transform(string input, string fromAlfabet, string toAlfabet)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int index = fromAlfabet.IndexOf(char.ToUpper(c));
                    char mappedChar = toAlfabet[index];
                    sb.Append(isUpper ? mappedChar : char.ToLower(mappedChar));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public void FrequencyAnalysis(string cipherText)
        {
            string romanianFreq = "EAIORNSTLUCMDPBGFVSHJQXKZW";

            var counts = new Dictionary<char, int>();
            foreach (char c in cipherText.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    if (counts.ContainsKey(c)) counts[c]++;
                    else counts[c] = 1;
                }
            }

            var sortedChars = counts.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            string foundFreq = new string(sortedChars.ToArray());


            StringBuilder mappingAttempt = new StringBuilder();
            foreach (char c in cipherText)
            {
                if (char.IsLetter(c))
                {
                    int pos = foundFreq.IndexOf(char.ToUpper(c));
                    if (pos != -1 && pos < romanianFreq.Length)
                    {
                        char suggested = romanianFreq[pos];
                        mappingAttempt.Append(char.IsUpper(c) ? suggested : char.ToLower(suggested));
                    }
                    else mappingAttempt.Append('?');
                }
                else mappingAttempt.Append(c);
            }

            Console.WriteLine(mappingAttempt.ToString());
        }
    }

    class Program
    {
        static void Main()
        {
            var cipher = new MonoalphabeticCipher();

            string secretKey = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string text = "Acesta este un mesaj secret care va fi analizat statistic.";

            string encrypted = cipher.Encrypt(text, secretKey);
            Console.WriteLine($"Text Original: {text}");
            Console.WriteLine($"Text Criptat:  {encrypted}");

            cipher.FrequencyAnalysis(encrypted);

            Console.ReadKey();
        }
    }
}