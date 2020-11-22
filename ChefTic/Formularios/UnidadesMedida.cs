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
    public partial class UnidadesMedida : Form
    {
        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Unidad AS [Unidad de Medida] FROM UnidadMedida ORDER BY Id";
       
        public UnidadesMedida()
        {
            InitializeComponent();
        }

        private void UnidadesMedida_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvMedidas.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarMedidad '{0}', '{1}'", txtCodigo.Text, txtUnidadMedida.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvMedidas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

            string borrado = string.Format("EXEC EliminaMedidas '{0}'", dgvMedidas.CurrentRow.Cells["Código"].Value);

            try
            {

                BaseDeDatos.procesosSql(borrado);
                dgvMedidas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtUnidadMedida.Text = "";
            txtCodigo.Focus();

        }

 
        private void btnModificar_Click(object sender, EventArgs e)
        {

           if (txtCodigo.Text != "" && txtUnidadMedida.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvMedidas.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarMedidas '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text, txtUnidadMedida.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvMedidas.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtUnidadMedida.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o unidad de medida debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Unidad AS [Unidad de Medida] FROM UnidadMedida" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvMedidas.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Unidad AS [Unidad de Medida] FROM UnidadMedida" +
                         " WHERE Unidad LIKE '%" + txtUnidadMedida.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvMedidas.DataSource = datosRecibidos.Tables[0];

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
                    dgvMedidas.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }


        }

        private void dgvMedidas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
            txtCodigo.Text = dgvMedidas.CurrentRow.Cells["Código"].Value.ToString();
            txtUnidadMedida.Text = dgvMedidas.CurrentRow.Cells["Unidad de Medida"].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
