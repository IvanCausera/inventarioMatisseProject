using System.Data.Entity;
using di.proyecto.clase.ribbon.Backend.Modelo;
using di.proyecto.clase.ribbon.Servicios;

namespace di.proyecto.clase.ribbon.Servicios{
    public class DptoServicio : ServicioGenerico<departamento>{

        public DptoServicio(DbContext context) : base(context){
        }
    }
}
