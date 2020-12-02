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
    public partial class Cocinados : Form

    { 
        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Cocinado AS [Tipo de Cocinado] FROM Cocinados " +
            "ORDER BY Id";
    
        public Cocinados()
        {
            InitializeComponent();
        }

        private void Cocinados_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvCocinados.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarCocinados '{0}', '{1}'", txtCodigo.Text, txtCocinado.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvCocinados.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

                string borrado = string.Format("EXEC EliminaCocinado '{0}'", dgvCocinados.CurrentRow.Cells["Código"].Value);

                try
                {

                    BaseDeDatos.procesosSql(borrado);
                    dgvCocinados.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtCocinado.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtCocinado.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvCocinados.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarCocinados '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text, 
                    txtCocinado.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvCocinados.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtCocinado.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o categoría debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Cocinado AS [Tipo de Cocinado] FROM Cocinados" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvCocinados.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Cocinado AS [Tipo de Cocinado] FROM Cocinados" +
                         " WHERE Cocinado LIKE '%" + txtCocinado.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvCocinados.DataSource = datosRecibidos.Tables[0];

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
                    dgvCocinados.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }

        }

        private void dgvCocinados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtCodigo.Text = dgvCocinados.CurrentRow.Cells["Código"].Value.ToString();
            txtCocinado.Text = dgvCocinados.CurrentRow.Cells["Tipo de Cocinado"].Value.ToString();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
