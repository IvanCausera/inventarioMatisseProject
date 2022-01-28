using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using di.proyecto.clase.ribbon.Backend.Modelo;

namespace di.proyecto.clase.ribbon.Servicios{
    /*
     * Clase que contiene las reglas de negocio de la clase Articulo
     */
    public class ArticuloServicio : ServicioGenerico<articulo>{
        private DbContext contexto;

        /*
         * Constructor
         */
        public ArticuloServicio(DbContext context) : base(context){
            contexto = context;
        }

        /*
         * Obtiene el último id de la tabla articulo
         * La clave primaria no es autoincrementable y debemos gestionarla nosotros
         */
        public int getLastId(){
            articulo art = contexto.Set<articulo>().OrderByDescending(a => a.idarticulo).FirstOrDefault();
            return art.idarticulo;
        }
        /*
         * Devuelve true en caso de que el número de serie no se encuentre en la base de datos
         * Devuelve false en caso de que el número de serie ya exista
         */
        public bool numserieUnico(string serie){
            bool unico = true;
            if (contexto.Set<articulo>().Where(a => a.numserie == serie).Count() > 0){
                unico = false;
            }
            return unico;
        }
    }
}
