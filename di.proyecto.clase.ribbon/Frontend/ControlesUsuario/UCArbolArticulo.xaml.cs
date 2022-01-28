using di.proyecto.clase.ribbon.Backend.Modelo;
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
    public partial class UCArbolArticulo : UserControl {

        private inventarioEntities invEnt;
        private TipoArticuloServicio tipoServ;
        public UCArbolArticulo(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            tipoServ = new TipoArticuloServicio(invEnt);
            treeArticulo.ItemsSource = tipoServ.getAll().ToList();
        }

        private void treeArticulo_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (treeArticulo.SelectedItem is articulo) {
                dgPrestamos.ItemsSource = ((articulo)treeArticulo.SelectedItem).salida;
            }
        }
    }
}
