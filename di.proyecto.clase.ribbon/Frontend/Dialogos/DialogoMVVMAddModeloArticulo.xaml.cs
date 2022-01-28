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
    /// Interaction logic for DialogoMVVMAddModeloArticulo.xaml
    /// </summary>
    public partial class DialogoMVVMAddModeloArticulo : MetroWindow {
        private inventarioEntities invEnt;
        private MVModeloArticulo mvModeloArt;
        private Boolean editar;
        public DialogoMVVMAddModeloArticulo(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            inicializa();
            editar = false;
            mvModeloArt.modeloNuevo = new modeloarticulo();
        }

        public DialogoMVVMAddModeloArticulo(inventarioEntities ent, modeloarticulo modelo) {
            InitializeComponent();
            invEnt = ent;
            inicializa();
            editar = true;
            btnGuardar.Content = "Editar";
            mvModeloArt.modeloNuevo = modelo;
        }

        private void inicializa() {
            mvModeloArt = new MVModeloArticulo(invEnt);
            DataContext = mvModeloArt;
            // Añadimos el manejador de eventos para los errores de validación
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvModeloArt.OnErrorEvent));
            // Asignamos el boton a la propiedad de la clase MV.
            // Este botón se habilitará o deshabilitará en función de si hay errores o no.
            mvModeloArt.btnGuardar = btnGuardar;
        }

        private void txtNombreModelo_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void cbTipoModelo_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            Boolean result;
            if (!mvModeloArt.IsValid(this)) {
                await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                       "ERROR!!! Cuidado hay campos obligatorios");
            } else {
                if (editar) {
                    result = mvModeloArt.editar;
                } else {
                    result = mvModeloArt.guarda;
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
