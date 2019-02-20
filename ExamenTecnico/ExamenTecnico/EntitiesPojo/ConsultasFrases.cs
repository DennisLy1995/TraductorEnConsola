using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class ConsultasFrases : BaseEntity
    {

        public string CODIGO_CONSULTA {get;set;}
        public int CEDULA { get; set; }
        public string FRASE { get; set; }
        public string TRADUCCION_ESPANOL { get; set; }
        public int CANTIDAD_PALABRAS { get; set; }
        public string FECHA_CONSULTA { get; set; }
        public int POPULARIDAD { get; set; }

        public ConsultasFrases()
        {

        }

        public ConsultasFrases(String[] infoArray)
        {
            this.CODIGO_CONSULTA = infoArray[0];
            this.CEDULA = int.Parse(infoArray[1]);
            this.FRASE = infoArray[2];
            this.TRADUCCION_ESPANOL= infoArray[4];
            this.CANTIDAD_PALABRAS = int.Parse(infoArray[5]);
            this.FECHA_CONSULTA = infoArray[6];
            this.POPULARIDAD = int.Parse(infoArray[7]);
        }

    }
}
