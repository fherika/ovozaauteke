using System.Collections;
using System.IO;
using System.Windows.Forms;
using MySqlConnector;
using System.IO;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using RadioButton = System.Windows.Forms.RadioButton;
using System.Data;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using DocumentFormat.OpenXml.Vml;

namespace carswroomwroom
{
    public partial class Form1 : Form
    {
        public class Car
        {
            public string marka;
            public string model;
            public int stanje;
            public string vrsta_motora;
            public int mjenjac;
            public int kilometraza;
            public int godiste;
            public string lokacija;
            public int jacina_motora;
            public int cijena;
            public byte[] imageData;
            public Image slika;
        };
        List<Car> cars = new List<Car>();
        List<Panel> panels = new List<Panel>();

        
        Kupnja kupnja = new Kupnja();
        Prodaj prodaj = new Prodaj();

        List<PictureBox> slikica = new List<PictureBox>();
        List<Label> labelmarka = new List<Label>();
        List<Label> labelmodel = new List<Label>();
        List<Label> labelstanje = new List<Label>();
        List<Label> labelvrsta_motora = new List<Label>();
        List<Label> labelmjenjac = new List<Label>();
        List<Label> labelkilometraza = new List<Label>();
        List<Label> labelgodiste = new List<Label>();
        List<Label> labellokacija = new List<Label>();
        List<Label> labeljacina_motora = new List<Label>();
        List<Label> labelcijena = new List<Label>();
        List<Button> kupnjatipka = new List<Button>();


        public Form1()
        {
            InitializeComponent();

            menuStrip1.Items[0].Text = GlobalneVar.MyGlobalVariable;

            string connectionString = "server=davidrpi.ddns.net;port=3306;database=auti;user=Kranjcec;password=KranjcecJeKul!";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM modeli", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(new Car());
                            cars[cars.Count - 1].marka = (reader.GetString(0));
                            cars[cars.Count - 1].model = (reader.GetString(1));
                            cars[cars.Count - 1].stanje = (reader.GetInt32(2));
                            cars[cars.Count - 1].vrsta_motora = (reader.GetString(3));
                            cars[cars.Count - 1].mjenjac = (reader.GetInt32(4));
                            cars[cars.Count - 1].kilometraza = (reader.GetInt32(5));
                            cars[cars.Count - 1].godiste = (reader.GetInt32(6));
                            cars[cars.Count - 1].lokacija = (reader.GetString(7));
                            cars[cars.Count - 1].jacina_motora = (reader.GetInt32(8));
                            cars[cars.Count - 1].cijena = (reader.GetInt32(9));
                            cars[cars.Count - 1].imageData = ((byte[])reader.GetValue(10));
                            using (MemoryStream ms = new MemoryStream(cars[cars.Count - 1].imageData))
                            {
                                cars[cars.Count - 1].slika = Image.FromStream(ms);
                            }
                        }
                    }
                }
                connection.Close();
            }





            Panel panel = new Panel();
            panel.Width = 600;
            panel.Height = 486;
            panel.Location = new System.Drawing.Point(185, 12);
            panel.BackColor = Color.FromArgb(255, 193, 191, 181);
            panel.AutoScroll = true;

            this.Controls.Add(panel);
            for (int i = 0; i < cars.Count; i++)
            {
                panels.Add(new Panel());
                panels[i].Width = 560;
                panels[i].Height = 250;
                panels[i].Click += new EventHandler(Paneli_Click);
                panels[i].Location = new System.Drawing.Point(10, 10 + (i * (panels[i].Height + 10)));
                panels[i].BackColor = Color.FromArgb(255, 188, 150, 230);
                panel.Controls.Add(panels[i]);

                labelmarka.Add(new Label());
                labelmarka[i].Text = cars[i].marka;
                labelmarka[i].Font = new Font(labelmarka[i].Font, FontStyle.Bold);
                labelmarka[i].Location = new System.Drawing.Point(250, 10);
                panels[i].Controls.Add(labelmarka[i]);

                labelmodel.Add(new Label());
                labelmodel[i].Text = cars[i].model;
                labelmodel[i].Font = new Font(labelmodel[i].Font, FontStyle.Bold);
                labelmodel[i].Location = new System.Drawing.Point(250, labelmarka[i].Location.Y + labelmarka[i].Height);
                panels[i].Controls.Add(labelmodel[i]);

                labelstanje.Add(new Label());
                labelstanje[i].Width = 200;
                if (cars[i].stanje == 1)
                {
                    labelstanje[i].Text = "Stanje: rabljeno";
                }
                if (cars[i].stanje == 0)
                {
                    labelstanje[i].Text = "Stanje: novo";
                }
                labelstanje[i].Location = new System.Drawing.Point(250, labelmodel[i].Location.Y + labelmodel[i].Height);
                panels[i].Controls.Add(labelstanje[i]);

                labelvrsta_motora.Add(new Label());
                labelvrsta_motora[i].Width = 200;
                labelvrsta_motora[i].Text = "Vrsta motora: " + cars[i].vrsta_motora.ToLower();
                labelvrsta_motora[i].Location = new System.Drawing.Point(250, labelstanje[i].Location.Y + labelstanje[i].Height);
                panels[i].Controls.Add(labelvrsta_motora[i]);

                labelmjenjac.Add(new Label());
                labelmjenjac[i].Width = 300;
                if (cars[i].mjenjac == 1)
                {
                    labelmjenjac[i].Text = "Mjenjac: rucni";
                }
                if (cars[i].mjenjac == 0)
                {
                    labelmjenjac[i].Text = "Mjenjac: automatski";
                }
                labelmjenjac[i].Location = new System.Drawing.Point(250, labelvrsta_motora[i].Location.Y + labelvrsta_motora[i].Height);
                panels[i].Controls.Add(labelmjenjac[i]);

                labelkilometraza.Add(new Label());
                labelkilometraza[i].Width = 200;
                labelkilometraza[i].Text = "Kilometraza: " + cars[i].kilometraza.ToString();
                labelkilometraza[i].Location = new System.Drawing.Point(250, labelmjenjac[i].Location.Y + labelmjenjac[i].Height);
                panels[i].Controls.Add(labelkilometraza[i]);

                labelgodiste.Add(new Label());
                labelgodiste[i].Width = 200;
                labelgodiste[i].Text = "Godiste: " + cars[i].godiste.ToString();
                labelgodiste[i].Location = new System.Drawing.Point(250, labelkilometraza[i].Location.Y + labelkilometraza[i].Height);
                panels[i].Controls.Add(labelgodiste[i]);

                labellokacija.Add(new Label());
                labellokacija[i].Width = 200;
                labellokacija[i].Text = "Lokacija: " + cars[i].lokacija;
                labellokacija[i].Location = new System.Drawing.Point(250, labelgodiste[i].Location.Y + labelgodiste[i].Height);
                panels[i].Controls.Add(labellokacija[i]);

                labeljacina_motora.Add(new Label());
                labeljacina_motora[i].Width = 200;
                labeljacina_motora[i].Text = "Jacina motora: " + cars[i].jacina_motora.ToString() + "ks";
                labeljacina_motora[i].Location = new System.Drawing.Point(250, labellokacija[i].Location.Y + labellokacija[i].Height);
                panels[i].Controls.Add(labeljacina_motora[i]);

                labelcijena.Add(new Label());
                labelcijena[i].Width = 200;
                labelcijena[i].Text = "Cijena: " + cars[i].cijena.ToString() + "€";
                labelcijena[i].Location = new System.Drawing.Point(250, labeljacina_motora[i].Location.Y + labeljacina_motora[i].Height);
                panels[i].Controls.Add(labelcijena[i]);

                slikica.Add(new PictureBox());
                slikica[i].Width = 230;
                slikica[i].Height = 230;
                slikica[i].Image = cars[i].slika;
                slikica[i].SizeMode = PictureBoxSizeMode.StretchImage;
                slikica[i].Location = new System.Drawing.Point(10, 10);
                panels[i].Controls.Add(slikica[i]);

                kupnjatipka.Add(new Button());
                kupnjatipka[i].Width = 100;
                kupnjatipka[i].Text = "Kupi auto";
                kupnjatipka[i].Location = new System.Drawing.Point(350,10);
                panels[i].Controls.Add(kupnjatipka[i]);
                kupnjatipka[i].Click += new EventHandler(KupnjaTipka_Klik);
            }
        }
        private void Paneli_Click(object sender, EventArgs e)
        {
        }
        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private void odjavaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Closed += (s, args) => this.Close();
            prijava.Show();
        }
        void KupnjaTipka_Klik(object sender, EventArgs e)
        {
            Button button = (Button)sender; 
            Panel panel = (Panel)button.Parent; 
            int index = panels.IndexOf(panel);

            GlobalneVar.markakupnja = cars[index].marka;
            GlobalneVar.modelkupnja = cars[index].model;
            GlobalneVar.stanjekupnja = cars[index].stanje;
            GlobalneVar.vrsta_motorakupnja = cars[index].vrsta_motora;
            GlobalneVar.mjenjackupnja = cars[index].mjenjac;
            GlobalneVar.kilometrazakupnja = cars[index].kilometraza;
            GlobalneVar.godistekupnja = cars[index].godiste;
            GlobalneVar.lokacijakupnja = cars[index].lokacija;
            GlobalneVar.jacina_motorakupnja = cars[index].jacina_motora;
            GlobalneVar.cijenakupnja = cars[index].cijena;
            GlobalneVar.slikakupnja = cars[index].slika;

            this.Hide();
            kupnja.Closed += (s, args) => this.Close();
            kupnja.Show();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            prodaj.Closed += (s, args) => this.Close();
            prodaj.Show();
        }
    }
}
