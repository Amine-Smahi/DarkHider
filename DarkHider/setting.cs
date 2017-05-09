using System;
using System.IO;
using System.Windows.Forms;

namespace DarkHider
{
    public partial class setting : Form
    {
        public setting()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            using (var lan = new StreamWriter("datalang2.txt"))
            {
                if (langchouse.Value)
                    lan.WriteLine("arabic");
                else
                    lan.WriteLine("english");
            }

            MessageBox.Show("Saved Succesfully ! You need to restart the programme to apply the changes");
        }

        private void setting_Load(object sender, EventArgs e)
        {
            using (var lan2 = new StreamReader("datalang2.txt"))
            {
                var langMode = lan2.ReadLine();
                if (langMode == "arabic")
                    langchouse.Value = true;
                else
                    langchouse.Value = false;
            }
        }
    }
}