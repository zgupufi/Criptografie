using System;
using System.Collections.Generic;
using System.Text;

namespace Cifrul_Playfair
{
    public class PlayfairCipher
    {
        private char[,] matrix = new char[5, 5];
        private const char Filler = 'X'; //caracter de umplere pentru litere identice sau lungime impara

        public PlayfairCipher(string key)
        {
            GenerateMatrix(key);
        }

        private void GenerateMatrix(string key)
        {
            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; 
            string combined = (key.ToUpper() + alphabet).Replace("J", "I");

            HashSet<char> used = new HashSet<char>();
            int row = 0, col = 0;

            foreach (char c in combined)
            {
                if (char.IsLetter(c) && !used.Contains(c))
                {
                    used.Add(c);
                    matrix[row, col] = c;
                    col++;
                    if (col == 5) { col = 0; row++; }
                }
            }
        }

        private List<string> PrepareText(string text)
        {
            text = text.ToUpper().Replace("J", "I").Replace(" ", "");
            List<string> digrams = new List<string>();

            for (int i = 0; i < text.Length; i += 2)
            {
                char first = text[i];
                char second;

                if (i + 1 < text.Length)
                {
                    if (text[i] == text[i + 1])
                    {
                        second = Filler;
                        i--; 
                    }
                    else
                    {
                        second = text[i + 1];
                    }
                }
                else
                {
                    second = Filler;
                }
                digrams.Add($"{first}{second}");
            }
            return digrams;
        }

        public string Process(string text, bool encrypt)
        {
            List<string> digrams = PrepareText(text);
            StringBuilder result = new StringBuilder();
            int direction = encrypt ? 1 : -1;

            foreach (string d in digrams)
            {
                var (r1, c1) = FindPosition(d[0]);
                var (r2, c2) = FindPosition(d[1]);

                if (r1 == r2) 
                {
                    result.Append(matrix[r1, (c1 + direction + 5) % 5]);
                    result.Append(matrix[r2, (c2 + direction + 5) % 5]);
                }
                else if (c1 == c2) 
                {
                    result.Append(matrix[(r1 + direction + 5) % 5, c1]);
                    result.Append(matrix[(r2 + direction + 5) % 5, c2]);
                }
                else 
                {
                    result.Append(matrix[r1, c2]);
                    result.Append(matrix[r2, c1]);
                }
            }
            return result.ToString();
        }

        private (int row, int col) FindPosition(char c)
        {
            for (int r = 0; r < 5; r++)
                for (int col = 0; col < 5; col++)
                    if (matrix[r, col] == c) return (r, col);
            return (-1, -1);
        }

        public void PrintMatrix()
        {
            Console.WriteLine("Matricea Playfair:");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++) Console.Write(matrix[i, j] + " ");
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Introdu cheia: ");
            string key = Console.ReadLine();
            PlayfairCipher pc = new PlayfairCipher(key);
            pc.PrintMatrix();

            Console.Write("\nIntrodu textul: ");
            string text = Console.ReadLine();

            string encrypted = pc.Process(text, true);
            Console.WriteLine($"\nCriptat: {encrypted}");

            string decrypted = pc.Process(encrypted, false);
            Console.WriteLine($"Decriptat: {decrypted}");
        }
    }
}