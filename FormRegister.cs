using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ImplementasiMonitoring 
{
    public partial class FormRegister : Form
    {
        string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=db_monitoring";

        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            // Form Load Code if needed
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!");
                return;
            }

            string query = "INSERT INTO data_user (username, password, role) VALUES (@username, @password, @role)";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@username", username);
            commandDatabase.Parameters.AddWithValue("@password", password);
            commandDatabase.Parameters.AddWithValue("@role", "user"); // Default role adalah user

            try
            {
                databaseConnection.Open();
                commandDatabase.ExecuteNonQuery();
                databaseConnection.Close();
                MessageBox.Show("Registrasi Berhasil!");
                this.Hide();

                // Pass the role to FormHomePage
                FormHomePage formHomePage = new FormHomePage("user"); // Mengirim role "user" ke FormHomePage
                formHomePage.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat registrasi: " + ex.Message);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
