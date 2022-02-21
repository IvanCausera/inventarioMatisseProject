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
    /// Interaction logic for UCArbolGrupo.xaml
    /// </summary>
    public partial class UCArbolGrupo : UserControl {
        private inventarioEntities invEnt;
        //private GrupoServicio grupServ;
        private MVUsuario mvUsu;
        public UCArbolGrupo(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            //grupServ = new GrupoServicio(invEnt);
            mvUsu = new MVUsuario(invEnt);
            DataContext = mvUsu;
            //treeGrupo.ItemsSource = grupServ.getAll().ToList();
        }

        private void treeGrupo_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (treeGrupo.SelectedItem is usuario) {
                dgPrestamos.ItemsSource = ((usuario)treeGrupo.SelectedItem).salida;
            }
        }

        private async void treeGrupo_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            if (treeGrupo.SelectedItem != null && treeGrupo.SelectedItem is usuario) {
                DialogoMVVMAddUsuario diag = new DialogoMVVMAddUsuario(invEnt, ((usuario)treeGrupo.SelectedItem));
                diag.ShowDialog();
            } else {
                //MessageBox.Show("CUIDADO!! mis comidas preferidas todas fallecidas", "MI BOCADILLO HA' PALMADO", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("CUIDADO!! No has seleccionado un usuario","GESTIÓN USUARIOS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
