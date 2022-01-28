using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Servicios;
using MahApps.Metro.Controls;
using System;
using NLog;
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
using MahApps.Metro.Controls.Dialogs;

namespace di.proyecto.clase.ribbon.Frontend.Dialogos {
    /// <summary>
    /// Interaction logic for DialogoMVCAddArticulo.xaml
    /// </summary>
    public partial class DialogoMVCAddArticulo : MetroWindow {
        private inventarioEntities invEnt;
        private usuario usuLogin;
        private articulo articuloNuevo;
        private ArticuloServicio articuloServicio;
        private Brush borderOriginal;
        private Brush colorOriginal;
        private static Logger log = LogManager.GetCurrentClassLogger();
        public DialogoMVCAddArticulo(inventarioEntities invEnt, usuario user) {
            InitializeComponent();
            this.invEnt = invEnt;
            articuloServicio = new ArticuloServicio(invEnt);
            articuloNuevo = new articulo();
            dateFechaAlta.SelectedDate = DateTime.Now;
            this.usuLogin = user;

            borderOriginal = txtNumSerie.BorderBrush;
            colorOriginal = txtNumSerie.Foreground;

            comboUsuarioAlta.ItemsSource = new UsuarioServicio(invEnt).getAll().ToList();
            comboUsuarioAlta.SelectedItem = user;
            comboModelo.ItemsSource = new ModeloArticuloServicio(invEnt).getAll().ToList();
            comboEspacio.ItemsSource = new EspacioServicio(invEnt).getAll().ToList();
            comboDepartamento.ItemsSource = new DptoServicio(invEnt).getAll().ToList();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (valida()) {
                recogerDatos();
                articuloServicio.add(articuloNuevo);
                try {
                    articuloServicio.save();
                    await espera(2);
                    popCorrecto.IsOpen = false;
                }
                catch (Exception ex) {
                    log.Info("INSERTANDO UN OBJETO MODELO ARTICULO ...\n");
                    log.Error(ex.InnerException);
                    log.Error(ex.StackTrace);
                    await this.ShowMessageAsync("GESTION MODELO ARTICULO", "ERROR!! no se puede insertar en la base de datos");
                    //MessageBox.Show("ERROR!! no se puede insertar en la base de datos",
                    //"GESTION MODELO ARTICULO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task espera(int secs) {
            popCorrecto.IsOpen = true;
            await Task.Delay(secs * 1000);
        }

        private void recogerDatos() {
            articuloNuevo.idarticulo = articuloServicio.getLastId() + 1;
            articuloNuevo.numserie = txtNumSerie.Text;
            articuloNuevo.estado = txtEstado.Text;
            articuloNuevo.fechaalta = dateFechaAlta.SelectedDate;
            articuloNuevo.fechabaja = null;
            articuloNuevo.usuario = (usuario)comboUsuarioAlta.SelectedItem;
            articuloNuevo.usuario1 = null;
            articuloNuevo.modeloarticulo = (modeloarticulo)comboModelo.SelectedItem;
            articuloNuevo.departamento1 = (departamento)comboDepartamento.SelectedItem;
            articuloNuevo.espacio1 = (espacio)comboEspacio.SelectedItem;
            articuloNuevo.dentrode = null;
            articuloNuevo.observaciones = txtObservaciones.Text;
        }

        private Boolean valida() {
            Boolean correcto = true;
            if (comboModelo.SelectedItem == null) {
                correcto = false;
                resaltarError(comboModelo, "El campo no puede estar vacío");
            }
            if (String.IsNullOrEmpty(txtNumSerie.Text)) {
                correcto = false;
                resaltarError(txtNumSerie, "El campo no puede estar vacío");
            }
            if (comboEspacio.SelectedItem == null) {
                correcto = false;
                resaltarError(comboEspacio, "El campo no puede estar vacío");
            }
            if (comboDepartamento.SelectedItem == null) {
                correcto = false;
                resaltarError(comboDepartamento, "El campo no puede estar vacío");
            }
            if(DateTime.Now > dateFechaAlta.SelectedDate) {
                correcto = false;
                resaltarError(dateFechaAlta, "La fecha introducida debe ser posterior a la actual");
            }

            List<articulo> articuloList = articuloServicio.getAll().ToList();
            foreach (articulo articulo in articuloList) {
                if (articulo.numserie == txtNumSerie.Text) {
                    correcto = false;
                    resaltarError(txtNumSerie, "El número de serie ya existe");
                }
            }
            return correcto;
        }

        private void resaltarError(Control c, String error) {
            c.BorderBrush = Brushes.Red;
            //c.Foreground = Brushes.Red;
            c.ToolTip = error;
        }

        private void quitarError(Control c) {
            c.BorderBrush = borderOriginal;
            //c.Foreground = colorOriginal;
            c.ToolTip = null;
        }

        private void comboModelo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            quitarError(comboModelo);
        }

        private void txtNumSerie_TextChanged(object sender, TextChangedEventArgs e) {
            quitarError(txtNumSerie);
        }

        private void comboDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            quitarError(comboDepartamento);
        }

        private void comboEspacio_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            quitarError(comboEspacio);
        }

        private void dateFechaAlta_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            quitarError(dateFechaAlta);
        }
    }
}
