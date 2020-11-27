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

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Categorias ventanaCategorias = new Categorias();
            ventanaCategorias.MdiParent = this;
            ventanaCategorias.Show();

        }

        private void comidasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Comidas ventanaComidas = new Comidas();
            ventanaComidas.MdiParent = this;
            ventanaComidas.Show();

        }

        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Autores ventanaAutores = new Autores();
            ventanaAutores.MdiParent = this;
            ventanaAutores.Show();

        }

        private void tiposDePlatosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TiposPlatos ventanaTiposPlatos = new TiposPlatos();
            ventanaTiposPlatos.MdiParent = this;
            ventanaTiposPlatos.Show();

        }

        private void tiposDeCocinadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cocinados ventanaCocinados = new Cocinados();
            ventanaCocinados.MdiParent = this;
            ventanaCocinados.Show();
        }

        private void temporadasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Temporadas ventanaTemporadas = new Temporadas();
            ventanaTemporadas.MdiParent = this;
            ventanaTemporadas.Show();

        }

        private void dificultadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Dificultad ventanaDificultad = new Dificultad();
            ventanaDificultad.MdiParent = this;
            ventanaDificultad.Show();

        }

        private void caloríasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Calorias ventanaCalorias = new Calorias();
            ventanaCalorias.MdiParent = this;
            ventanaCalorias.Show();

        }

        private void publicacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Publicaciones ventanaPublicaciones = new Publicaciones();
            ventanaPublicaciones.MdiParent = this;
            ventanaPublicaciones.Show();

        }
    }
}
