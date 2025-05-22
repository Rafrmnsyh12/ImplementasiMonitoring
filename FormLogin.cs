using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ImplementasiMonitoring
{
    public partial class FormLogin : Form
    {
        string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=db_monitoring";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM data_user WHERE username = @username AND password = @password";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@username", textBoxUsername.Text);
            commandDatabase.Parameters.AddWithValue("@password", textBoxPassword.Text);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string role = reader["role"].ToString();
                    //Arahkan ke Homepage
                    FormHomePage frmHomePage = new FormHomePage(role); //pass role ke homepage
                    frmHomePage.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Akun Tidak Ditemukan! Daftar telebih dahulu.");
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //Kembali ke Form1 jika tombol back ditekan
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
