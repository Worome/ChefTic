using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using ChefTic.BBDD;

namespace ChefTic.Formularios
{
    public partial class Periodos : Form
    {

        public string consultaTotal = "SELECT Codigo AS Código, Periodo FROM Periodos";

        public Periodos()
        {
            InitializeComponent();
        }

        private void frmPeriodos_Load(object sender, EventArgs e)
        {
            //string codigos = ""; 
            //string periodos = "";

            try
            {

                DataSet datosRecibidos = BaseDeDatos.procesosSql(consultaTotal);
                //codigos = datosRecibidos.Tables[0].Rows[0]["Codigo"].ToString();
                //periodos = datosRecibidos.Tables[0].Rows[0]["Periodo"].ToString();
                dgvPeriodos.DataSource = datosRecibidos.Tables[0];

            } catch (Exception ex)
            {


                MessageBox.Show("Ha ocurrido el siguiente error: " + ex.Message);

            }            


            //txtCodigo.Text = codigos;
            //txtPeriodo.Text = periodos;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
