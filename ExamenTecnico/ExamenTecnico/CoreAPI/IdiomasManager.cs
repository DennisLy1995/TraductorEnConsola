using Combiner;
using DataAccess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;


namespace CoreAPI
{
    public class IdiomasManager : BaseManager
    {
        private IdiomasCrudFactory crudIdiomas;

        public IdiomasManager()
        {
            crudIdiomas = new IdiomasCrudFactory();
        }

        public String Create(Idiomas idioma)
        {
            try
            {
                var c = crudIdiomas.Retrieve<Idiomas>(idioma);

                if (c != null)
                {
                    throw new BusinessException(100);
                }
                else
                {
                    crudIdiomas.Create(idioma);
                    return "Registro un caso de uso con éxito";
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                return "Ya existe un idioma con el nombre especificado";
            }
        }

        public List<Idiomas> RetrieveAll()
        {
            return crudIdiomas.RetrieveAll<Idiomas>();
        }

        public Idiomas RetrieveByName(Idiomas idioma)
        {
            Idiomas c = null;
            try
            {
                c = crudIdiomas.Retrieve<Idiomas>(idioma);
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

        public void Update(Idiomas idioma)
        {
            crudIdiomas.Update(idioma);
        }

        public void Delete(Idiomas idioma)
        {
            crudIdiomas.Delete(idioma);
        }
    }
}


