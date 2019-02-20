using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Palabras : BaseEntity
    {

        public string PALABRA{ get; set; }
        public string NOMBRE_IDIOMA{ get; set; }
        public string PALABRA_PRIMER_REGISTRO{ get; set; }
        public string TIPO{ get; set; }

        public Palabras()
        {

        }

        public Palabras(string[] infoArray)
        {

            this.PALABRA = infoArray[0];
            this.NOMBRE_IDIOMA = infoArray[1];
            this.PALABRA_PRIMER_REGISTRO = infoArray[2];
            this.TIPO = infoArray[3];

        }

    }
}
