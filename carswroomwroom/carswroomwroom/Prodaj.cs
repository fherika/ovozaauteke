using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace carswroomwroom
{
    public partial class Prodaj : Form
    {
        public int stanje1=0;
        public int mjenjac1=0;
        byte[] imageBytes;
        Form1 forma=new Form1();
        public Prodaj()
        {
            InitializeComponent();
        }




        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            stanje1 = 1;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "server=davidrpi.ddns.net;port=3306;database=auti;user=Kranjcec;password=KranjcecJeKul!";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string marka = textBox1.Text;
                string model = textBox2.Text;
                int stanje = stanje1;
                string vrsta_motora = textBox4.Text;
                int mjenjac = mjenjac1;
                string kilometraza = textBox6.Text;
                string godiste = textBox7.Text;
                string lokacija = textBox8.Text;
                string jacina_motora = textBox9.Text;
                string cijena = textBox10.Text;

                string query = "INSERT INTO modeli (marka, model, stanje, vrsta_motora, mjenjac, kilometraza, godiste, lokacija, jacina_motora, cijena, slika) VALUES (@marka, @model, @stanje, @vrsta_motora, @mjenjac, @kilometraza, @godiste, @lokacija, @jacina_motora, @cijena, @slika)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@marka", marka);
                    command.Parameters.AddWithValue("@model", model);
                    command.Parameters.AddWithValue("@stanje", stanje);
                    command.Parameters.AddWithValue("@vrsta_motora", vrsta_motora);
                    command.Parameters.AddWithValue("@mjenjac", mjenjac);
                    command.Parameters.AddWithValue("@kilometraza", kilometraza);
                    command.Parameters.AddWithValue("@godiste", godiste);
                    command.Parameters.AddWithValue("@lokacija", lokacija);
                    command.Parameters.AddWithValue("@jacina_motora", jacina_motora);
                    command.Parameters.AddWithValue("@cijena", cijena);
                    command.Parameters.AddWithValue("@slika", imageBytes);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            stanje1 = 0;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            mjenjac1 = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mjenjac1 = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.png; *.gif; *.bmp)|*.jpg; *.png; *.gif; *.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    imageBytes = File.ReadAllBytes(imagePath);
                }
            }
        }
    }
}
