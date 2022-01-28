using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Servicios;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for DialogoMVCAddUsuario.xaml
    /// </summary>
    public partial class DialogoMVCAddUsuario : MetroWindow {
        private inventarioEntities invEnt;
        private Brush borderOriginal;
        private usuario usuarioNuevo;
        private UsuarioServicio usuServ;
        private static Logger log = LogManager.GetCurrentClassLogger();
        public DialogoMVCAddUsuario(inventarioEntities invEnt) {
            InitializeComponent();
            this.invEnt = invEnt;

            borderOriginal = txtNombre.BorderBrush;

            usuarioNuevo = new usuario();
            usuServ = new UsuarioServicio(invEnt);

            comboDepartamento.ItemsSource = new DptoServicio(invEnt).getAll().ToList();
            comboGrupo.ItemsSource = new GrupoServicio(invEnt).getAll().ToList();
            comboRol.ItemsSource = new ServicioGenerico<rol>(invEnt).getAll().ToList();
            comboTipo.ItemsSource = new ServicioGenerico<tipousuario>(invEnt).getAll().ToList();
        }

        private void recogerDatos() {
            usuarioNuevo.username = txtUsername.Text;
            usuarioNuevo.password = passPassword.Password;
            usuarioNuevo.tipousuario = (tipousuario)comboTipo.SelectedItem;
            usuarioNuevo.rol1 = (rol)comboRol.SelectedItem;
            usuarioNuevo.grupo1 = (grupo)comboGrupo.SelectedItem;
            usuarioNuevo.departamento1 = (departamento)comboDepartamento.SelectedItem;
            usuarioNuevo.nombre = txtNombre.Text;
            usuarioNuevo.apellido1 = txtApellido1.Text;
            usuarioNuevo.apellido2 = txtApellido2.Text;
            usuarioNuevo.domicilio = txtDomicilio.Text;
            usuarioNuevo.poblacion = txtPoblacion.Text;
            usuarioNuevo.codpostal = txtCodpostal.Text;
            usuarioNuevo.email = txtEmail.Text;
            usuarioNuevo.telefono = txtTelefono.Text;
        }

        private Boolean valida() {
            Boolean correcto = true;
            if (String.IsNullOrEmpty(txtNombre.Text)) {
                correcto = false;
                resaltarError(txtNombre, "El campo no puede estar vacío");
            }
            if (String.IsNullOrEmpty(txtApellido1.Text)) {
                correcto = false;
                resaltarError(txtApellido1, "El campo no puede estar vacío");
            }

            if (String.IsNullOrEmpty(txtUsername.Text)) {
                correcto = false;
                resaltarError(txtUsername, "El campo no puede estar vacío");
            } else if (!usuServ.usuarioUnico(txtUsername.Text)) {
                correcto = false;
                resaltarError(txtUsername, "El nombre de usuario ya existe");
            }


            if (String.IsNullOrEmpty(passPassword.Password)) {
                correcto = false;
                resaltarError(passPassword, "El campo no puede estar vacío");
            }
            if (comboTipo.SelectedItem == null) {
                correcto = false;
                resaltarError(comboTipo, "El campo no puede estar vacío");
            }
            if (comboRol.SelectedItem == null) {
                correcto = false;
                resaltarError(comboRol, "El campo no puede estar vacío");
            }
            return correcto;
        }
        private void quitarError(Control c) {
            c.BorderBrush = borderOriginal;
            c.ToolTip = null;
        }

        private void resaltarError(Control c, String error) {
            c.BorderBrush = Brushes.Red;
            c.ToolTip = error;
        }

        private async Task espera(int secs) {
            popCorrecto.IsOpen = true;
            await Task.Delay(secs * 1000);
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (valida()) {
                recogerDatos();
                usuServ.add(usuarioNuevo);
                try {
                    usuServ.save();
                    await espera(2);
                    popCorrecto.IsOpen = false;
                }
                catch (Exception ex) {
                    log.Info("INSERTANDO UN OBJETO MODELO ARTICULO ...\n");
                    log.Error(ex.InnerException);
                    log.Error(ex.StackTrace);
                    await this.ShowMessageAsync("GESTION MODELO ARTICULO", "ERROR!! no se puede insertar en la base de datos");
                }
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e) {
            quitarError(txtUsername);
        }

        private void passPassword_PasswordChanged(object sender, RoutedEventArgs e) {
            quitarError(passPassword);
        }

        private void comboTipo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            quitarError(comboTipo);
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

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e) {
            quitarError(txtNombre);
        }

        private void txtApellido1_TextChanged(object sender, TextChangedEventArgs e) {
            quitarError(txtApellido1);
        }

        private void comboRol_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            quitarError(comboRol);
        }
    }
}
