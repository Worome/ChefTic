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
    public partial class Periodos : Form
    {

        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Periodo AS Periodo FROM Periodos ORDER BY Id";        

        public Periodos()
        {
            InitializeComponent();
        }

        private void frmPeriodos_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvPeriodos.DataSource = datosRecibidos.Tables[0];

            } catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }
    
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
             string insercion = string.Format("EXEC InsertarPeriodos '{0}', '{1}'", txtCodigo.Text, txtPeriodo.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvPeriodos.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

            string borrado = string.Format("EXEC EliminaPeriodos '{0}'", dgvPeriodos.CurrentRow.Cells["Código"].Value);

            try
            {

                BaseDeDatos.procesosSql(borrado);
                dgvPeriodos.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtPeriodo.Text = "";
            txtCodigo.Focus();

        }

        private void dgvPeriodos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtCodigo.Text = dgvPeriodos.CurrentRow.Cells["Código"].Value.ToString();
            txtPeriodo.Text = dgvPeriodos.CurrentRow.Cells["Periodo"].Value.ToString();
            
        }

        private void btnModificar_Click(object sender, EventArgs e)

        {
               if (txtCodigo.Text != "" && txtPeriodo.Text != "")
            {

                int Identificador = Convert.ToInt32(dgvPeriodos.CurrentRow.Cells["Identificador"].Value);

                string actualiza = string.Format("EXEC ActualizarPeriodos '{0}', '{1}', '{2}'", Identificador, txtCodigo.Text, txtPeriodo.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvPeriodos.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtPeriodo.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o periodo debe tener algún valor");
                    txtCodigo.Focus();

                } else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Periodo AS Periodo FROM Periodos" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvPeriodos.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {
                    
                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Periodo AS Periodo FROM Periodos" +
                         " WHERE Periodo LIKE '%" + txtPeriodo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvPeriodos.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }


                }

            } else
            {

                btnBuscar.Text = "&Filtrar";
                try
                {

                    DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                    dgvPeriodos.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
