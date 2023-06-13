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
    public partial class Kupnja : Form
    {
        Prijava prijava = new Prijava();
        public Kupnja()
        {
            InitializeComponent();
            this.Load += Kupnja_Load;
        }

        private void Kupnja_Load(object sender, EventArgs e)
        {
            menuStrip1.Items[0].Text = GlobalneVar.MyGlobalVariable;
            pictureBox1.Image = GlobalneVar.slikakupnja;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = GlobalneVar.markakupnja;
            label2.Text = GlobalneVar.modelkupnja;
            if (GlobalneVar.stanjekupnja == 0)
            {
                label3.Text = "Stanje: novo";
            }
            else
            {
                label3.Text = "Stanje: rabljeno";
            }
            label4.Text = "Vrsta motora: "+GlobalneVar.vrsta_motorakupnja.ToLower();
            if (GlobalneVar.mjenjackupnja == 0)
            {
                label5.Text = "Mjenjac: automatski";
            }
            else
            {
                label5.Text = "Mjenjac: rucni";
            }
            label6.Text = "Kilometraza: " + GlobalneVar.kilometrazakupnja+" km";
            label7.Text = "Godiste: " + GlobalneVar.godistekupnja+".";
            label8.Text = "Lokacija: " + GlobalneVar.lokacijakupnja;
            label9.Text = "Jacina motora: " + GlobalneVar.jacina_motorakupnja + "ks";
            label10.Text = "Cijena: " + GlobalneVar.cijenakupnja + "€";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Odaberi putanju gdje ce se spremiti racun.");
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = folderDialog.SelectedPath;
                    string fileName = "racun_za_auto.txt";
                    string filePath = Path.Combine(path, fileName);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("RACUN ZA SLJEDECI AUTO");
                    sb.AppendLine("Marka: " + GlobalneVar.markakupnja);
                    sb.AppendLine("Model: " + GlobalneVar.modelkupnja);
                    sb.AppendLine("Stanje: " + (GlobalneVar.stanjekupnja == 0 ? "novo" : "rabljeno"));
                    sb.AppendLine("Vrsta motora: " + GlobalneVar.vrsta_motorakupnja.ToLower());
                    sb.AppendLine("Mjenjač: " + (GlobalneVar.mjenjackupnja == 0 ? "automatski" : "ručni"));
                    sb.AppendLine("Kilometraža: " + GlobalneVar.kilometrazakupnja + " km");
                    sb.AppendLine("Godište: " + GlobalneVar.godistekupnja + ".");
                    sb.AppendLine("Lokacija: " + GlobalneVar.lokacijakupnja);
                    sb.AppendLine("Jačina motora: " + GlobalneVar.jacina_motorakupnja + " ks");
                    sb.AppendLine("Cijena: " + GlobalneVar.cijenakupnja + "€");
                    sb.AppendLine("Auto mozete pokupiti u bilo koje vrijeme na gore napisanoj lokaciji.");
                    sb.AppendLine("Auto je od ovoga trenutka rezerviran samo za Vas, tako da Vas molimo da pri pokupljanju auta ponesete sav novac koji je potreban za kupnju istog.");
                    sb.AppendLine("Novi vlasnik auta je "+GlobalneVar.ime.ToUpper()+" "+GlobalneVar.prezime.ToUpper());

                    File.WriteAllText(filePath, sb.ToString());

                    string query = "DELETE FROM modeli WHERE model = @ID";
                    string idAutomobila = GlobalneVar.modelkupnja;
                    string connectionString = "server=davidrpi.ddns.net;port=3306;database=auti;user=Kranjcec;password=KranjcecJeKul!";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", idAutomobila);

                            try
                            {
                                connection.Open();

                                int rowsAffected = command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Došlo je do greške pri brisanju podataka iz baze podataka: " + ex.Message);
                            }
                        }
                    }


                    MessageBox.Show("Racun je spremljen u: " + filePath);
                    
                }
            }
        }

        private void povratakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 forma = new Form1();
            this.Hide();
            forma.Closed += (s, args) => this.Close();
            forma.Show();
        }

        private void odjavaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            prijava.Closed += (s, args) => this.Close();
            prijava.Show();
        }
    }
}
