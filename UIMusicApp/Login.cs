using Application.Implementations;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UIMusicApp
{
    public partial class Login : Form
    {
        private readonly IAuthenticationService _authService;
       
        public Login()
        {

            InitializeComponent();
            var dbConnection = new Infrastructure.Connection();
            _authService = new AuthenticationService(dbConnection);
            this.txtContraseña.PasswordChar = '*';
            InitializeNotifyIcon();
        }
        private void InitializeNotifyIcon()
        {
            notifyIcon1 = new NotifyIcon();
            notifyIcon1.Icon = SystemIcons.Information; // Usa un ícono predeterminado del sistema o tu propio ícono
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = txtUsuario.Text;
            string password = txtContraseña.Text;

            try
            {
                int role = _authService.Authenticate(username, password);

                if (role == 1)
                {
                    ShowNotification("¡Bienvenido Administrador!");
                    Reproductor adminForm = new Reproductor(role);
                    adminForm.Show();
                    this.Hide();
                }
                else if (role == 2)
                {
                    ShowNotification("¡Bienvenido Usuario!");
                    Reproductor userForm = new Reproductor(role);
                    userForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }
        private void ShowNotification(string message)
        {
            notifyIcon1.BalloonTipTitle = "Notificación";
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.ShowBalloonTip(2000);
        }
    }
}
