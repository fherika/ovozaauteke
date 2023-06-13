using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carswroomwroom
{
    public partial class Registracija : Form
    {
        public Registracija()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=davidrpi.ddns.net;port=3306;database=auti;user=Kranjcec;password=KranjcecJeKul!");
            connection.Open();
            string sql = "INSERT INTO ljudi (nadimak, lozinka, Ime, Prezime, Mail) VALUES (@value1, @value2, @value3, @value4, @value5)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@value1", textBox3.Text);
            command.Parameters.AddWithValue("@value2", textBox4.Text);
            command.Parameters.AddWithValue("@value3", textBox1.Text);
            command.Parameters.AddWithValue("@value4", textBox2.Text);
            command.Parameters.AddWithValue("@value5", textBox5.Text);
            command.ExecuteNonQuery();
            connection.Close();
            this.Hide();
            Application.Restart();
        }
    }
}
