using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace ImplementasiMonitoring
{
    public partial class FormSensorData : Form
    {
        string connectionString = "server=127.0.0.1;port=3306;username=root;password=;database=db_monitoring";
        private string userRole;

        public FormSensorData(string role)
        {
            InitializeComponent();
            userRole = role;
            InitializeChart();
            SetRoleAccess();
        }
        private void InitializeChart()
        {
            if (chartSensor.Series.IsUniqueName("Soil Moisture"))
            {
                chartSensor.Series.Add("Soil Moisture");
            }
            chartSensor.Series["Soil Moisture"].ChartType = SeriesChartType.Line;
            chartSensor.Series["Soil Moisture"].Color = Color.Blue;

            if (chartSensor.Series.IsUniqueName("Ultrasonic"))
            {
                chartSensor.Series.Add("Ultrasonic");
            }
            chartSensor.Series["Ultrasonic"].ChartType = SeriesChartType.Line;
            chartSensor.Series["Ultrasonic"].Color = Color.Orange;

            if (chartSensor.Series.IsUniqueName("Temperature"))
            {
                chartSensor.Series.Add("Temperature");
            }
            chartSensor.Series["Temperature"].ChartType = SeriesChartType.Line;
            chartSensor.Series["Temperature"].Color = Color.Red;
        }

        private void SetRoleAccess()
        {
            if (userRole == "admin" || userRole == "operator")
            {
                textBoxSoilMoisture.Enabled = true;
                textBoxUltrasonic.Enabled = true;
                textBoxTemperature.Enabled = true;
                buttonInput.Enabled = true;
                labelSoilMoisture.Enabled = true;
                labelUltrasonic.Enabled = true;
                labelTemperature.Enabled = true;
            }
            else
            {
                textBoxSoilMoisture.Enabled = false;
                textBoxUltrasonic.Enabled = false;
                textBoxTemperature.Enabled = false;
                buttonInput.Enabled = false;
                labelSoilMoisture.Enabled = false;
                labelUltrasonic.Enabled = false;
                labelTemperature.Enabled = false;
                this.Hide();
            }
        }

        // Fungsi untuk memuat data sensor dan menambahkannya ke grafik
        private void LoadSensorData()
        {
            string query = "SELECT * FROM sensor_data";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                //menambah data ke grafik
                chartSensor.Series["Soil Moisture"].Points.Clear();
                chartSensor.Series["Ultrasonic"].Points.Clear();
                chartSensor.Series["Temperature"].Points.Clear();

                while (reader.Read())
                {
                    DateTime timestamp = reader.GetDateTime("timestamp");
                    decimal soilMoisture = reader.GetDecimal("soil_moisture");
                    decimal ultrasonic = reader.GetDecimal("ultrasonic");
                    decimal temperature = reader.GetDecimal("temperature");

                    chartSensor.Series["Soil Moisture"].Points.AddXY(timestamp, soilMoisture);
                    chartSensor.Series["Ultrasonic"].Points.AddXY(timestamp, ultrasonic);
                    chartSensor.Series["Temperature"].Points.AddXY(timestamp, temperature);


                }

                databaseConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormHomePage frmHomePage = new FormHomePage("user");
            frmHomePage.Show();
            this.Hide(); 
            
        }

        private void FormSensorData_Load(object sender, EventArgs e)
        {
            InitializeChart();
            LoadSensorData();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSoilMoisture.Text) || string.IsNullOrEmpty(textBoxUltrasonic.Text) || string.IsNullOrEmpty(textBoxTemperature.Text))
            {
                MessageBox.Show("Harap masukkan semua data sensor!");
                return;
            }

            chartSensor.Series["Soil Moisture"].Points.AddY(Convert.ToDecimal(textBoxSoilMoisture.Text));
            chartSensor.Series["Ultrasonic"].Points.AddY(Convert.ToDecimal(textBoxUltrasonic.Text));
            chartSensor.Series["Temperature"].Points.AddY(Convert.ToDecimal(textBoxTemperature.Text));

            textBoxSoilMoisture.Clear();
            textBoxUltrasonic.Clear();
            textBoxTemperature.Clear();
        }
    }
}
