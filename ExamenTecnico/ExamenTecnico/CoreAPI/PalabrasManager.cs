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
    public class PalabrasManager : BaseManager
    {
        private PalabrasCrudFactory crudPalabra;

        public PalabrasManager()
        {
            crudPalabra = new PalabrasCrudFactory();
        }


        public string CreatePrimeraPalabra(Palabras palabra)
        {
            try
            {
                
                crudPalabra.CreatePrimeraPalabra(palabra);
                return "Registro una palabra con éxito";

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                return "Ya existe un caso de uso con el código especificado";
            }
        }

        public String Create(Palabras palabra)
        {
            try
            {
                var c = crudPalabra.Retrieve<Palabras>(palabra);

                if (c != null)
                {
                    throw new BusinessException(100);
                }
                else
                {
                    crudPalabra.Create(palabra);
                    return "Registro un caso de uso con éxito";
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                return "Ya existe un caso de uso con el código especificado";
            }
        }

        public List<Palabras> RetrieveAll()
        {
            return crudPalabra.RetrieveAll<Palabras>();
        }

        public List<Palabras> RetrieveAllPrimerasPalabras()
        {
            return crudPalabra.RetrieveAllPrimerasPalabras<Palabras>();
        }

        public Palabras RetrieveByName(Palabras palabra)
        {
            Palabras c = null;
            try
            {
                c = crudPalabra.Retrieve<Palabras>(palabra);
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

        public Palabras RetrieveByPrimeraPalabra(Palabras palabra)
        {
            Palabras c = null;
            try
            {
                c = crudPalabra.RetrieveByPrimeraPalabra<Palabras>(palabra);
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

        public Palabras RetrieveByNameAndIdiom(Palabras palabra)
        {
            Palabras c = null;
            try
            {
                c = crudPalabra.RetrieveByPalabraAndIdiom<Palabras>(palabra);
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
        


        public void Update(Palabras palabra)
        {
            crudPalabra.Update(palabra);
        }

        public void Delete(Palabras palabra)
        {
            crudPalabra.Delete(palabra);
        }


    }
}
