using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokeDiary
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, 255, 70, 70);
            label1.BackColor = Color.FromArgb(0, 0, 0, 0);
            label2.BackColor = Color.FromArgb(0, 0, 0, 0);
            label3.BackColor = Color.FromArgb(0, 0, 0, 0);
            label4.BackColor = Color.FromArgb(0, 0, 0, 0);
            linkLabel1.BackColor = Color.FromArgb(0, 0, 0, 0);
            RoundPanelCorners(panel1, 50);
            RoundButtonCorners(button1, 40);
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
            var allUsers = readFile();
            int count = 0;

            foreach (var user in allUsers)
            {
                if (user.Nickname == nicknameTextbox.Text || user.Email == loginTextbox.Text)
                {
                    count += 1;
                    if (user.Email == loginTextbox.Text)
                    {
                        MessageBox.Show("This Email has already used");
                        break;
                    }

                    else if (user.Nickname == nicknameTextbox.Text)
                    {
                        MessageBox.Show("This nickname has already used");
                        break;
                    }
                }
            }

            if(count == 0)
            {
                MessageBox.Show("Account created!");
                User newUser = new User(loginTextbox.Text, nicknameTextbox.Text, passwordTextbox.Text);
                saveFile(newUser);
                Form3 frm3 = new Form3(nicknameTextbox.Text);
                frm3.Show();
                this.Hide();
            }

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void nicknameTextbox_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void loginTextbox_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void passwordTextbox_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            if (!string.IsNullOrEmpty(loginTextbox.Text) && !string.IsNullOrEmpty(passwordTextbox.Text) && !string.IsNullOrEmpty(nicknameTextbox.Text))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        const string filePath = "users.json";

        static void saveFile(User user)
        {
            List<User> allUser = readFile();
            allUser.Add(user);

            string serealizedUsers = JsonConvert.SerializeObject(allUser);

            File.WriteAllText(filePath, serealizedUsers);
        }

        static List<User> readFile()
        {
            string json = File.ReadAllText(filePath);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

            return users;
        }

        class User
        {
            public string Email { get; set; }
            public string Nickname { get; set; }
            public string Password { get; set; }

            public User(string login, string name, string pass)
            {
                Email = login;
                Nickname = name;
                Password = pass;
            }
        }
    }
}
