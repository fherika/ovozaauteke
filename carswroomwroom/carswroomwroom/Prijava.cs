using DocumentFormat.OpenXml.Wordprocessing;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static carswroomwroom.Form1;

namespace carswroomwroom
{
    public partial class Prijava : Form
    {
        public class Korisnik
        {
            public int id;
        };
        List<Korisnik> user = new List<Korisnik>();
        
        bool prijavljeno = false;
        Form1 form1 = new Form1();

        public Prijava()
        {
            InitializeComponent();
        }

        private void prijavagumb_Click(object sender, EventArgs e)
        {
            string connectionString = "server=davidrpi.ddns.net;port=3306;database=auti;user=Kranjcec;password=KranjcecJeKul!";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM ljudi", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Add(new Korisnik());
                            if (textBox1.Text == (reader.GetString(0)) && textBox2.Text == (reader.GetString(1)))
                            {
                                GlobalneVar.MyGlobalVariable = reader.GetString(0);
                                GlobalneVar.ime = reader.GetString(2);
                                GlobalneVar.prezime = reader.GetString(3);
                                this.Hide();
                                form1.Closed += (s, args) => this.Close();
                                form1.Show();
                                prijavljeno = true;
                                break;
                            }
                        }
                        if (prijavljeno == false)
                        {
                            MessageBox.Show("Netocni podaci za prijavu");
                        }
                    }
                }
                connection.Close();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registracija registracija = new Registracija();
            registracija.Closed += (s, args) => this.Close();
            registracija.Show();
        }
    }
}
