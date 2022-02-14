using CrystalDecisions.CrystalReports.Engine;
using di.proyecto.clase.ribbon.Backend.Servicios;
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
        public UCInformeSalidas() {
            InitializeComponent();
            CargarInforme();
        }

        public void CargarInforme() {
            try {
                // Creamos un nuevo objeto Documento
                ReportDocument rd = new ReportDocument();
                // Indicamos la ruta del informe
                rd.Load("../../Informes/CRPrestamo.rpt");
                // Obtenemos el servicio asociado
                ServicioSQL sqlServ = new ServicioSQL();
                // Rellenamos la fuente de datos del informe con los datos
                // que obtenemos del servicio SQL mediante el método getDatos
                // al cual le pasamos la sentencia SQL
                rd.SetDataSource(sqlServ.getDatos("SELECT m.nombre as modeloarticulo, a.numserie, u.nombre, u.apellido1, s.fechasalida, s.fechadevolucion " +
                    "FROM salida s " +
                    "inner join articulo a on s.articulo = a.idarticulo " +
                    "inner join modeloarticulo m on a.modelo = m.idmodeloarticulo " +
                    "inner join usuario u on s.usuario = u.idusuario"));
                // Rellenamos los datos del informe
                crvInfomeSalidas.ViewerCore.ReportSource = rd;
            } catch (Exception ex) {
                System.Console.WriteLine(ex.StackTrace);
                System.Console.WriteLine(ex.InnerException);
            }
        }
    }
}
