using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChefTic.OtrasClases;
using ChefTic.BBDD;

namespace ChefTic.Formularios
{
    public partial class Comidas : Form
    {

        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Comida AS Comida FROM Comidas ORDER BY Id";

        public Comidas()
        {
            InitializeComponent();
        }

        private void Comidas_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvComidas.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarComidas '{0}', '{1}'", txtCodigo.Text, txtComida.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvComidas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }

                Limpiar();

            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "" && MessageBox.Show("¿Seguro que quieres borrar: " + txtCodigo.Text + "?",
                "Eliminar registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {

                string borrado = string.Format("EXEC EliminaComida '{0}'", dgvComidas.CurrentRow.Cells["Código"].Value);

                try
                {

                    BaseDeDatos.procesosSql(borrado);
                    dgvComidas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }

            }

        }

        private void Limpiar()
        {

            txtCodigo.Text = "";
            txtComida.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtComida.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvComidas.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarComidas '{0}', '{1}', '{2}'", 
                    Identificador, txtCodigo.Text, txtComida.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvComidas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }

            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            string cadenaBusqueda = "";

            if (btnBuscar.Text == "&Filtrar")
            {

                if (txtCodigo.Text == "" && txtComida.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o comida debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Comida AS Comida FROM Comidas" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvComidas.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Comida AS Comida FROM Comidas" +
                         " WHERE Comida LIKE '%" + txtComida.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvComidas.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }


                }

            }
            else
            {

                btnBuscar.Text = "&Filtrar";
                try
                {

                    DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                    dgvComidas.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }
        }

        private void dgvComidas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCodigo.Text = dgvComidas.CurrentRow.Cells["Código"].Value.ToString();
            txtComida.Text = dgvComidas.CurrentRow.Cells["Comida"].Value.ToString();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
