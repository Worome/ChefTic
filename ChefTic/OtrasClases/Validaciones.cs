using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ChefTic.OtrasClases
{
    class Validaciones
    {

        public static Boolean CompruebaMail(string email)
        {

            bool correcto = false;
            //string patron = @"\A(\w{3,})(\.\w{3,})?@(\w{2,}\.){1,}(\w{2,4})\Z";
            string patron = @"\A(\w{3,})(\.)?(\-)?(\w+)?@(\w{2,}\.){1,}(\w{2,4})\Z";

            Regex comprobacion = new Regex(patron);
            correcto = comprobacion.IsMatch(email);

            return correcto;

        }

        public static Boolean CompruebaWeb(string url)
        {

            bool correcto = false;
            string patron = @"\Ahttps?://([w]{3}\.)?[a-z0-9]+\.[a-z]{2,3}\Z";
            Regex comprobacion = new Regex(patron);
            correcto = comprobacion.IsMatch(url);

            return correcto;

        }


    }
}
