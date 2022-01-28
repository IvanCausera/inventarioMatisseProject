using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Servicios;
using MahApps.Metro.Controls;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace di.proyecto.clase.ribbon.Frontend.Dialogos {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow{
        private static Logger log = LogManager.GetCurrentClassLogger();
        private inventarioEntities invEnt;
        private UsuarioServicio usuServ;
        public Login() {
            InitializeComponent();
            if (!conectar()) {
                MessageBox.Show("ERROR!!! NO SE PUEDE CONECTAR CON LA BASE DE DATOS",
                "CONEXION BASE DE DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            usuServ = new UsuarioServicio(invEnt);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            if (String.IsNullOrEmpty(txtUserName.Text) || String.IsNullOrEmpty(passPassword.Password)) {
                MessageBox.Show("Es necesario establecer el usuario y la contraseña",
                "FALTAN CAMPOS", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if (usuServ.login(txtUserName.Text, passPassword.Password)) {
                MainWindow ventanaPrincipal = new MainWindow(invEnt, usuServ.usuLogin);
                ventanaPrincipal.Show();
                this.Close();
            } else {
                MessageBox.Show("Usuario o contraseña incorrectos",
                "CAMPOS INCORRECTOS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Boolean conectar() {
            Boolean conectar = true;
            invEnt = new inventarioEntities();
            try {
                invEnt.Database.Connection.Open();
            } catch (Exception ex) { 
                conectar = false;
                log.Info("Conexion con base de datos fallida");
                log.Error(ex.InnerException);
                log.Error(ex.StackTrace);
            }
            return conectar;
        }

    }
}