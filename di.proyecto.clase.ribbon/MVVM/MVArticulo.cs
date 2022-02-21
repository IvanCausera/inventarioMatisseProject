using di.proyecto.clase.ribbon.Servicios;
using di.proyecto.clase.ribbon.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NLog;
using System.Windows.Data;

namespace di.proyecto.clase.ribbon.MVVM {
    class MVArticulo: MVBaseCRUD<articulo> {
        // Variables privadas ***************************************************************************************
        private inventarioEntities invEnt;
        private articulo art;
        private ArticuloServicio articuloServicio;
        private ListCollectionView lista;
        private DateTime fechaIni;
        private DateTime fechaFin;
        private salida salidaSel;
        private ServicioGenerico<tipousuario> tipousuarioServicio;
        private bool mas10;
        private List<Predicate<articulo>> criterios; //Filtros combinados
        private Predicate<articulo> criterioFechas; //Filtros individuales
        private Predicate<articulo> criterioMas10;

        // Constructor ***************************************************************************************
        public MVArticulo(inventarioEntities ent, usuario user) {
            this.invEnt = ent;
            articuloServicio = new ArticuloServicio(invEnt);
            servicio = articuloServicio;
            art = new articulo();
            art.idarticulo = articuloServicio.getLastId() + 1;
            art.usuario = user;
            lista = new ListCollectionView(articuloServicio.getAll().ToList());
            inicializa();
        }

        public MVArticulo(inventarioEntities ent) {
            this.invEnt = ent;
            articuloServicio = new ArticuloServicio(invEnt);
            servicio = articuloServicio;
            art = new articulo();
            art.idarticulo = articuloServicio.getLastId() + 1;
            lista = new ListCollectionView(articuloServicio.getAll().ToList());
            inicializa();
        }

        private void inicializa() {
            salidaSel = new salida();
            tipousuarioServicio = new ServicioGenerico<tipousuario>(invEnt);

            // Inicializamos los obetos para el filtrado de la tabla
            criterios = new List<Predicate<articulo>>();
            inicializaCriterios();
        }

        private void inicializaCriterios() { //Definicion criterios
            criterioFechas = new Predicate<articulo>(a => a.fechaalta >= fechaInicial && fechaFinal == a.fechaalta);
            criterioMas10 = new Predicate<articulo>(a => a.salida.Count > 20);
        }

        // Getters and Setters ***************************************************************************************

        /// <summary>
        /// Guarda la inicial final seleccionada en la interfaz
        /// </summary>
        public DateTime fechaInicial {
            get { return fechaIni; }
            set { fechaIni = value; NotifyPropertyChanged(nameof(fechaInicial)); }
        }
        /// <summary>
        /// Guarda la fecha final seleccionada en la interfaz
        /// </summary>
        public DateTime fechaFinal {
            get { return fechaFin; }
            set { fechaFin = value; NotifyPropertyChanged(nameof(fechaFin)); }
        }

        public articulo articuloNuevo {
            get { return art; }
            set { art = value; NotifyPropertyChanged(nameof(articuloNuevo)); }
        }

        public List<usuario> listaUsuarios {
            get { return new UsuarioServicio(invEnt).getAll().ToList(); }
        }

        public ListCollectionView listaArticulos {
            get { return lista; }
        }

        public List<modeloarticulo> listaModeloArticulos {
            get { return new ModeloArticuloServicio(invEnt).getAll().ToList(); }
        }

        public List<espacio> listaEspacios {
            get { return new EspacioServicio(invEnt).getAll().ToList(); }
        }

        public List<departamento> listaDepartamentos {
            get { return new DptoServicio(invEnt).getAll().ToList(); }
        }

        public salida salidaSeleccionada {
            get { return salidaSel; }
            set { salidaSel = value; NotifyPropertyChanged(nameof(salidaSeleccionada)); }
        }

        public List<tipousuario> listaTiposUsuario {
            get { return tipousuarioServicio.getAll().ToList(); }
        }

        public bool checkMas10 {
            get { return mas10; }
            set { mas10 = value; NotifyPropertyChanged(nameof(checkMas10)); }
        }

        public bool guarda {
            get { return valida(); }
        }

        // Metodos ***************************************************************************************

        public bool FiltroFecha(object obj) {
            if (obj == null) return false;

            bool correcto = false;
            articulo art = (articulo)obj;
            if (art.fechaalta >= fechaInicial && fechaFinal <= fechaFinal) {
                correcto = true;
            }
            return correcto;
        }

        public bool FiltroPrestamos(object obj) {
            if (obj == null) return false;

            bool correcto = false;
            articulo art = (articulo)obj;
            if (art.salida.Count > 10) {
                correcto = true;
            }
            return correcto;
        }

        private Boolean valida() {
            Boolean correcto = true;
            if (art.modeloarticulo == null) {
                correcto = false;
            }
            if (String.IsNullOrEmpty(art.numserie)) {
                correcto = false;
            }
            if (art.espacio1 == null) {
                correcto = false;
            }
            if (art.departamento1 == null) {
                correcto = false;
            }
            if (DateTime.Now > art.fechaalta) {
                correcto = false;
            }
            if (!articuloServicio.numserieUnico(art.numserie)) {
                correcto = false;
            }


            if (correcto) {
                return add(art);
            }
            return correcto;
        }

        public void addCriterios() {
            criterios.Clear(); //Importante limpiar lista

            criterios.Add(criterioFechas);

            if (checkMas10) {
                criterios.Add(criterioMas10);
            }
        }

        public bool FiltroCombinadoCriterios(object obj) {
            if (obj == null) return false;

            bool correct = true;
            articulo art = (articulo)obj;
            if (criterios.Count() != 0) {
                correct = criterios.TrueForAll(x => x(art));
            }

            return correct;
        }
    }
}
