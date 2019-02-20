using Combiner;
using DataAccess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class ConsultasFrasesManager : BaseManager
    {
        private ConsultasFrasesCrudFactory crudConsulta;

        public ConsultasFrasesManager()
        {
            crudConsulta = new ConsultasFrasesCrudFactory();
        }

        public String Create(ConsultasFrases consulta)
        {
            try
            {
                var c = crudConsulta.Retrieve<ConsultasFrases>(consulta);

                if (c != null)
                {
                    throw new BusinessException(100);
                }
                else
                {
                    crudConsulta.Create(consulta);
                    return "Registro un caso de uso con éxito";
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                return "Ya existe un caso de uso con el código especificado";
            }
        }

        public List<ConsultasFrases> RetrieveAll()
        {
            return crudConsulta.RetrieveAll<ConsultasFrases>();
        }
        

        public ConsultasFrases RetrieveByCodigo(ConsultasFrases consulta)
        {
            ConsultasFrases c = null;
            try
            {
                c = crudConsulta.Retrieve<ConsultasFrases>(consulta);
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


        public ConsultasFrases RetrieveCounter(ConsultasFrases consulta)
        {
            ConsultasFrases c = null;
            try
            {
                c = crudConsulta.RetrieveCounter<ConsultasFrases>(consulta);
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

        public void Update(ConsultasFrases consulta)
        {
            crudConsulta.Update(consulta);
        }

        public void Delete(ConsultasFrases consulta)
        {
            crudConsulta.Delete(consulta);
        }
    }
}
