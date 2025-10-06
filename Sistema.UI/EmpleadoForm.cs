using Sistema.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI
{
    public partial class EmpleadoForm : Form
    {
        private EmpleadoBLL empleadoBLL = new EmpleadoBLL();
        private int empleadoSeleccionadoId = 0;
        public EmpleadoForm()
        {
            InitializeComponent();
            CargarEmpleados();
        }
        //crear el metodo de CargarEmpleados
        private void CargarEmpleados()
        {
            dgvEmpleados.DataSource = empleadoBLL.ListarEmpleados();
            dgvEmpleados.Columns["Foto"].Visible = false; // Ocultar la columna de foto
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxFoto.Image = Image.FromFile(ofd.FileName);
            }
        }

        //PARA DAR FUNCIONALIDAD A LA CARGAR E IMAGEN
        private byte[] ImagenToByteArray()
        {
            if(pictureBoxFoto.Image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                pictureBoxFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                string cargo = txtCargo.Text;
                byte[] foto = ImagenToByteArray();

                empleadoBLL.RegistrarEmpleado(nombre, cargo, foto);
                MessageBox.Show("Empleado registrado correctamente");
                //LimpiarFormularios();
                CargarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (empleadoSeleccionadoId == 0)
                {
                    MessageBox.Show("Seleccione un empleado para actualizar.");
                    return;
                }

                string nombre = txtNombre.Text.Trim();
                string cargo = txtCargo.Text.Trim();
                byte[] foto = ImagenToByteArray();

                empleadoBLL.ActualizarEmpleado(empleadoSeleccionadoId, nombre, cargo, foto);
                MessageBox.Show("Empleado actualizado correctamente.");
                LimpiarFormulario();
                CargarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (empleadoSeleccionadoId == 0)
                {
                    MessageBox.Show("Seleccione un empleado para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Está seguro de eliminar este empleado?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    empleadoBLL.EliminarEmpleado(empleadoSeleccionadoId);
                    MessageBox.Show("Empleado eliminado correctamente.");
                    LimpiarFormulario();
                    CargarEmpleados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dgvEmpleados.Rows[e.RowIndex];
                empleadoSeleccionadoId = Convert.ToInt32(fila.Cells["Id"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtCargo.Text = fila.Cells["Cargo"].Value.ToString();
                pictureBoxFoto.Image = null; // Puedes cargar la imagen si lo deseas
            }

        }
        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtCargo.Clear();
            pictureBoxFoto.Image = null;
            empleadoSeleccionadoId = 0;
        }

    }
}
