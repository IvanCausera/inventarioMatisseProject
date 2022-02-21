using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace di.proyecto.clase.ribbon.MVVM {
    class MVUsuario : MVBaseCRUD<usuario>{
        // Definicion de variables *************************************
        private inventarioEntities invEnt;
        private usuario usu;
        private UsuarioServicio usuServ;
        private ListCollectionView list;
        private rol rolSelec;
        private departamento departamento;
        private grupo grupo;
        private string textFiltro;
        private string txtNombre;
        private int mesSel = 0;

        // Definicion criterios filtro *************************************
        private List<Predicate<usuario>> criterios;
        private Predicate<usuario> criterioRol;
        private Predicate<usuario> criterioNombre;
        private Predicate<usuario> criterioGrupo;

        // Constructor *************************************
        public MVUsuario(inventarioEntities invEnt) {
            this.invEnt = invEnt;

            usuServ = new UsuarioServicio(invEnt);
            servicio = usuServ;
            list = new ListCollectionView(usuServ.getAll().ToList());

            criterios = new List<Predicate<usuario>>();
            criterioRol = new Predicate<usuario>(usu => usu.rol1 != null && usu.rol1.Equals(rolSeleccionado));
            criterioNombre = new Predicate<usuario>(usu => (!string.IsNullOrEmpty(usu.nombre) && usu.nombre.ToUpper().StartsWith(textNombreApellido.ToUpper()))
                ||
                (!string.IsNullOrEmpty(usu.apellido1) && usu.apellido1.ToUpper().StartsWith(textNombreApellido.ToUpper())));
            criterioGrupo = new Predicate<usuario>(usu => usu.grupo1 == grupoSeleccionado);
        }

        // Definicion de variables publicas *************************************
        public usuario usuarioNuevo {
            get { return usu; }
            set { usu = value; NotifyPropertyChanged(nameof(usuarioNuevo)); }
        }

        public departamento dptoSeleccionado {
            get { return departamento; }
            set { departamento = value; NotifyPropertyChanged(nameof(dptoSeleccionado)); }
        }

        public grupo grupoSeleccionado {
            get { return grupo; }
            set { grupo = value; NotifyPropertyChanged(nameof(grupoSeleccionado)); }
        }

        public int mesSeleccionado {
            get { return mesSel; }
            set { mesSel = value; NotifyPropertyChanged(nameof(mesSeleccionado)); }
        }

        public string textoNombre {
            get { return txtNombre; }
            set { txtNombre = value; NotifyPropertyChanged(nameof(textoNombre)); }
        }

        public ListCollectionView listaUsuarios {
            get { return list; }
        }

        public List<departamento> listaDepartamentos {
            get { return new DptoServicio(invEnt).getAll().ToList(); }
        }

        public List<grupo> listaGrupos {
            get { return new GrupoServicio(invEnt).getAll().ToList(); }
        }

        public List<rol> listaRoles {
            get { return new ServicioGenerico<rol>(invEnt).getAll().ToList(); }
        }

        public List<tipousuario> listaTipoUsuarios {
            get { return new ServicioGenerico<tipousuario>(invEnt).getAll().ToList(); }
        }

        public List<int> listaMeses {
            get { return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }; }
        }

        public rol rolSeleccionado {
            get { return rolSelec; }
            set { rolSelec = value; NotifyPropertyChanged(nameof(rolSeleccionado)); }
        }

        public String textNombreApellido {
            get { return textFiltro; }
            set { textFiltro = value; NotifyPropertyChanged(nameof(textNombreApellido)); }
        }

        public bool guarda {
            get { return add(usu); }
        }

        public bool editar {
            get { return update(usu); }
        }

        public bool borrar {
            get { return delete(usu); }
        }

        public bool usuarioUnico {
            get { return usuServ.usuarioUnico(usu.username); }
        }

        public bool passwordValida {
            get { return validarPassword(usu.password); }
        }

        // Definicion de Metodos *************************************

        public void setPass(String pass) {
            usu.password = pass;
        }

        public bool filtroRol(object obj) {
            if (obj == null) return false;

            bool correct = false;
            usuario usu = (usuario)obj;

            if (usu.rol1 != null && usu.rol1.Equals(rolSeleccionado)) {
                correct = true;
            }

            return correct;
        }

        public bool filtroNombreApellido(object obj) {
            if (obj == null) return false;

            bool correct = false;
            usuario usu = (usuario)obj;

            if ((!string.IsNullOrEmpty(usu.nombre) && usu.nombre.ToUpper().StartsWith(textNombreApellido.ToUpper()))
                ||
                (!string.IsNullOrEmpty(usu.apellido1) && usu.apellido1.ToUpper().StartsWith(textNombreApellido.ToUpper()))) {
                correct = true;
            }

            return correct;
        }

        public bool filtroCombinadoCriterios(object obj) {
            if (obj == null) return false;
            bool correct = true;

            usuario user = (usuario)obj;
            if (criterios.Count() != 0) {
                correct = criterios.TrueForAll(x => x(user));
            }

            return correct;
        }

        public void addCriterios() {
            criterios.Clear();
            if (rolSeleccionado != null) {
                criterios.Add(criterioRol);
            }
            if (!string.IsNullOrEmpty(textNombreApellido)) {
                criterios.Add(criterioNombre);
            }
            if (grupoSeleccionado != null) {
                criterios.Add(criterioGrupo);
            }
        }

        public void borrarCriterios() {
            criterios.Clear();
        }

        private Boolean validarPassword(String pass) {
            if (pass.Length < 8) return false;

            int validConditions = 0;
            foreach (char c in pass) {
                if (c >= 'a' && c <= 'z') {
                    validConditions++;
                    break;
                } else if (c >= 'A' && c <= 'Z') {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in pass) {
                if (c >= '0' && c <= '9') {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '*', '/', '(', ')'};
            if (pass.IndexOfAny(special) == -1) return false;
            else
                return true;
        }
    }
}
