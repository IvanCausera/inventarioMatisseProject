using di.proyecto.clase.ribbon.inventarioDataSetTableAdapters;
using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for UCReportViewer.xaml
    /// </summary>
    public partial class UCReportViewer : UserControl {
        public UCReportViewer() {
            InitializeComponent();
            ReportViewer_Load();
        }

        private void ReportViewer_Load() {
            // Comprobamos si el informe ya está cargado

            // Definimos la fuente de los datos a los que accederá el informe
            ReportDataSource reportDSJInventario = new ReportDataSource();
            inventarioDataSet dataset = new inventarioDataSet();
            // Iniciamos la configuración del DataSet
            dataset.BeginInit();
            // Damos los valores de la fuente de datos que hemos utilizado al configurar el
            reportDSJInventario.Name = "DSInventario";
            reportDSJInventario.Value = dataset.modeloarticulo;
            rvModeloArticulo.LocalReport.DataSources.Add(reportDSJInventario);
            // Indicamos la ruta del informe
            rvModeloArticulo.LocalReport.ReportPath = "../../Informes/RModeloArticulo.rdlc";
            dataset.EndInit();
            // Configuramos el TableAdapter que se encarga de rellenar los datos
            // Aquí es donde configuramos la consulta SQL
            modeloarticuloTableAdapter modeloTableAdapter = new modeloarticuloTableAdapter();
            modeloTableAdapter.ClearBeforeFill = true;
            // Podemos tener varias consultas asociadas al mismo Table Adapter y cada una se
            // representa por un nombre
            modeloTableAdapter.Fill(dataset.modeloarticulo);
            // Refrescamos la visualización del informe
            rvModeloArticulo.RefreshReport();
        }
    }
}
