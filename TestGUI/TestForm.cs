using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X12translator;

namespace TestGUI
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxTest.Text += "start process";

                string[] x12DocInfo = new string[0];
                string EOL = "~";
                char delimiter = '*';

                X12.TranslateX12(x12DocInfo, EOL, delimiter);

            }
            catch(Exception ex)
            {
                //return errors
            }
        }
    }
}
