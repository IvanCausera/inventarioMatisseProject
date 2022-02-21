using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.MVVM;
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
    /// Lógica de interacción para UCUsuarioExamen.xaml
    /// </summary>
    public partial class UCUsuarioExamen : UserControl {
        private inventarioEntities invEnt;
        private MVUsuario mvUsuario;
        private Predicate<object> predUser;

        public UCUsuarioExamen(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            inicializa();
        }

        public void inicializa() {
            mvUsuario = new MVUsuario(invEnt);
            DataContext = mvUsuario;
            predUser = new Predicate<object>(mvUsuario.filtroCombinadoCriterios);
        }

        private void tboxFiltroNombre_TextChanged(object sender, TextChangedEventArgs e) {
            Filtrar();
        }

        private void cbFiltroGrupo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Filtrar();
        }

        private void Filtrar() {
            mvUsuario.addCriterios();
            mvUsuario.listaUsuarios.Filter = predUser;
        }
    }
}
