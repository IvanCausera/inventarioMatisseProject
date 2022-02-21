using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace di.proyecto.clase.ribbon.MVVM {
    class MVModeloArticulo : MVBaseCRUD<modeloarticulo>{
        // Definicion de variables ***********************
        private const int NUM_CORTE = 5;

        private inventarioEntities invEnt;
        private TipoArticuloServicio tipoServ;
        private ModeloArticuloServicio modServ;
        private modeloarticulo modArt;
        private ListCollectionView lista;
        private tipoarticulo tipoSelec;
        private String textFiltro;
        private bool chkMayor10;

        private List<Predicate<modeloarticulo>> criterios;
        private Predicate<modeloarticulo> criterioTipo;
        private Predicate<modeloarticulo> criterioNombre;
        private Predicate<modeloarticulo> criterioNumArt;
        // Constructor ***********************************
        public MVModeloArticulo(inventarioEntities ent) {
            invEnt = ent;
            inicializa();   
        }

        private void inicializa() {
            tipoServ = new TipoArticuloServicio(invEnt);
            modServ = new ModeloArticuloServicio(invEnt);
            servicio = modServ;
            lista = new ListCollectionView(modServ.getAll().ToList());
            criterios = new List<Predicate<modeloarticulo>>();
            criterioTipo = new Predicate<modeloarticulo>(m => m.tipoarticulo != null && m.tipoarticulo.Equals(tipoSeleccionado));
            criterioNombre = new Predicate<modeloarticulo>(m => (!string.IsNullOrEmpty(m.nombre) && m.nombre.ToUpper().StartsWith(textNombreMarca.ToUpper()))
                                                                    ||
                                                                (!string.IsNullOrEmpty(m.marca) && m.marca.ToUpper().StartsWith(textNombreMarca.ToUpper())));
            criterioNumArt = new Predicate<modeloarticulo>(m => m.articulo.Count >= 5);
        }

        //Getters and Setters ****************************
        public List<tipoarticulo> listaTipos { 
            get { return tipoServ.getAll().ToList(); }
        }

        public ListCollectionView listaModelos { 
            get { return lista; } 
        }

        public modeloarticulo modeloNuevo {
            get { return modArt; }
            set { modArt = value; NotifyPropertyChanged(nameof(modeloNuevo)); }
        }

        public tipoarticulo tipoSeleccionado {
            get { return tipoSelec; }
            set { tipoSelec = value; NotifyPropertyChanged(nameof(tipoSeleccionado)); }
        }

        public String textNombreMarca {
            get { return textFiltro; }
            set { textFiltro = value; NotifyPropertyChanged(nameof(textNombreMarca)); }
        }

        public Boolean masDe10Art {
            get { return chkMayor10; }
            set { chkMayor10 = value; NotifyPropertyChanged(nameof(masDe10Art)); }
        }

        public bool guarda {
            get { return add(modeloNuevo); }
        }

        public bool editar { get { return update(modeloNuevo); } }


        public bool filtroTipo(object obj) {
            
            if (obj == null) return false;

            bool correct = false;
            modeloarticulo modelo = (modeloarticulo)obj;

            if (modelo.tipoarticulo != null && modelo.tipoarticulo.Equals(tipoSeleccionado)) {
                correct = true;
            }

            return correct;
        }

        public bool filtroNombreMarca(object obj) {
            if (obj == null) return false;
            bool correct = true;
            modeloarticulo modelo = (modeloarticulo)obj;
            if ((string.IsNullOrEmpty(modelo.nombre) ||
                !modelo.nombre.ToUpper().StartsWith(textNombreMarca.ToUpper()))
                &&
                (string.IsNullOrEmpty(modelo.marca) ||
                !modelo.marca.ToUpper().StartsWith(textNombreMarca.ToUpper()))) {
                correct = false;
            }
            return correct;
        }

        public bool filtroCombinado(object obj) {
            if (obj == null) return false;
            bool correct = true;

            modeloarticulo modelo = (modeloarticulo)obj;
            if (
                (modelo.tipoarticulo == null || !modelo.tipoarticulo.Equals(tipoSeleccionado))
                
                &&

                ((string.IsNullOrEmpty(modelo.nombre) ||
                !modelo.nombre.ToUpper().StartsWith(textNombreMarca.ToUpper()))
                &&
                (string.IsNullOrEmpty(modelo.marca) ||
                !modelo.marca.ToUpper().StartsWith(textNombreMarca.ToUpper())))
                ) {
                correct = false;
            }

            return correct;
        }

        public bool filtroCombinadoCriterios(object obj) {
            if (obj == null) return false;
            bool correct = true;

            modeloarticulo modelo = (modeloarticulo)obj;
            if (criterios.Count() != 0) {
                correct = criterios.TrueForAll(x => x(modelo));
            }

            return correct;
        }

        public void addCriterios() {
            criterios.Clear(); // Importante limpiar lista antes de añadir
            if (tipoSeleccionado != null) {
                criterios.Add(criterioTipo);
            }
            if (!string.IsNullOrEmpty(textNombreMarca)) {
                criterios.Add(criterioNombre);
            }
            if (masDe10Art) {
                criterios.Add(criterioNumArt);
            }
        }

        public void borrarCriterios() {
            criterios.Clear();
        }
    }
}
