using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using di.proyecto.clase.ribbon.Backend.Modelo;

namespace di.proyecto.clase.ribbon.Servicios{
    public class GrupoServicio : ServicioGenerico<grupo>{
        public GrupoServicio(DbContext context) : base(context){
        }
    }
}
