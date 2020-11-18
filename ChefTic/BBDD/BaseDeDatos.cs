using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ChefTic.BBDD
{
    public class BaseDeDatos
    {

        public static DataSet procesosSql (string cmd)
        {

            string cadenaConexionBD = "Data Source=.;Initial Catalog=ChefTic;Integrated Security=True;Pooling=False";
            SqlConnection conexionBD = new SqlConnection(cadenaConexionBD);
            conexionBD.Open();

            DataSet datos = new DataSet();
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd, cadenaConexionBD);
            adaptador.Fill(datos);

            conexionBD.Close();

            return datos;

        }

    }
}
