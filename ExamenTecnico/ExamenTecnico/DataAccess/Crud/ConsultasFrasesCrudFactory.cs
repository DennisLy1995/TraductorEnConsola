using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;
using System;
using System.Collections.Generic;

namespace DataAccess.Crud
{
    public class ConsultasFrasesCrudFactory : CrudFactory
    {
        ConsultasFrasesMapper mapper;

        public ConsultasFrasesCrudFactory() : base()
        {
            mapper = new ConsultasFrasesMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var consulta = (ConsultasFrases)entity;
            var sqlOperation = mapper.GetCreateStatement(consulta);

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

        public T RetrieveCounter<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveCounterStatement(entity));
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
            var consulta = (ConsultasFrases)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(consulta));
        }

        public override void Delete(BaseEntity entity)
        {
            var consulta = (ConsultasFrases)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(consulta));
        }
    }
}
