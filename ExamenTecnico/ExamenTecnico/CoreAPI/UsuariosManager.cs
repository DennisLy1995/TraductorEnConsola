using Combiner;
using DataAccess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;


namespace CoreAPI
{
    public class UsuariosManager : BaseManager
    {
        private UsuariosCrudFactory crudUsuarios;

        public UsuariosManager()
        {
            crudUsuarios = new UsuariosCrudFactory();
        }

        public String Create(Usuarios usuario)
        {
            try
            {
                var c = crudUsuarios.Retrieve<Usuarios>(usuario);

                if (c != null)
                {
                    throw new BusinessException(100);
                }
                else
                {
                    crudUsuarios.Create(usuario);
                    return "Registro un caso de uso con éxito";
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                return "Ya existe un caso de uso con el código especificado";
            }
        }

        public List<Usuarios> RetrieveAll()
        {
            return crudUsuarios.RetrieveAll<Usuarios>();
        }

        public Usuarios RetrieveByUserName(Usuarios usuario)
        {
            Usuarios c = null;
            try
            {
                c = crudUsuarios.Retrieve<Usuarios>(usuario);
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

        public void Update(Usuarios usuario)
        {
            crudUsuarios.Update(usuario);
        }

        public void Delete(Usuarios usuario)
        {
            crudUsuarios.Delete(usuario);
        }
    }
}

