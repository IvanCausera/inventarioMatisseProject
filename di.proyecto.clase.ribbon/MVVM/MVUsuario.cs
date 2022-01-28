﻿using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace di.proyecto.clase.ribbon.MVVM {
    class MVUsuario : MVBaseCRUD<usuario>{
        // Definicion de variables ***********************
        private inventarioEntities invEnt;
        private usuario usu;
        private UsuarioServicio usuServ;
        private ListCollectionView list;
        private rol rolSelec;
        private string textFiltro;

        private List<Predicate<usuario>> criterios;
        private Predicate<usuario> criterioRol;
        private Predicate<usuario> criterioNombre;

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
        }

        public usuario usuarioNuevo {
            get { return usu; }
            set { usu = value; NotifyPropertyChanged(nameof(usuarioNuevo)); }
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
