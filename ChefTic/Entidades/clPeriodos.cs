using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefTic.Entidades
{
    public class clPeriodos
    {
        
        private string codigo, periodo;

        public string Codigo { get => codigo; set => codigo = value; }
        public string Periodo { get => periodo; set => periodo = value; }

        public clPeriodos()
        {

            Codigo = "";
            Periodo = "";

        }
    }
}
