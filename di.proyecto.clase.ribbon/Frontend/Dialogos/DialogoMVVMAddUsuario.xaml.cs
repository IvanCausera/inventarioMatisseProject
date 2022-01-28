using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.MVVM;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for DialogoMVVMAddUsuario.xaml
    /// </summary>
    public partial class DialogoMVVMAddUsuario : MetroWindow {
        private inventarioEntities invEnt;
        private MVUsuario mvUsuario;
        private Boolean editar;
        public DialogoMVVMAddUsuario(inventarioEntities invEnt) {
            InitializeComponent();
            this.invEnt = invEnt;
            inicializa();
            editar = false;
            mvUsuario.usuarioNuevo = new usuario();
        }

        public DialogoMVVMAddUsuario(inventarioEntities invEnt, usuario user) {
            InitializeComponent();
            this.invEnt = invEnt;
            inicializa();
            editar = true;
            btnGuardar.Content = "Editar";
            mvUsuario.usuarioNuevo = user;
            passPassword.Password = user.password;
        }

        private void inicializa() {
            mvUsuario = new MVUsuario(invEnt);
            DataContext = mvUsuario;

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvUsuario.OnErrorEvent));
            mvUsuario.btnGuardar = btnGuardar;
        }


        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void passPassword_PasswordChanged(object sender, RoutedEventArgs e) {

        }

        private void comboTipo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tipousuario userTipo = (tipousuario)comboTipo.SelectedItem;
            if (userTipo.idtipousuario == 1) {
                comboDepartamento.IsEnabled = true;
                comboGrupo.IsEnabled = false;
                comboGrupo.SelectedItem = null;
            } else {
                comboDepartamento.SelectedItem = null;
                comboDepartamento.IsEnabled = false;
                comboGrupo.IsEnabled = true;
            }
        }

        private void comboRol_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void txtApellido1_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(passPassword.Password)) {
                await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                           "ERROR!!! La contraseña no puede estar vacia");
            } else {
                mvUsuario.setPass(passPassword.Password);
                if (!mvUsuario.passwordValida) {
                    passPassword.Focus();
                    await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                           "ERROR!!! La contraseña tiene que contener: Nº, letra, alfanumérico");
                } else {
                    Boolean result;
                    if (editar) {
                        result = mvUsuario.editar;
                    } else {
                        result = mvUsuario.guarda;
                    }

                    if (result) {
                        await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                                   "TODO CORRECTO!!! Objeto guardado correctamente");
                        DialogResult = true;
                    } else {
                        await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                                   "ERROR!!! No se puede guardar el objeto");
                    }
                }
            }
        }
    }
}
