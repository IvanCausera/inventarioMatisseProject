using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Frontend.Dialogos;
using di.proyecto.clase.ribbon.MVVM;
using di.proyecto.clase.ribbon.Servicios;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase.ribbon.Frontend.ControlesUsuario {
    /// <summary>
    /// Interaction logic for UCArticulo.xaml
    /// </summary>
    public partial class UCArticulo : UserControl {
        private inventarioEntities invEnt;
        private ArticuloServicio modArt;
        private MVArticulo mvArticulo;
        public UCArticulo(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            inicializa();
        }
        private void inicializa() {
            modArt = new ArticuloServicio(invEnt);
            dgArticulo.ItemsSource = modArt.getAll().ToList();
            mvArticulo = new MVArticulo(invEnt, new usuario());
            DataContext = mvArticulo;
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            DialogoMVVMAddArticulo diag = new DialogoMVVMAddArticulo(invEnt, mvArticulo.articuloNuevo);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgArticulo.Items.Refresh();
            }
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {

        }

        private void btnFiltrarFechas_Click(object sender, RoutedEventArgs e) {
            if (mvArticulo.fechaInicial < mvArticulo.fechaFinal) {
                mvArticulo.listaArticulos.Filter = new Predicate<object>(mvArticulo.FiltroFecha);
            }
            
        }
    }
}
