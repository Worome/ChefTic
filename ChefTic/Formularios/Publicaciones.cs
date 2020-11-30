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
    public partial class Publicaciones : Form
    {

        public string consultaPeriodos = "SELECT Codigo AS Codigo, Periodo AS Periodo FROM Periodos ORDER BY Periodo";
        //public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Calorias AS Calorías FROM Calorias ORDER BY Id";
        public string periodoSeleccionado = "";

        public Publicaciones()
        {
            InitializeComponent();
        }

        private void Publicaciones_Load(object sender, EventArgs e)
        {

            try
            {

                DataSet datosPeriodos = BaseDeDatos.procesosSql(consultaPeriodos);
                cbPeriodos.DataSource = datosPeriodos.Tables[0];

                cbPeriodos.ValueMember = "Codigo";
                cbPeriodos.DisplayMember = "Periodo";


            }
            catch (Exception)
            {



            }


        }

        private void cbPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbPeriodos.SelectedValue.ToString() != null)
            {

                periodoSeleccionado = cbPeriodos.SelectedValue.ToString();

            }
            

        }

        private void btnPeriodos_Click(object sender, EventArgs e)
        {
            Periodos ventanaPeriodo = new Periodos();
            ventanaPeriodo.ShowDialog();

        }
    }
}
