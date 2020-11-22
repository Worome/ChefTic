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
    public partial class Autores : Form
    {

        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Nombre AS Autor, Telefono AS Teléfono, " +
            "Email AS [E-Mail], Web AS Web, Localidad AS Localidad, Pais AS País FROM Autores ORDER BY Id";

        public Autores()
        {
            InitializeComponent();
        }

        private void Autores_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                dgvAutores.DataSource = datosRecibidos.Tables[0];

            }
            catch (Exception ex)
            {

                Mensajes.MostrarMensajesError(ex.Message);

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string insercion = string.Format("EXEC InsertarAutores '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'", txtCodigo.Text, txtNombre.Text, 
                txtTelefono.Text, txtEmail.Text, txtWeb.Text, txtLocalidad.Text, txtPais.Text);

            if (txtCodigo.Text == "")
            {
                Mensajes.MostrarMensajesError("El Código no puede estar vacío");
            }
            else
            {
                try
                {

                    BaseDeDatos.procesosSql(insercion);
                    dgvAutores.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];

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

            string borrado = string.Format("EXEC EliminaAutores '{0}'", dgvAutores.CurrentRow.Cells["Código"].Value);

            try
            {

                BaseDeDatos.procesosSql(borrado);
                dgvAutores.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtWeb.Text = "";
            txtLocalidad.Text = "";
            txtPais.Text = "";
            txtCodigo.Focus();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            int Identificador = Convert.ToInt32(dgvAutores.CurrentRow.Cells["Identificador"].Value);

            if (Identificador > 0)
            {

                string actualiza = string.Format("EXEC ActualizarAutores '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'",
                    Identificador, txtCodigo.Text, txtNombre.Text, txtTelefono.Text, txtEmail.Text, txtWeb.Text, txtLocalidad.Text, 
                    txtPais.Text);

                try
                {

                    BaseDeDatos.procesosSql(actualiza);
                    dgvAutores.DataSource = BaseDeDatos.procesosSql(consultaTotal).Tables[0];
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

                if (txtCodigo.Text == "" && txtNombre.Text == "")
                {

                    Mensajes.MostrarMensajesError("El campo código o comida debe tener algún valor");
                    txtCodigo.Focus();

                }
                else if (txtCodigo.Text != "")
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Nombre AS Autor, Telefono AS Teléfono, " +
                        "Email AS [E-Mail], Web AS Web, Localidad AS Localidad, Pais AS País FROM Autores" +
                        " WHERE Codigo LIKE '%" + txtCodigo.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvAutores.DataSource = datosRecibidos.Tables[0];

                    }
                    catch (Exception ex)
                    {

                        Mensajes.MostrarMensajesError(ex.Message);

                    }

                }
                else
                {

                    cadenaBusqueda = "SELECT Id as Identificador, Codigo AS Código, Nombre AS Autor, Telefono AS Teléfono, " +
                        "Email AS [E-Mail], Web AS Web, Localidad AS Localidad, Pais AS País FROM Autores" +
                        " WHERE Codigo LIKE '%" + txtNombre.Text + "%' ORDER BY Id";
                    btnBuscar.Text = "&Quitar Filtro";

                    try
                    {

                        DataSet datosRecibidos = BaseDeDatos.procesosSql(cadenaBusqueda);
                        dgvAutores.DataSource = datosRecibidos.Tables[0];

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
                    dgvAutores.DataSource = datosRecibidos.Tables[0];
                    Limpiar();

                }
                catch (Exception ex)
                {

                    Mensajes.MostrarMensajesError(ex.Message);

                }


            }

        }

        private void dgvAutores_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtCodigo.Text = dgvAutores.CurrentRow.Cells["Código"].Value.ToString();
            txtNombre.Text = dgvAutores.CurrentRow.Cells["Autor"].Value.ToString();
            txtTelefono.Text = dgvAutores.CurrentRow.Cells["Teléfono"].Value.ToString();
            txtEmail.Text = dgvAutores.CurrentRow.Cells["E-Mail"].Value.ToString();
            txtWeb.Text = dgvAutores.CurrentRow.Cells["Web"].Value.ToString();
            txtLocalidad.Text = dgvAutores.CurrentRow.Cells["Localidad"].Value.ToString();
            txtPais.Text = dgvAutores.CurrentRow.Cells["País"].Value.ToString();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();

        }
    }
}
