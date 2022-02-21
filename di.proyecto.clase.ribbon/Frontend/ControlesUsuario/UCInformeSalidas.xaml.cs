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
    /// Interaction logic for UCInformeSalidas.xaml
    /// </summary>
    public partial class UCInformeSalidas : UserControl {

        private inventarioEntities invEnt;
        private MVUsuario mvUsu;

        private string SQL_QUERY;
        private List<MySqlParameter> parametros;

        public UCInformeSalidas(inventarioEntities ent) {
            InitializeComponent();
            invEnt = ent;
            mvUsu = new MVUsuario(invEnt);
            DataContext = mvUsu;
        }

        public void CargarInforme() {
            // Creamos un nuevo objeto Documento
            ReportDocument rd = new ReportDocument();
            // Indicamos la ruta del informe
            rd.Load("../../Informes/CRPrestamo.rpt");
            // Obtenemos el servicio asociado
            ServicioSQL sqlServ = new ServicioSQL();
            
            cargaParametros();

            // Rellenamos la fuente de datos del informe con los datos
            // que obtenemos del servicio SQL mediante el método getDatos
            // al cual le pasamos la sentencia SQL
            rd.SetDataSource(sqlServ.getDatos(SQL_QUERY, parametros));
            // Rellenamos los datos del informe
            crvInfomeSalidas.ViewerCore.ReportSource = rd;
        }

        private void cargaParametros() {
            SQL_QUERY = "SELECT m.nombre as modeloarticulo, a.numserie, u.nombre, u.apellido1, s.fechasalida, s.fechadevolucion " +
                    "FROM salida s inner join articulo a on s.articulo = a.idarticulo " +
                    "inner join modeloarticulo m on a.modelo = m.idmodeloarticulo " +
                    "inner join usuario u on s.usuario = u.idusuario " +
                    "where 1 = 1";
            parametros = new List<MySqlParameter>();

            if (mvUsu.usuarioNuevo != null) {
                MySqlParameter paramUsu = new MySqlParameter();
                paramUsu.ParameterName = "usu";

                paramUsu.Value = ((usuario)mvUsu.usuarioNuevo).idusuario;
                SQL_QUERY += " AND u.idusuario = @usu";
                parametros.Add(paramUsu);
            }

            if (mvUsu.mesSeleccionado != 0) {
                MySqlParameter paramMes = new MySqlParameter();
                paramMes.ParameterName = "mes";

                paramMes.Value = mvUsu.mesSeleccionado;
                SQL_QUERY += " AND MONTH(s.fechasalida) = @mes";
                parametros.Add(paramMes);
            }

        }

        private void btnGenerarInforme_Click(object sender, RoutedEventArgs e) {
            CargarInforme();
        }

        private void btnBorrarFiltros_Click(object sender, RoutedEventArgs e) {
            mvUsu.usuarioNuevo = null;

            comboMes.SelectedItem = null;
            mvUsu.mesSeleccionado = 0;
        }
    }
}
