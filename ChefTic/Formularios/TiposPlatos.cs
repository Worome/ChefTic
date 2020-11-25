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
    public partial class TiposPlatos : Form
    {

        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Plato AS [Tipo de Plato] FROM TiposPlato ORDER BY Id";
        public TiposPlatos()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarTiposPlatos '{0}', '{1}'", txtCodigo.Text, txtTipoPlato.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvTiposPlatos.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }

                Limpiar();

            }


        }

        private void TiposPlatos_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvTiposPlatos.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text != "" && txtTipoPlato.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvTiposPlatos.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarTiposPlatos '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text, 
                    txtTipoPlato.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvTiposPlatos.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }

            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            string borrado = string.Format("EXEC EliminaTiposPlatos '{0}'", dgvTiposPlatos.CurrentRow.Cells["Código"].Value);

            try
            {

                BaseDeDatos.procesosSql(borrado);
                dgvTiposPlatos.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtTipoPlato.Text = "";
            txtCodigo.Focus();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cadenaBusqueda = "";

            if (btnBuscar.Text == "&Filtrar")
            {

                if (txtCodigo.Text == "" && txtTipoPlato.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o categoría debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Plato AS [Tipo de Plato] FROM TiposPlato" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvTiposPlatos.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Plato AS [Tipo de Plato] FROM TiposPlato" +
                         " WHERE Plato LIKE '%" + txtTipoPlato.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvTiposPlatos.DataSource = datosRecibidos.Tables[0];

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
                    dgvTiposPlatos.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }
        }

        private void dgvTiposPlatos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtCodigo.Text = dgvTiposPlatos.CurrentRow.Cells["Código"].Value.ToString();
            txtTipoPlato.Text = dgvTiposPlatos.CurrentRow.Cells["Tipo de Plato"].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
