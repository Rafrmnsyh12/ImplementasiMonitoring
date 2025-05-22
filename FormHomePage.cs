using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImplementasiMonitoring
{
    public partial class FormHomePage : Form
    {
        private string userRole; 

        public FormHomePage(string role)
        {
            InitializeComponent();
            userRole = role; 
        }
        private void FormHomePage_Load(object sender, EventArgs e)
        {
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void buttonMonitoring_Click(object sender, EventArgs e)
        {
            FormSensorData frmSensorData = new FormSensorData(userRole);
            frmSensorData.Show();
            this.Hide();
        }
    }
}
