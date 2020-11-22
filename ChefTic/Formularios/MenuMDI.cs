using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChefTic.Formularios;

namespace ChefTic
{
    public partial class menuMDI : Form
    {
        public menuMDI()
        {
            InitializeComponent();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void periodosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            Periodos ventanaPeriodos = new Periodos();
            ventanaPeriodos.MdiParent = this;
            ventanaPeriodos.Show();

        }

        private void unidadesDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnidadesMedida ventanaUnidadesMedida = new UnidadesMedida();
            ventanaUnidadesMedida.MdiParent = this;
            ventanaUnidadesMedida.Show();
        }

        private void fuentesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Fuentes ventanaFuentes = new Fuentes();
            ventanaFuentes.MdiParent = this;
            ventanaFuentes.Show();

        }
    }
}
