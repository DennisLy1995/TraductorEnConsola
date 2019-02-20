﻿using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;
using System;
using System.Collections.Generic;

namespace DataAccess.Crud
{
    public class UsuariosCrudFactory : CrudFactory
    {
        UsuariosMapper mapper;

        public UsuariosCrudFactory() : base()
        {
            mapper = new UsuariosMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var usuario = (Usuarios)entity;
            var sqlOperation = mapper.GetCreateStatement(usuario);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstCasos = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCasos.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCasos;
        }

        public override void Update(BaseEntity entity)
        {
            var requerimiento = (Usuarios)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(requerimiento));
        }

        public override void Delete(BaseEntity entity)
        {
            var requerimiento = (Usuarios)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(requerimiento));
        }
    }
}
