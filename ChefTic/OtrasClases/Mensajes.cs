using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChefTic.OtrasClases
{
    public class Mensajes
    {
        public static void MostrarMensajesError(string mensaje)
        {

            MessageBox.Show("Ha ocurrido el siguiente error: " + mensaje, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
