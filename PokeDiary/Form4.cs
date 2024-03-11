using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PokeDiary
{
    public partial class Form4 : Form
    {
        string pokName;
        string nicknamek;
        public Form4(string name, string nickname)
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, 255, 70, 70);
            pokName = name;
            label1.BackColor = Color.FromArgb(0, 0, 0, 0);
            RoundPanelCorners(panel1, 50);
            RoundPanelCorners(panel3, 50);
            RoundButtonCorners(button1, 40);
            nicknamek = nickname;
        }

        private void RoundPanelCorners(Panel panel, int radius)
        {
            // Создаем путь для скругления углов
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.StartFigure();
            path.AddArc(panel.ClientRectangle.X, panel.ClientRectangle.Y, radius, radius, 180, 90);
            path.AddArc(panel.ClientRectangle.Width - radius, panel.ClientRectangle.Y, radius, radius, 270, 90);
            path.AddArc(panel.ClientRectangle.Width - radius, panel.ClientRectangle.Height - radius, radius, radius, 0, 90);
            path.AddArc(panel.ClientRectangle.X, panel.ClientRectangle.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            // Устанавливаем путь как регион для панели
            panel.Region = new System.Drawing.Region(path);
        }

        private void RoundButtonCorners(Button button, int radius)
        {
            // Создаем объект GraphicsPath для определения формы кнопки с закругленными углами
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.StartFigure();
            path.AddArc(button.ClientRectangle.X, button.ClientRectangle.Y, radius, radius, 180, 90);
            path.AddArc(button.ClientRectangle.Width - radius, button.ClientRectangle.Y, radius, radius, 270, 90);
            path.AddArc(button.ClientRectangle.Width - radius, button.ClientRectangle.Height - radius, radius, radius, 0, 90);
            path.AddArc(button.ClientRectangle.X, button.ClientRectangle.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            // Устанавливаем путь как регион для кнопки
            button.Region = new System.Drawing.Region(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3(nicknamek);
            frm3.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            pokemonName.Text = pokName;
        
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pokeapi.co/api/v2/pokemon/" + pokName.ToLower());

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);

            string data = sr.ReadToEnd();
            response.Close();

            dynamic d = JsonConvert.DeserializeObject<dynamic>(data);

            this.pokemonName.Text = d.name;

            for (int i = 0; i < d.types.Count; i++)
            {
                pokemonType.Text += " "+d.types[i].type.name + ",";
            }

            pokemonType.Text = pokemonType.Text.TrimEnd(',');

            for (int i = 0; i < d.abilities.Count; i++)
            {
                pokemonAbilities.Text += d.abilities[i].ability.name + '\n';
            }

            pictureBox1.ImageLocation = d.sprites.other.home.front_default;

            //stats
            pokemonHP.Text += d.stats[0].base_stat;
            pokemonAttack.Text += d.stats[1].base_stat;
            pokemonDefence.Text += d.stats[2].base_stat;
            pokemonSpeed.Text += d.stats[5].base_stat;





        }
    }
}
