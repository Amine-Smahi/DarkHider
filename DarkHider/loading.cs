using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DarkHider
{
    public partial class loading : Form
    {
        private readonly Timer t = new Timer();
        private Bitmap bmp;
        private readonly Form1 f = new Form1();
        private Graphics g;
        private double pbUnit;
        private int pbWIDTH, pbHEIGHT, pbComplete;

        public loading()
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

        private void loading_Load(object sender, EventArgs e)
        {
            pbWIDTH = picboxPB.Width;
            pbHEIGHT = picboxPB.Height;

            pbUnit = pbWIDTH / 100.0;
            pbComplete = 0;
            bmp = new Bitmap(pbWIDTH, pbHEIGHT);

            //timer 270
            t.Interval = 50; //in millisecond
            t.Tick += t_Tick;

            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            var myBrush = new SolidBrush(Color.FromArgb(37, 155, 36));
            g.FillRectangle(myBrush, new Rectangle(0, 0, (int) (pbComplete * pbUnit), pbHEIGHT));

            if (pbComplete == 60)
            {
                t.Interval = 40;
                using (var lan2 = new StreamReader("datalang2.txt"))
                {
                    var langMode = lan2.ReadLine();
                    if (langMode == "arabic")
                    {
                        f.bunifuTileButton1.LabelText = "إفتح صورة";
                        f.bunifuTileButton2.LabelText = "إحفظ الصورة";
                        f.bunifuTileButton3.LabelText = "أكتب الرسالة";
                        f.bunifuTileButton4.LabelText = "أظهر الصورة";
                        f.bunifuTileButton5.LabelText = "عن البرنامج";
                        f.bunifuTileButton6.LabelText = "إعداداتي";
                    }
                }
            }

            picboxPB.Image = bmp;
            pbComplete++;

            if (pbComplete > 100)
            {
                g.Dispose();
                t.Stop();
                Hide();
                f.Show();
            }
        }
    }
}