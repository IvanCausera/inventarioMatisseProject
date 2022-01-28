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
    /// Interaction logic for UCUsuario.xaml
    /// </summary>
    public partial class UCUsuario : UserControl {
        private inventarioEntities invEnt;
        private UsuarioServicio modUsu;
        private MVUsuario mvUsuario;
        private Predicate<object> predUsuario;
        public UCUsuario(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            inicializa();
        }

        private void inicializa() {
            mvUsuario = new MVUsuario(invEnt);
            DataContext = mvUsuario;
            modUsu = new UsuarioServicio(invEnt);
            predUsuario = new Predicate<object>(mvUsuario.filtroCombinadoCriterios);
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            DialogoMVVMAddUsuario diag = new DialogoMVVMAddUsuario(invEnt, mvUsuario.usuarioNuevo);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgUsuario.Items.Refresh();
            }
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            if (mvUsuario.borrar) {
                dgUsuario.Items.Refresh();
            }
        }

        private void comboFiltroRol_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            mvUsuario.addCriterios();
            mvUsuario.listaUsuarios.Filter = predUsuario;
        }

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e) {
            mvUsuario.addCriterios();
            mvUsuario.listaUsuarios.Filter = predUsuario;
        }
    }
}
