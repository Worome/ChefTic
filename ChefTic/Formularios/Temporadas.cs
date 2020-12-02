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
    public partial class Temporadas : Form
    {

        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Temporada AS [Temporada] FROM Temporadas " +
            "ORDER BY Id";

        public Temporadas()
        {
            InitializeComponent();
        }

        private void Temporadas_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvTemporadas.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {


            string insercion = string.Format("EXEC InsertarTemporadas '{0}', '{1}'", txtCodigo.Text, txtTemporada.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvTemporadas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

                string borrado = string.Format("EXEC EliminaTemporada '{0}'", dgvTemporadas.CurrentRow.Cells["Código"].Value);

                try
                {

                    BaseDeDatos.procesosSql(borrado);
                    dgvTemporadas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtTemporada.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtTemporada.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvTemporadas.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarTemporadas '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text,
                    txtTemporada.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvTemporadas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtTemporada.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o categoría debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Temporada AS Temporada FROM Temporadas" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvTemporadas.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Temporada AS Temporada FROM Temporadas" +
                         " WHERE Temporada LIKE '%" + txtTemporada.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvTemporadas.DataSource = datosRecibidos.Tables[0];

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
                    dgvTemporadas.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }

        }

        private void dgvTemporadas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            txtCodigo.Text = dgvTemporadas.CurrentRow.Cells["Código"].Value.ToString();
            txtTemporada.Text = dgvTemporadas.CurrentRow.Cells["Temporada"].Value.ToString();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
