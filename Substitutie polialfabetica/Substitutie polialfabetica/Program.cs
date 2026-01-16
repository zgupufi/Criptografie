using System;
using System.Text;
using System.Collections.Generic;

namespace Substitutie_polialfabetica
{
    public class VigenereCipher
    {
        private const string AlfabetStandard = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly string[] alfabete;
        private readonly int n;

        public VigenereCipher(string[] alfabeteSubstitutie)
        {
            this.alfabete = alfabeteSubstitutie;
            this.n = alfabeteSubstitutie.Length;
        }

        public string Encrypt(string plainText)
        {
            StringBuilder sb = new StringBuilder();
            int letterIndex = 0;

            foreach (char c in plainText.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int currentAlfabetIdx = letterIndex % n;
                    string alfabetCurent = alfabete[currentAlfabetIdx];

                    int posInStandard = AlfabetStandard.IndexOf(c);

                    sb.Append(alfabetCurent[posInStandard]);

                    letterIndex++;
                }
                else
                {
                    sb.Append(c); 
                }
            }
            return sb.ToString();
        }

        public string Decrypt(string cipherText)
        {
            StringBuilder sb = new StringBuilder();
            int letterIndex = 0;

            foreach (char c in cipherText.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int currentAlfabetIdx = letterIndex % n;
                    string alfabetCurent = alfabete[currentAlfabetIdx];

                    int posInSubstitutie = alfabetCurent.IndexOf(c);

                    sb.Append(AlfabetStandard[posInSubstitutie]);

                    letterIndex++;
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
            string[] alfabeteSpecifice = new string[]
            {
                "FRQSPYMHNJOELKDVAGXTIWBUZC",
                "SWZCINJTELAFQUMKPXDOVBRGHY", 
                "CITYWLNZEVOMQGUPJFXBRAHSKD" 
            };

            VigenereCipher vigenere = new VigenereCipher(alfabeteSpecifice);

            string textClar = "ABBBCCAB";

            string criptat = vigenere.Encrypt(textClar);
            string decriptat = vigenere.Decrypt(criptat);

            Console.WriteLine($"Text Clar:    {textClar}");
            Console.WriteLine($"Text Criptat: {criptat}");
            Console.WriteLine($"Decriptat:    {decriptat}");

            Console.ReadKey();
        }
    }
}