//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace di.proyecto.clase.ribbon.Backend.Modelo
{
    using di.proyecto.clase.ribbon.MVVM;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class usuario : PropertyChangedDataError {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.articulo = new HashSet<articulo>();
            this.articulo1 = new HashSet<articulo>();
            this.ficherousuario = new HashSet<ficherousuario>();
            this.salida = new HashSet<salida>();
        }
    

        public int idusuario { get; set; }

        [Required(ErrorMessage = "Campo Username obligatorio")]
        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string username { get; set; }

        [Required(ErrorMessage = "Campo Password obligatorio")]
        public string password { get; set; }
        public int tipo { get; set; }
        public int rol { get; set; }
        public string grupo { get; set; }
        public Nullable<int> departamento { get; set; }

        [Required(ErrorMessage = "Campo Nombre obligatorio")]
        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Campo Primer Apellido obligatorio")]
        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string apellido1 { get; set; }

        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string apellido2 { get; set; }

        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string domicilio { get; set; }

        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string poblacion { get; set; }

        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string codpostal { get; set; }

        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string email { get; set; }

        [MaxLength(ErrorMessage = "Campo Username no puede tener tantos carácteres")]
        public string telefono { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<articulo> articulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<articulo> articulo1 { get; set; }
        public virtual departamento departamento1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ficherousuario> ficherousuario { get; set; }
        public virtual grupo grupo1 { get; set; }

        [Required(ErrorMessage = "Campo Rol obligatorio")]
        public virtual rol rol1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<salida> salida { get; set; }

        [Required(ErrorMessage = "Campo Tipo Usuario obligatorio")]
        public virtual tipousuario tipousuario { get; set; }

        public override string ToString() {
            return nombre + " " + apellido1;
        }
    }

}
