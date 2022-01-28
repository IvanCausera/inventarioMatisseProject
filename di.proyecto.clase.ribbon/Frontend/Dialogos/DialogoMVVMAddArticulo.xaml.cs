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
    /// Interaction logic for DialogoMVVMAddArticulo.xaml
    /// </summary>
    public partial class DialogoMVVMAddArticulo : MetroWindow {
        private inventarioEntities invEnt;
        private usuario user;
        private MVArticulo mArticulo;
        private Boolean editar;
        public DialogoMVVMAddArticulo(inventarioEntities ent, usuario usu) {
            InitializeComponent();
            this.invEnt = ent;
            this.user = usu;
            inicializa();
        }

        public DialogoMVVMAddArticulo(inventarioEntities ent, articulo art) {
            InitializeComponent();
            this.invEnt = ent;
            this.user = art.usuario;
            inicializa();
        }

        private void inicializa() {
            mArticulo = new MVArticulo(invEnt, user);
            DataContext = mArticulo;

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mArticulo.OnErrorEvent));
            mArticulo.btnGuardar = btnGuardar;
        }

        private void comboModelo_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void txtNumSerie_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void dateFechaAlta_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void comboDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void comboEspacio_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (!mArticulo.IsValid(this)) {
                await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                       "ERROR!!! Hay campos obligatorios");
            } else {
                if (mArticulo.guarda) {
                    await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                       "TODO CORRECTO!!! Objeto insertado correctamente");
                } else {
                    await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                       "ERROR!!! No se puede insertar el objeto");
                }
            }
        }
    }
}
