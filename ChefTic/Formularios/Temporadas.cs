using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChefTic.Formularios
{
    public partial class Temporadas : Form
    {

        public string consultaTotal = "SELECT Id as Identificador, Codigo AS Código, Temporada AS [Temporada] FROM Cocinados " +
            "ORDER BY Id";

        public Temporadas()
        {
            InitializeComponent();
        }

        private void Temporadas_Load(object sender, EventArgs e)
        {

        }
    }
}
