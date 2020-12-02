using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChefTic.BBDD;
using ChefTic.OtrasClases;

namespace ChefTic.Formularios
{
    public partial class Fuentes : Form
    {
        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Fuente AS Fuente FROM Fuentes ORDER BY Id";

        public Fuentes()
        {
            InitializeComponent();
        }

        private void Fuentes_Load(object sender, EventArgs e)
        {


            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvFuentes.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }


        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarFuentes '{0}', '{1}'", txtCodigo.Text, txtFuente.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvFuentes.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

                string borrado = string.Format("EXEC EliminaFuentes '{0}'", dgvFuentes.CurrentRow.Cells["Código"].Value);

                try
                {

                    BaseDeDatos.procesosSql(borrado);
                    dgvFuentes.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtFuente.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtFuente.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvFuentes.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarFuentes '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text, txtFuente.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvFuentes.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtFuente.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o fuente debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Fuente AS Fuente FROM Fuentes" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvFuentes.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Fuente AS Fuente FROM Fuentes" +
                         " WHERE Fuente LIKE '%" + txtFuente.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvFuentes.DataSource = datosRecibidos.Tables[0];

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
                    dgvFuentes.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }


        }

        private void dgvFuentes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCodigo.Text = dgvFuentes.CurrentRow.Cells["Código"].Value.ToString();
            txtFuente.Text = dgvFuentes.CurrentRow.Cells["Fuente"].Value.ToString();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
