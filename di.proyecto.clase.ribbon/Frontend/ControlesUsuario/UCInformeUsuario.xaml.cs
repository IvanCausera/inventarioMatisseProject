using CrystalDecisions.CrystalReports.Engine;
using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Backend.Servicios;
using di.proyecto.clase.ribbon.MVVM;
using MySql.Data.MySqlClient;
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
    /// Interaction logic for UCInformeUsuario.xaml
    /// </summary>
    public partial class UCInformeUsuario : UserControl {
        
        private inventarioEntities invEnt;
        private MVUsuario mvUsu;
        private string SQL_QUERY;
        private List<MySqlParameter> parametros;
        public UCInformeUsuario(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            mvUsu = new MVUsuario(invEnt);
            DataContext = mvUsu;
        }

        public void CargarInforme() {

            // Creamos un nuevo objeto Documento
            ReportDocument rd = new ReportDocument();
            // Indicamos la ruta del informe
            rd.Load("../../Informes/CPUsuario.rpt");
            // Obtenemos el servicio asociado
            ServicioSQL sqlServ = new ServicioSQL();

            cargaParametros();
            // Rellenamos la fuente de datos del informe con los datos
            // que obtenemos del servicio SQL mediante el método getDatos
            // al cual le pasamos la sentencia SQL
            rd.SetDataSource(sqlServ.getDatos(SQL_QUERY, parametros));

            // Rellenamos los datos del informe
            crvInfomeUsuarios.ViewerCore.ReportSource = rd;

            /*try {
                
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }*/
        }

        private void cargaParametros() {
            SQL_QUERY = "Select u.nombre, u.apellido1, u.username, u.grupo, d.nombre as nombredpto, t.nombre as tipousuario, r.nombre as rolusuario " +
            "from usuario u " +
            "left join departamento d on u.departamento = d.iddepartamento " +
            "inner join tipousuario t on u.tipo = t.idtipousuario " +
            "inner join rol r on u.rol = r.idrol " +
            "where 1 = 1";
            parametros = new List<MySqlParameter>();

            MySqlParameter paramDpto = new MySqlParameter();
            if (mvUsu.dptoSeleccionado != null) {
                // Nombramos el parámetro
                paramDpto.ParameterName = "dpto";
                // Asignamos el valor del parámetro
                paramDpto.Value = ((departamento)comboDepartamento.SelectedItem).iddepartamento;

                SQL_QUERY += " AND u.departamento = @dpto";
                parametros.Add(paramDpto);
            }

            MySqlParameter paramNombre = new MySqlParameter();
            if (!string.IsNullOrEmpty(mvUsu.textoNombre)) {
                // Nombramos el parámetro
                paramNombre.ParameterName = "nom";
                // Asignamos el valor del parámetro
                paramNombre.Value = mvUsu.textoNombre;

                SQL_QUERY += " AND u.nombre like @nom";
                parametros.Add(paramNombre);
            }
        }

        private void btnGenerarInforme_Click(object sender, RoutedEventArgs e) {
            CargarInforme();
        }

        private void btnBorrarFiltros_Click(object sender, RoutedEventArgs e) {
            mvUsu.dptoSeleccionado = null;
            mvUsu.textoNombre = null;
        }
    }
}
