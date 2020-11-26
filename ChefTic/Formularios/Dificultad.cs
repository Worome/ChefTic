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
    public partial class Dificultad : Form
    {

        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Dificultad AS [Dificultad] FROM Dificultad " +
          "ORDER BY Id";

        public Dificultad()
        {
            InitializeComponent();
        }

        private void Dificultad_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvDificultad.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarDificultad '{0}', '{1}'", txtCodigo.Text, txtDificultad.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvDificultad.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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


            string borrado = string.Format("EXEC EliminaDificultad '{0}'", dgvDificultad.CurrentRow.Cells["Código"].Value);

            try
            {

                BaseDeDatos.procesosSql(borrado);
                dgvDificultad.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
                Limpiar();

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void Limpiar()
        {

            txtCodigo.Text = "";
            txtDificultad.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtDificultad.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvDificultad.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarDificultades '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text,
                    txtDificultad.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvDificultad.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtDificultad.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o categoría debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Dificultad AS Dificultad FROM Dificultad" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvDificultad.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Dificultad AS Dificultad FROM Dificultad" +
                         " WHERE Dificultad LIKE '%" + txtDificultad.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvDificultad.DataSource = datosRecibidos.Tables[0];

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
                    dgvDificultad.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }

        }

        private void dgvDificultad_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtCodigo.Text = dgvDificultad.CurrentRow.Cells["Código"].Value.ToString();
            txtDificultad.Text = dgvDificultad.CurrentRow.Cells["Dificultad"].Value.ToString();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
