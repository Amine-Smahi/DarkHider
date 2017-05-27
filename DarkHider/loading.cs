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
        private readonly Form1 fom = new Form1();
        private Graphics g;
        private readonly string path = @".\a\lang.txt";
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

                FileInfo fi = new FileInfo(@".\a\lang.txt");
                DirectoryInfo di = new DirectoryInfo(@".\a");
                if (!di.Exists)
                {
                    di.Create();
                }
                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                }

                /* if (!File.Exists(path))
                 {
                     File.Create(path).Dispose();
                     using (TextWriter tw = new StreamWriter(path))
                     {
                         tw.WriteLine("english");
                         tw.Close();
                     }
                 }
                 */
            }

            if (pbComplete == 70)
                if (File.Exists(path))
                    using (TextReader tr = new StreamReader(path))
                    {
                        var langugeChoused = tr.ReadLine();

                        if (langugeChoused == "arabic")
                        {
                            fom.bunifuTileButton1.LabelText = "إفتح صورة";
                            fom.bunifuTileButton2.LabelText = "إحفظ الصورة";
                            fom.bunifuTileButton3.LabelText = "أكتب الرسالة";
                            fom.bunifuTileButton4.LabelText = "أظهر الصورة";
                            fom.bunifuTileButton5.LabelText = "عن البرنامج";
                            fom.bunifuTileButton6.LabelText = "إعداداتي";
                        }
                    }

            picboxPB.Image = bmp;
            pbComplete++;

            if (pbComplete > 100)
            {
                g.Dispose();
                t.Stop();
                Hide();

                fom.Show();
            }
        }
    }
}