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
    /// Interaction logic for UCModeloArticulo.xaml
    /// </summary>
    public partial class UCModeloArticulo : UserControl {
        private inventarioEntities invEnt;
        //private ModeloArticuloServicio modServ;
        private MVModeloArticulo mvModelo;
        private PropertyGroupDescription groupMarca;
        private Predicate<object> prepModeloArticulo;
        public UCModeloArticulo(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            inicializa();
        }

        private void inicializa() {
            //modServ = new ModeloArticuloServicio(invEnt);
            //dgModeloArticulo.ItemsSource = modServ.getAll().ToList();

            mvModelo = new MVModeloArticulo(invEnt);
            DataContext = mvModelo;
            groupMarca = new PropertyGroupDescription("marca");
            prepModeloArticulo = new Predicate<object>(mvModelo.filtroCombinadoCriterios);
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            DialogoMVVMAddModeloArticulo diag = new DialogoMVVMAddModeloArticulo(invEnt, mvModelo.modeloNuevo);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgModeloArticulo.Items.Refresh();
            }
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {

        }

        private void checkMarca_Checked(object sender, RoutedEventArgs e) {
            mvModelo.listaModelos.GroupDescriptions.Add(groupMarca);
        }

        private void checkMarca_Unchecked(object sender, RoutedEventArgs e) {
            //mvModelo.listaModelos.GroupDescriptions.Clear();
            mvModelo.listaModelos.GroupDescriptions.Remove(groupMarca);
        }

        private void btnCierraSeleccion_Click(object sender, RoutedEventArgs e) {
            dgModeloArticulo.SelectedIndex = -1;
        }

        private void comboFiltroTipoArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            mvModelo.addCriterios();
            mvModelo.listaModelos.Filter = prepModeloArticulo;
        }

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e) {
            mvModelo.addCriterios();
            mvModelo.listaModelos.Filter = prepModeloArticulo;
        }

        private void btnQuitFiltros_Click(object sender, RoutedEventArgs e) {
            comboFiltroTipoArticulo.SelectedItem = null;
            comboFiltroTipoArticulo.Text = "Seleciona un tipo de articulo";
            txtFiltro.Text = null;
            mvModelo.borrarCriterios();
            checkNumArticulos.IsChecked = false;

            mvModelo.listaModelos.Filter = null;
        }

        private void btnFiltroCombinado_Click(object sender, RoutedEventArgs e) {
            mvModelo.addCriterios();
            mvModelo.listaModelos.Filter = prepModeloArticulo;
        }

        private void checkNumArticulos_Checked(object sender, RoutedEventArgs e) {
            mvModelo.addCriterios();
            mvModelo.listaModelos.Filter = prepModeloArticulo;
        }

        private void checkNumArticulos_Unchecked(object sender, RoutedEventArgs e) {
            mvModelo.addCriterios();
            mvModelo.listaModelos.Filter = prepModeloArticulo;
        }
    }
}
