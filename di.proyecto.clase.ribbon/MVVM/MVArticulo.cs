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
        private inventarioEntities invEnt;
        private articulo art;
        private ArticuloServicio articuloServicio;
        private ListCollectionView lista;
        private DateTime fechaIni;
        private DateTime fechaFin;
        private salida salidaSel;
        private ServicioGenerico<tipousuario> tipousuarioServicio;

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
        }

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

        public bool guarda {
            get { return valida(); }
        }

        public bool FiltroFecha(object obj) {
            if (obj == null) return false;

            bool correcto = false;
            articulo art = (articulo)obj;
            if (art.fechaalta > fechaInicial && art.fechaalta < fechaFinal) {
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

    }
}
