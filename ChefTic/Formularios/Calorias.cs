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
    public partial class Calorias : Form
    {
        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Calorias AS Calorías FROM Calorias ORDER BY Id";

        public Calorias()
        {
            InitializeComponent();
        }

        private void dgvCalorias_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            txtCodigo.Text = dgvCalorias.CurrentRow.Cells["Código"].Value.ToString();
            txtCaloria.Text = dgvCalorias.CurrentRow.Cells["Calorías"].Value.ToString();

        }

        private void Calorias_Load(object sender, EventArgs e)
        {

            try
            {
                
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvCalorias.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarCalorias '{0}', '{1}'", txtCodigo.Text, txtCaloria.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvCalorias.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

                string borrado = string.Format("EXEC EliminaCaloria '{0}'", dgvCalorias.CurrentRow.Cells["Código"].Value);

                try
                {

                    BaseDeDatos.procesosSql(borrado);
                    dgvCalorias.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtCaloria.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtCaloria.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvCalorias.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarCalorias '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text, 
                    txtCaloria.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvCalorias.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtCaloria.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o categoría debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Calorias AS Calorías FROM Calorias" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvCalorias.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Calorias AS Calorías FROM Calorias" +
                         " WHERE Calorias LIKE '%" + txtCaloria.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvCalorias.DataSource = datosRecibidos.Tables[0];

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
                    dgvCalorias.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
