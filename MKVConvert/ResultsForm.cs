using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MKVConvert
{
    public partial class ResultsForm : Form
    {
        public ResultsForm()
        {
            InitializeComponent();
            
        }

        
        public string TextBoxValue
        {
            get { return txtBxResultsForm.Text; }
            set { txtBxResultsForm.Text = value; }
           

        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBxResultsForm_TextChanged(object sender, EventArgs e)
        {
            txtBxResultsForm.TextAlign = HorizontalAlignment.Center;
        }
    }
}
