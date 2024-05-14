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

    public partial class SenderForm : Form
    {
        string projPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public SenderForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs eventargs)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(projPath + $"\\Sender\\{openFile.SafeFileName}"))
                {
                    MessageBox.Show("Файл уже добавлен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                listBox1.Items.Add(openFile.FileName);
            }
            else return;
            

            byte[] buffer = File.ReadAllBytes(openFile.FileName);
            
            File.WriteAllBytes(projPath + $"\\Sender\\{openFile.SafeFileName}", buffer);


        }


        private void button2_Click(object sender, EventArgs e)
        {
            foreach(var file in Directory.GetFiles(projPath + "\\Sender"))
            {
                if (file.Substring(file.IndexOf('.')).Contains("_hash"))
                {
                    continue;
                }
                byte[] buffer = File.ReadAllBytes(file);
                File.WriteAllBytes(projPath + "\\Notary\\" + file.Substring(file.LastIndexOf('\\')), buffer);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach(var item in listBox2.Items)
            {
                string filename = item.ToString().Substring(item.ToString().LastIndexOf('\\'));
                File.Copy(item.ToString(), projPath + "\\Reciever\\" + filename);
                File.Copy(item.ToString() + "_hash", projPath + "\\Reciever\\" + filename + "_hash");
                File.Copy(item.ToString() + "_hash_sign", projPath + "\\Reciever\\" + filename + "_hash_sign");
            }
        }

        private void SenderForm_Load(object sender, EventArgs e)
        {
            foreach (string file in Directory.GetFiles(projPath + "\\Sender"))
            {
                if (File.Exists(file + "_hash"))
                {
                    listBox2.Items.Add(file);
                }

            }
        }
    }
}
