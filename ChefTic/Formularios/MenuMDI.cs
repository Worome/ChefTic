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

        private void periodosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Periodos ventanaPeriodos = new Periodos();
            ventanaPeriodos.MdiParent = this;
            ventanaPeriodos.Show();

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
