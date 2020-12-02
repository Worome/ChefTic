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
    public partial class Categorias : Form
    {
        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Categoria AS Categoría FROM Categorias ORDER BY Id";

        public Categorias()
        {
            InitializeComponent();
        }

        private void Categorias_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvCategorias.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarCategorias '{0}', '{1}'", txtCodigo.Text, txtCategoria.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvCategorias.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

                string borrado = string.Format("EXEC EliminaCategoria '{0}'", dgvCategorias.CurrentRow.Cells["Código"].Value);

                try
                {

                    BaseDeDatos.procesosSql(borrado);
                    dgvCategorias.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtCategoria.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtCategoria.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvCategorias.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarCategorias '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text, txtCategoria.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvCategorias.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtCategoria.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o categoría debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Categoria AS Categoría FROM Categorias" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvCategorias.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Categoria AS Categoría FROM Categorias" +
                         " WHERE Categoria LIKE '%" + txtCategoria.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvCategorias.DataSource = datosRecibidos.Tables[0];

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
                    dgvCategorias.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }

        }

        private void dgvCategorias_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtCodigo.Text = dgvCategorias.CurrentRow.Cells["Código"].Value.ToString();
            txtCategoria.Text = dgvCategorias.CurrentRow.Cells["Categoría"].Value.ToString();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
