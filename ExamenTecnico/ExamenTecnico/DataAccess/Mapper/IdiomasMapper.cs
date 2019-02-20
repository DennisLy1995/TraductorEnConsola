using DataAccess.Dao;
using Entities_POJO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class IdiomasMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_NOMBRE_IDIOMA = "NOMBRE_IDIOMA";
        private const string DB_COL_PAIS_ORIGEN = "PAIS_ORIGEN";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_IDIOMA_PR" };

            var c = (Idiomas)entity;
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);
            operation.AddStringParam(DB_COL_PAIS_ORIGEN, c.PAIS_ORIGEN);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_IDIOMA_PR" };

            var c = (Idiomas)entity;
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_IDIOMAS_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_IDIOMA_PR" };

            var c = (Idiomas)entity;
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);
            operation.AddStringParam(DB_COL_PAIS_ORIGEN, c.PAIS_ORIGEN);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_IDIOMA_PR" };

            var c = (Idiomas)entity;
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var caso = BuildObject(row);
                lstResults.Add(caso);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var idioma = new Idiomas
            {
                NOMBRE_IDIOMA = GetStringValue(row, DB_COL_NOMBRE_IDIOMA),
                PAIS_ORIGEN = GetStringValue(row, DB_COL_PAIS_ORIGEN),
            };

            return idioma;
        }
    }
}


