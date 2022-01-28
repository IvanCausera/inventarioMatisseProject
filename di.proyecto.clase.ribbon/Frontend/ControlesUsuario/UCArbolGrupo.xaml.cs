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
    /// <summary>
    /// Interaction logic for UCArbolGrupo.xaml
    /// </summary>
    public partial class UCArbolGrupo : UserControl {
        private inventarioEntities invEnt;
        private GrupoServicio grupServ;
        public UCArbolGrupo(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            grupServ = new GrupoServicio(invEnt);
            treeGrupo.ItemsSource = grupServ.getAll().ToList();
        }

        private void treeGrupo_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (treeGrupo.SelectedItem is usuario) {
                dgPrestamos.ItemsSource = ((usuario)treeGrupo.SelectedItem).salida;
            }
        }
    }
}
