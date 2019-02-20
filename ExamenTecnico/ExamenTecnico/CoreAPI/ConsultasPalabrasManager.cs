using Combiner;
using DataAccess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class ConsultasPalabrasManager : BaseManager
    {
        private ConsultasPalabrasCrudFactory consultaCrud;

        public ConsultasPalabrasManager()
        {
            consultaCrud = new ConsultasPalabrasCrudFactory();
        }

        public String Create(ConsultasPalabras consulta)
        {
            try
            {
                var c = consultaCrud.Retrieve<ConsultasPalabras>(consulta);

                if (c != null)
                {
                    throw new BusinessException(100);
                }
                else
                {
                    consultaCrud.Create(consulta);
                    return "Registro un caso de uso con éxito";
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                return "Ya existe un caso de uso con el código especificado";
            }
        }

        public List<ConsultasPalabras> RetrieveAll()
        {
            return consultaCrud.RetrieveAll<ConsultasPalabras>();
        }

        public ConsultasPalabras RetrieveByName(ConsultasPalabras consulta)
        {
            ConsultasPalabras c = null;
            try
            {
                c = consultaCrud.Retrieve<ConsultasPalabras>(consulta);
                if (c == null)
                {
                    throw new BusinessException(0);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public void Update(ConsultasPalabras consulta)
        {
            consultaCrud.Update(consulta);
        }

        public void Delete(ConsultasPalabras consulta)
        {
            consultaCrud.Delete(consulta);
        }
    }
}
