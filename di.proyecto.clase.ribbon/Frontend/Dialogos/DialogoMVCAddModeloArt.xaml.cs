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
    /// Interaction logic for DialogoMVCAddModeloArt.xaml
    /// </summary>
    public partial class DialogoMVCAddModeloArt : MetroWindow{
        private inventarioEntities invEnt;
        private TipoArticuloServicio tipoServ;
        private ModeloArticuloServicio modServ;
        private modeloarticulo modArtNuevo;
        private Brush borderOriginal;
        private Brush colorOriginal;
        private static Logger log = LogManager.GetCurrentClassLogger();
        public DialogoMVCAddModeloArt(inventarioEntities invEnt) {
            InitializeComponent();
            this.invEnt = invEnt;
            inicializar();
        }

        private void inicializar() {
            tipoServ = new TipoArticuloServicio(invEnt);
            modServ = new ModeloArticuloServicio(invEnt);
            modArtNuevo = new modeloarticulo();
            cbTipoModelo.ItemsSource = tipoServ.getAll().ToList();
            borderOriginal = txtNombreModelo.BorderBrush;
            colorOriginal = txtNombreModelo.Foreground;
        }

        private void recogerDatos() {
            modArtNuevo.nombre = txtNombreModelo.Text;
            modArtNuevo.marca = txtMarcaModelo.Text;
            modArtNuevo.modelo = txtModelo.Text;
            modArtNuevo.tipoarticulo = (tipoarticulo)cbTipoModelo.SelectedItem;
            modArtNuevo.descripcion = txtDescripcionModelo.Text;
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (valida()) {
                recogerDatos();
                modServ.add(modArtNuevo);
                try {
                    modServ.save();
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

        private void resaltarError(Control c, String error) {
            c.BorderBrush = Brushes.Red;
            c.Foreground = Brushes.Red;
            c.ToolTip = error;
        }

        private void quitarError(Control c) {
            c.BorderBrush = borderOriginal;
            c.Foreground = colorOriginal;
            c.ToolTip = null;
        }

        private bool valida() {
            bool correcto = true;
            if (String.IsNullOrEmpty(txtNombreModelo.Text)) {
                correcto = false;
                resaltarError(txtNombreModelo, "El campo no puede estar vacío");
            }
            if (String.IsNullOrEmpty(txtMarcaModelo.Text)) {
                correcto = false;
                resaltarError(txtMarcaModelo, "El campo no puede estar vacío");
            }
            if (cbTipoModelo.SelectedItem == null) {
                correcto = false;
                resaltarError(cbTipoModelo, "El campo no puede estar vacío");
            }
            return correcto;
        }

        private void txtNombreModelo_TextChanged(object sender, TextChangedEventArgs e) {
            quitarError(txtNombreModelo);
        }

        private void txtMarcaModelo_TextChanged(object sender, TextChangedEventArgs e) {
            quitarError(txtMarcaModelo);
        }

        private void cbTipoModelo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            quitarError(cbTipoModelo);
        }
    }
}
