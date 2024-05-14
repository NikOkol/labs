using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okol
{
    public partial class RecieverForm : Form
    {
        string projPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public RecieverForm()
        {
            InitializeComponent();
        }

        private void RecieverForm_Load(object sender, EventArgs e)
        {
            foreach (string file in Directory.GetFiles(projPath + "\\Reciever"))
            {
                if (file.Substring(file.LastIndexOf('\\') + 1) == "key" || file.Substring(file.IndexOf('.')).Contains("_hash"))
                {
                    continue;
                }
                listBox1.Items.Add(file);
                comboBox1.Items.Add(file);
            }
        }

        private void button1_Click(object sender, EventArgs evargs)
        {
            if (comboBox1.SelectedItem == null) return;
            int n, e;
            string[] key = File.ReadAllText(projPath + "\\Reciever\\" + "key").Split(' ');
            n = int.Parse(key[0]);
            e = int.Parse(key[1]);
            string fileName = comboBox1.SelectedItem.ToString();
            string fileHash = File.ReadAllText(fileName + "_hash");
            string signedHash = File.ReadAllText(fileName + "_hash_sign");
            string decryptedHash = myRSA.Decrypt(signedHash, n, e);
            label3.Text = $"{fileHash}\n{decryptedHash}";
            if (fileHash == decryptedHash) label3.Text += "\nПодпись верна";
        }
    }
}
