using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Idiomas : BaseEntity
    {

        public string NOMBRE_IDIOMA { get; set; }
        public string PAIS_ORIGEN { get; set; }

        public Idiomas()
        {

        }

        public Idiomas(string[] infoArray)
        {

            this.NOMBRE_IDIOMA = infoArray[0];
            this.PAIS_ORIGEN = infoArray[1];

        }

    }
}
