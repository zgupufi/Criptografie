using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtResults.Clear();
                    txtResults.AppendText("Se calculează hash-urile." + Environment.NewLine);

                    try
                    {
                        string path = ofd.FileName;
                        string results = await Task.Run(() => GetHashes(path));
                        txtResults.Text = results;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare: " + ex.Message);
                    }
                }
            }
        }

        private string GetHashes(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Rezultate pentru: " + Path.GetFileName(filePath));
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine("MD5:        " + ComputeHash(MD5.Create(), filePath));
            sb.AppendLine("SHA1:       " + ComputeHash(SHA1.Create(), filePath));
            sb.AppendLine("SHA256:     " + ComputeHash(SHA256.Create(), filePath));
            sb.AppendLine("SHA384:     " + ComputeHash(SHA384.Create(), filePath));
            sb.AppendLine("SHA512:     " + ComputeHash(SHA512.Create(), filePath));

            try
            {
                sb.AppendLine("RIPEMD160:  " + ComputeHash(RIPEMD160.Create(), filePath));
            }
            catch
            {
                sb.AppendLine("RIPEMD160:  Nu este suportat pe acest sistem.");
            }

            sb.AppendLine("SHA3:       Indisponibil în .NET Framework (necesita .NET 8 sau BouncyCastle)");

            return sb.ToString();
        }

        private string ComputeHash(HashAlgorithm algorithm, string path)
        {
            try
            {
                using (algorithm)
                using (var stream = File.OpenRead(path))
                {
                    byte[] hashBytes = algorithm.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
            catch (Exception ex)
            {
                return "Eroare: " + ex.Message;
            }
        }
    }
}