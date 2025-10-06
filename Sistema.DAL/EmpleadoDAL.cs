using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.DAL
{
    public class EmpleadoDAL
    {
        //Insertar el procedimiento almacenado de empleado
        public void Insertar(string nombre, string cargo, byte[] foto)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Cargo", cargo);
                cmd.Parameters.AddWithValue("@Foto", foto);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Actualizar empelado
        public void Actualizar(int id, string nombre, string cargo, byte[] foto)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Cargo", cargo);
                //cmd.Parameters.AddWithValue("@Foto", foto);
                cmd.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = (object)foto ?? DBNull.Value;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Eliminar empleado
        public void Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Listar empleados
        public List<Empleado> Listar()
        {
            List<Empleado> lista = new List<Empleado>();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ListarEmpleados", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Empleado {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        Cargo = dr["Cargo"].ToString(),
                        Foto = dr["Foto"] as byte[]
                    });
                }
            }
            return lista;
        }
    }
}
