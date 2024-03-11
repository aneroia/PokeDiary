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

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            LoadUserData();
            panel1.BackColor = Color.FromArgb(100, 255, 70, 70);
            label1.BackColor = Color.FromArgb(0, 255, 5, 0);
            label2.BackColor = Color.FromArgb(0, 255, 5, 0);
            label3.BackColor = Color.FromArgb(0, 255, 5, 0);
            linkLabel1.BackColor = Color.FromArgb(0, 255, 5, 0);
            button1.FlatAppearance.BorderColor = Color.Red;
            RoundPanelCorners(panel1, 50);
            RoundButtonCorners(button1, 40);
            loginTextbox.Padding = new Padding(10, 0, 0, 0);
        }

        private void LoadUserData()
        {
            try
            {
                /*string json = File.ReadAllText(usersFilePath);
                users = JsonConvert.DeserializeObject<List<User>>(json);*/
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл с данными пользователей не найден.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных пользователей: " + ex.Message);
                Close();
            }
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


        private void panel1_Paint(object sender, PaintEventArgs e)
        {/*
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //e.Graphics.Clear(panel1.Parent.BackColor);
            Control control = panel1;
            int radius = 30;
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddLine(radius, 0, control.Width - radius, 0);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddLine(control.Width, radius, control.Width, control.Height - radius);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddLine(control.Width - radius, control.Height, radius, control.Height);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.AddLine(0, control.Height - radius, 0, radius);
                path.AddArc(0, 0, radius, radius, 180, 90);
                using (SolidBrush brush = new SolidBrush(control.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }*/
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string path = "users.json";
            var data = File.ReadAllText(path);

            dynamic d = JsonConvert.DeserializeObject<dynamic>(data);

            string userLogin = loginTextbox.Text;
            string userPass = passwordTextbox.Text;

            for(int i =0; i<d.Count; i++)
            {
                if (d[i].Email == userLogin && d[i].Password == userPass)
                {
                    string nickname = d[i].Nickname;
                    Form3 frm3 = new Form3(nickname);
                    frm3.Show();
                    this.Hide();
                    break;
                }
                else
                {
                    MessageBox.Show("Wrong Email or password.");
                    break;

                }
            }

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
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
            if (!string.IsNullOrEmpty(loginTextbox.Text) && !string.IsNullOrEmpty(passwordTextbox.Text))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}