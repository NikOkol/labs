using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okol
{
    public partial class NotaryForm : Form
    {
        string projPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public NotaryForm()
        {
            InitializeComponent();
        }

        private void NotaryForm_Load(object sender, EventArgs e)
        {
            foreach (string file in Directory.GetFiles(projPath + "\\Notary"))
            {

                listBox1.Items.Add(file);
            }
        }

        private void button1_Click(object sender, EventArgs evargs)
        {

            int n, e, d;
            (n, e, d) = myRSA.GenerateRandomKey();
            foreach(var item in listBox1.Items)
            {
                string path = item.ToString();
                byte[] hash = FileSHA256(path);
                string hashStr = ByteArrayToString(hash);
                File.WriteAllText(projPath + "\\Sender\\" + path.Substring(path.LastIndexOf('\\')) + "_hash", hashStr);
                hashStr = myRSA.Encrypt(hashStr, n, d);
                File.WriteAllText(projPath + "\\Sender\\" + path.Substring(path.LastIndexOf('\\')) + "_hash_sign", hashStr);
                File.Delete(path);
            }
            File.WriteAllText(projPath + "\\Reciever\\" + "key", $"{n} {e}");
            
        }

        static byte[] FileSHA256(string path)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                return sha256.ComputeHash(fileBytes);
            }
        }
    

        static string ByteArrayToString(byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                builder.Append(array[i].ToString("x2"));
            }
            return builder.ToString();
        }

    }
}
