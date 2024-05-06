using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace solucion_crud.logica
{
    public class conexion
    {
        private String Basedatos;
        private static conexion Con = null;

        private conexion()
        {
            this.Basedatos = "./base de datos";
        }

        public SQLiteConnection CrearConexion()
        {
            SQLiteConnection Cadena = new SQLiteConnection();
                try
            {
                Cadena.ConnectionString = "Data Source=" + this.Basedatos;
            }
            catch (Exception ex)
            {

                Cadena = null;
                throw ex;
            }

            return Cadena;
        }

        public static conexion getInstance() 
        {
            if (Con==null)
            {
                Con = new conexion();
            }
            
            return Con;
        }
    }
}
