namespace Okol
{
    public partial class NotaryStart : Form
    {
        string projPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public NotaryStart()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SenderForm senderForm = new SenderForm();
            senderForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NotaryForm notaryForm = new NotaryForm();
            notaryForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RecieverForm recieverForm = new RecieverForm();
            recieverForm.ShowDialog();
        }

        private void NotaryStart_Load(object sender, EventArgs e)
        {
            foreach (string file in Directory.GetFiles(projPath + "\\Sender"))
            {
                File.Delete(file);
            }
            foreach (string file in Directory.GetFiles(projPath + "\\Notary"))
            {
                File.Delete(file);
            }
            foreach (string file in Directory.GetFiles(projPath + "\\Reciever"))
            {
                File.Delete(file);
            }
        }
    }
}