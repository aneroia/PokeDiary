using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace PokeDiary
{
    public partial class Form3 : Form
    {
        string nicknamek;
        public Form3(string nickname)
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, 255, 70, 70);
            label1.BackColor = Color.FromArgb(0, 0, 0, 0);
            RoundPanelCorners(panel1, 50);
            RoundPanelCorners(panel2, 50);
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


            string pokemonName = nameTextbox.Text.ToLower();
            pokemonName = pokemonName.ToLower();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pokeapi.co/api/v2/pokemon/" + pokemonName);
            

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
                Form4 frm4 = new Form4(nameTextbox.Text,nicknameLabel.Text);
                frm4.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    MessageBox.Show("This pokemon doesn't exist");
                }
            };
        }

        private void nameTextbox_TextChanged(object sender, EventArgs e)
        {
            if (nameTextbox.Text != "")
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            nicknameLabel.Text = nicknamek;
        }
    }
}
