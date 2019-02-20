using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;
using System;
using System.Collections.Generic;

namespace DataAccess.Crud
{
    public class IdiomasCrudFactory : CrudFactory
    {
        IdiomasMapper mapper;

        public IdiomasCrudFactory() : base()
        {
            mapper = new IdiomasMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var idioma = (Idiomas)entity;
            var sqlOperation = mapper.GetCreateStatement(idioma);

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
            var idioma = (Idiomas)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(idioma));
        }

        public override void Delete(BaseEntity entity)
        {
            var idioma = (Idiomas)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(idioma));
        }
    }
}
