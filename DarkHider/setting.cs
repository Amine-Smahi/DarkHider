using System;
using System.IO;
using System.Windows.Forms;

namespace DarkHider
{
    public partial class setting : Form
    {
        private readonly string path = @".\a\lang.txt";

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
            using (TextWriter tw = new StreamWriter(path))
            {
                if (langchouse.Value)
                    tw.WriteLine("arabic");
                else
                    tw.WriteLine("english");
            }

            MessageBox.Show("Saved Succesfully ! You need to restart the programme to apply the changes");
        }

        private void setting_Load(object sender, EventArgs e)
        {
            using (TextReader tr = new StreamReader(path))
            {
                var langugeChoused = tr.ReadLine();
                if (langugeChoused == "arabic")
                    langchouse.Value = true;
                else
                    langchouse.Value = false;
            }
        }
    }
}