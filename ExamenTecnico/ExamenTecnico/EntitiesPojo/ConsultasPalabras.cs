using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class ConsultasPalabras : BaseEntity
    {

        public int CODIGO_REGISTRO {get;set;}
        public string CODIGO_CONSULTA { get; set; }
        public string PALABRA { get; set; }

        public ConsultasPalabras()
        {

        }

        public ConsultasPalabras(string[] infoArray)
        {

            this.CODIGO_REGISTRO = int.Parse(infoArray[0]);
            this.CODIGO_CONSULTA = infoArray[1];
            this.PALABRA = infoArray[2];

        }

    }
}
