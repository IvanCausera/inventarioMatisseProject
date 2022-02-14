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
using Fluent;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using di.proyecto.clase.ribbon.Servicios;
using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Frontend.Dialogos;
using di.proyecto.clase.ribbon.Frontend.ControlesUsuario;

namespace di.proyecto.clase.ribbon {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        private inventarioEntities invEnt;
        private usuario usuLogin;
        public MainWindow(inventarioEntities invEnt, usuario user) {
            InitializeComponent();
            this.invEnt = invEnt;
            this.usuLogin = user;
            tbUserLogin.Text = usuLogin.nombre;
        }

        private void Desconectar_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void rbtnAddModeloArticulo_Click(object sender, RoutedEventArgs e) {
            DialogoMVCAddModeloArt diag = new DialogoMVCAddModeloArt(invEnt);
            diag.ShowDialog();
        }

        private void rbtnAddArticulo_Click(object sender, RoutedEventArgs e) {
            DialogoMVCAddArticulo diag = new DialogoMVCAddArticulo(invEnt, usuLogin);
            diag.ShowDialog();
        }

        private void rbtnAddUsuario_Click(object sender, RoutedEventArgs e) {
            DialogoMVCAddUsuario diag = new DialogoMVCAddUsuario(invEnt);
            diag.ShowDialog();
        }

        private void rbtnAddMVVMModeloArticulo_Click(object sender, RoutedEventArgs e) {
            DialogoMVVMAddModeloArticulo diag = new DialogoMVVMAddModeloArticulo(invEnt);
            diag.ShowDialog();
        }

        private void rbtnAddMVArticulo_Click(object sender, RoutedEventArgs e) {
            DialogoMVVMAddArticulo diag = new DialogoMVVMAddArticulo(invEnt, usuLogin);
            diag.ShowDialog();
        }

        private void rbtnMVVMAddUsuario_Click(object sender, RoutedEventArgs e) {
            DialogoMVVMAddUsuario diag = new DialogoMVVMAddUsuario(invEnt);
            diag.ShowDialog();
        }

        private void rbtnListaModeloArticulo_Click(object sender, RoutedEventArgs e) {
            UCModeloArticulo uc = new UCModeloArticulo(invEnt);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        private void rbtnListaArticulo_Click(object sender, RoutedEventArgs e) {
            UCArticulo uc = new UCArticulo(invEnt);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        private void rbtnListaUsuario_Click(object sender, RoutedEventArgs e) {
            UCUsuario uc = new UCUsuario(invEnt);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        private void rbtnTipoArticulo_Click(object sender, RoutedEventArgs e) {
            UCArbolArticulo uc = new UCArbolArticulo(invEnt);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        private void rbtnArbolGrupo_Click(object sender, RoutedEventArgs e) {
            UCArbolGrupo uc = new UCArbolGrupo(invEnt);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        private void rbtnEspacios_Click(object sender, RoutedEventArgs e){
            UCArbolEspacios uc = new UCArbolEspacios(invEnt);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        private void rbtnInformes_Click(object sender, RoutedEventArgs e) {
            UCReportViewer uc = new UCReportViewer();
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        private void rbtnCrystal_Click(object sender, RoutedEventArgs e) {
            UCInformeSalidas uc = new UCInformeSalidas();
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }
    }
}
