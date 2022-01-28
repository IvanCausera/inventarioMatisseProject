using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using di.proyecto.clase.ribbon.Backend.Modelo;

namespace di.proyecto.clase.ribbon.Servicios{
    /*
     * Clase que contiene las reglas de negocio propias de la tabla usuario
     */
    public class UsuarioServicio : ServicioGenerico<usuario>{
        private DbContext contexto;
        private const int PROFESOR = 1;
        /*
         * Se almacena el usuario que ha iniciado sesión
         */
        public usuario usuLogin { get; set; }
        /*
         * Constructor 
         */
        public UsuarioServicio(DbContext context) : base(context){
            contexto = context;
        }
        /*
         * Método que comprueba las credenciales del usuario en la base de datos
         */
        public Boolean login(String user, String pass){
            Boolean correcto = true;
            try{
                usuLogin = contexto.Set<usuario>().Where(u => u.username == user).FirstOrDefault();
            } catch (Exception e){
                System.Console.WriteLine(e.StackTrace);
            }
            if(usuLogin == null){
                correcto = false;
            } else if (!usuLogin.username.Equals(user) || !usuLogin.password.Equals(pass)){
                correcto = false;
            }
            
            return correcto;
        }       
        /*
         * Comprueba si en la base de datos existe un usuario con ese login
         * El login de un usuario debe de ser único
         */
        public Boolean usuarioUnico(string usu){
            bool unico = true;
            if(contexto.Set<usuario>().Where(u => u.username == usu).Count() > 0){
                unico = false;
            }
            return unico;
        }
        /*
         * Devuelve un usuario en función del username pasado
         */
        public usuario getUsuarioPorNombre(string nom){
            usuario usu;
            usu = contexto.Set<usuario>().Where(u => u.username == nom).FirstOrDefault();
            return usu;
        }

        public List<usuario> getProfesores(){
            return contexto.Set<usuario>().Where(u => u.tipo == PROFESOR).ToList();
        }
    }
}