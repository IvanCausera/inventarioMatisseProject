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

namespace di.proyecto.clase.ribbon.Frontend.ControlesUsuario
{
    /// <summary>
    /// Interaction logic for UCArbolEspacios.xaml
    /// </summary>
    public partial class UCArbolEspacios : UserControl{

        private inventarioEntities invEnt;
        private EspacioServicio espServ;

        public UCArbolEspacios(inventarioEntities ent){
            InitializeComponent();
            invEnt = ent;
            espServ = new EspacioServicio(invEnt);
            treeEspacio.ItemsSource = espServ.getAll().ToList();
        }

        private void treeEspacio_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e){
            if (treeEspacio.SelectedItem is articulo) {
                dgPrestamos.ItemsSource = ((articulo)treeEspacio.SelectedItem).salida;
            }
        }
    }
}
