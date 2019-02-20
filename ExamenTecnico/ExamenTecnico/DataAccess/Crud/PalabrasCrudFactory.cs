using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;
using System;
using System.Collections.Generic;

namespace DataAccess.Crud
{
    public class PalabrasCrudFactory : CrudFactory
    {
        PalabrasMapper mapper;

        public PalabrasCrudFactory() : base()
        {
            mapper = new PalabrasMapper();
            dao = SqlDao.GetInstance();
        }

        public void CreatePrimeraPalabra(BaseEntity entity)
        {
            var palabra = (Palabras)entity;
            var sqlOperation = mapper.GetCreatePrimeraPalabraStatement(palabra);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Create(BaseEntity entity)
        {
            var palabra = (Palabras)entity;
            var sqlOperation = mapper.GetCreateStatement(palabra);

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

        public T RetrieveByPrimeraPalabra<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrivePrimeraPalabraStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }


        public T RetrieveByPalabraAndIdiom<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrivePalabraIdiomStatement(entity));
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

        public List<T> RetrieveAllPrimerasPalabras<T>()
        {
            var lstCasos = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllPrimerasPalabrasStatement());
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
            var palabra = (Palabras)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(palabra));
        }

        public override void Delete(BaseEntity entity)
        {
            var palabra = (Palabras)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(palabra));
        }
    }
}
