using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace DarkHider
{
    public partial class Form1 : Form
    {
        public Bitmap bmp;
        public Bitmap ko;
        private readonly string path = @"C:\DarkHider\lang.txt";

        public Form1()
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

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            var open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
                MainPictureBox.Image = Image.FromFile(open_dialog.FileName);
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {

                var save_dialog = new SaveFileDialog();
                save_dialog.Filter = "PNG Image|*.png";

                if (save_dialog.ShowDialog() == DialogResult.OK)
                    ko.Save(save_dialog.FileName, ImageFormat.Png);
            
          
        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            try
            {
                bmp = (Bitmap) MainPictureBox.Image;

                var SecretMessage = Stegnography.extractText(bmp);
                var sm = new ShowMessage();
                sm.userMessage.Text = SecretMessage;

                using (TextReader tr = new StreamReader(path))
                {
                    var langugeChoused = tr.ReadLine();

                    if (langugeChoused == "arabic")
                        sm.bunifuCustomLabel2.Text = "الرسالة";
                }


                sm.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Pleas select an image !");
            }
        }

        private void bunifuTileButton5_Click(object sender, EventArgs e)
        {
            var ab = new About();
            using (TextReader tr = new StreamReader(path))
            {
                var langugeChoused = tr.ReadLine();

                if (langugeChoused == "arabic")
                {
                    ab.bunifuCustomLabel1.Text = "هذا البرنامج مطور و مصمم من طرف سماحي أمين مع أستوديو جات لايت";
                    ab.bunifuCustomLabel2.Text = "كل الحقوق محفوظة";
                }
            }
            ab.Show();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            var ms = new Message();
            ms.Btp = (Bitmap) MainPictureBox.Image;
            ms.Imagepath = MainPictureBox.Image;
            using (TextReader tr = new StreamReader(path))
            {
                var langugeChoused = tr.ReadLine();

                if (langugeChoused == "arabic")
                {
                    ms.bunifuCustomLabel1.Text = "أكتب الرسالة التي تريد إخفائها";
                    ms.bunifuFlatButton1.Text = "إخفاء الصورة";
                }
            }
            ms.Show();
            Hide();
        }


        public void Hideit(string a, Bitmap bmp)
        {
            try
            {
                if (a.Equals(""))
                {
                    MessageBox.Show("Please Enter Some Text You Want to Hide.", "Warning");

                    return;
                }

                bmp = Stegnography.embedText(a, bmp);

                MessageBox.Show("Your Text is now Hidden in the Image.\nUse the Save Image option from Menu.", "Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("You didnt chouse an image");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*for (double opst = 0.0; opst < 1.0; opst+= 0.1)
                        {
                            DateTime start = DateTime.Now;
                            this.Opacity = opst;
                            while (DateTime.Now.Subtract(start).TotalMilliseconds <= 30.0)
                            {
                                Application.DoEvents();
                            }
                        }*/
            var inf = new info();
            showinfo.Text = inf.RandomInfo();
        }

        private void bunifuTileButton6_Click(object sender, EventArgs e)
        {
            var st = new setting();
            using (TextReader tr = new StreamReader(path))
            {
                var langugeChoused = tr.ReadLine();

                if (langugeChoused == "arabic")
                {
                    st.bunifuCustomLabel2.Text = "العربية";
                    st.bunifuFlatButton1.Text = "إلغاء التثبيت";
                    st.bunifuFlatButton2.Text = "إحفظ";
                }
            }
            st.Show();
        }
    }
}