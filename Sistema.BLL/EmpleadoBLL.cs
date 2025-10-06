using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.DAL;

namespace Sistema.BLL
{
    public class EmpleadoBLL
    {
        private EmpleadoDAL dal = new EmpleadoDAL();

        //Registrar empleado
        public void RegistrarEmpleado(string nombre, string cargo, byte[]foto)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre es obligatorio");
            if (string.IsNullOrWhiteSpace(cargo))
                throw new Exception("El cargo es obligatorio");
            dal.Insertar(nombre, cargo, foto);
        }

        //Actualizar empleado
        public void ActualizarEmpleado(int id, string nombre, string cargo, byte[]foto)
        {
            if (id <=0)
                throw new Exception("El ID es inválido");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre es obligatorio");
            if(string.IsNullOrWhiteSpace(cargo))
                throw new Exception("El cargo es obligatorio");
            dal.Actualizar(id, nombre, cargo, foto);
        }

        //Eliminar empleado
        public void EliminarEmpleado(int id)
        {
            if (id <= 0)
                throw new Exception("El ID es inválido");
            dal.Eliminar(id);
        }
    }
}
