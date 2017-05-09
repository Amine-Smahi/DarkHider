using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DarkHider
{
    public partial class Message : Form
    {
        public Bitmap Btp;
        public Image Imagepath;
        private readonly string path = @"C:\DarkHider\lang.txt";

        public Message()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x39000;
                var cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var f = new Form1();
            f.Hideit(SecretMessageBox2.Text, Btp);
            f.ko = Btp;
            f.MainPictureBox.Image = Imagepath;
            using (TextReader tr = new StreamReader(path))
            {
                var langugeChoused = tr.ReadLine();

                if (langugeChoused == "arabic")
                {
                    f.bunifuTileButton1.LabelText = "إفتح صورة";
                    f.bunifuTileButton2.LabelText = "إحفظ الصورة";
                    f.bunifuTileButton3.LabelText = "أكتب الرسالة";
                    f.bunifuTileButton4.LabelText = "أظهر الصورة";
                    f.bunifuTileButton5.LabelText = "عن البرنامج";
                    f.bunifuTileButton6.LabelText = "إعداداتي";
                }
            }
            f.Show();
            Hide();
        }

        private void Message_Load(object sender, EventArgs e)
        {
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            SecretMessageBox2.Text = Clipboard.GetText();
        }
    }
}