using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Usuarios : BaseEntity
    {

        public int CEDULA { get; set; }
        public string NOMBRE {get;set;}
        public string APELLIDO { get; set; }
        public string NOMBRE_USUARIO { get; set; }

        public Usuarios()
        {

        }

        public Usuarios(string[] infoArray)
        {

            this.CEDULA = int.Parse(infoArray[0]);
            this.NOMBRE = infoArray[1];
            this.APELLIDO = infoArray[2];
            this.NOMBRE_USUARIO = infoArray[3];

        }

    }
}
