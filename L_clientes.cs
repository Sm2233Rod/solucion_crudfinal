using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using solucion_crud.modeloaddup;
namespace solucion_crud.logica
{
    public class L_clientes

    {
        public DataTable listado_clientes()
        {
            SQLiteDataReader resultado;
            DataTable Tabla = new DataTable();
            SQLiteConnection SqlCon = new SQLiteConnection();

            try
            {
                SqlCon = conexion.getInstance().CrearConexion();
                string Sql_basededatos = "select * from clientes";
                SQLiteCommand Comando = new SQLiteCommand(Sql_basededatos, SqlCon);
                SqlCon.Open();
                resultado = Comando.ExecuteReader();
                Tabla.Load(resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        } //es la coneccion para ver los datos en en dg


        public string Guardar_clientes(int nopcion, R_clientes oCi) //es para actualizar o agregar al cliente
        {
            string Rpta = "";
            string Sql_basededatos = "";

            SQLiteConnection SqlCon = new SQLiteConnection();

            try
            {
                SqlCon = conexion.getInstance().CrearConexion();

                if (nopcion==1)//nuevo registro
                {
                    Sql_basededatos = "insert into clientes (nombrecliente,apellidocliente,telefono,ciudad)"+
                       "values ('"+oCi.nombrecliente+"','"+oCi.apellidocliente+"','"+oCi.telefono+"','"+oCi.ciudad+"')";
                }
                 else if(nopcion == 2) //actualizar registro
                {
                 Sql_basededatos = "update clientes set nombrecliente='"+oCi.nombrecliente+"', " +
                 "apellidocliente='"+oCi.apellidocliente+"', " +
                 "telefono='"+oCi.telefono+"', " +
                 "ciudad='"+oCi.ciudad+"' " +
                 "where idcliente='"+oCi.idcliente+"' ";
                 }

                  else if(nopcion == 3) //eliminar cliente
                 {
                  Sql_basededatos = "Delete from clientes where idcliente= '" + oCi.idcliente + "' "; 

                  }
                                Console.WriteLine("Consulta SQL: " + Sql_basededatos);

                

                SQLiteCommand Comando = new SQLiteCommand(Sql_basededatos, SqlCon);
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery()>=1 ? "ok":"el registro no se completo correctamente";
                return Rpta;
            }
            catch (Exception ex)
            {
                return Rpta = ex.Message;
            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public int ObtenerIdCliente(string nombrecliente, string apellidocliente)
        {
            int idcliente = -1; // Valor por defecto si no se encuentra el cliente

            SQLiteConnection SqlCon = new SQLiteConnection();

            try
            {
                SqlCon = conexion.getInstance().CrearConexion();
                string Sql_basededatos = "SELECT idcliente FROM clientes WHERE nombrecliente = @NombreCliente AND apellidocliente = @ApellidoCliente";
                SQLiteCommand Comando = new SQLiteCommand(Sql_basededatos, SqlCon);
                Comando.Parameters.AddWithValue("@NombreCliente", nombrecliente);
                Comando.Parameters.AddWithValue("@ApellidoCliente", apellidocliente);

                SqlCon.Open();
                object result = Comando.ExecuteScalar();
                if (result != null)
                {
                    idcliente = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return idcliente;
        }
    }
}
