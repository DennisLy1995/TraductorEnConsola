using DataAccess.Dao;
using Entities_POJO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class ConsultasPalabrasMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO_REGISTRO = "CODIGO_REGISTRO";
        private const string DB_COL_CODIGO_CONSULTA = "CODIGO_CONSULTA";
        private const string DB_COL_PALABRA = "PALABRA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CONSULTA_PALABRA_PR" };

            var c = (ConsultasPalabras)entity;
            operation.AddIntParam(DB_COL_CODIGO_REGISTRO, c.CODIGO_REGISTRO);
            operation.AddStringParam(DB_COL_CODIGO_CONSULTA, c.CODIGO_CONSULTA);
            operation.AddStringParam(DB_COL_PALABRA, c.PALABRA);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONSULTA_PALABRA_PR" };

            var c = (ConsultasPalabras)entity;
            operation.AddIntParam(DB_COL_CODIGO_REGISTRO, c.CODIGO_REGISTRO);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CONSULTAS_PALABRA_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONSULTA_PALABRA_PR" };

            var c = (ConsultasPalabras)entity;
            operation.AddIntParam(DB_COL_CODIGO_REGISTRO, c.CODIGO_REGISTRO);
            operation.AddStringParam(DB_COL_CODIGO_CONSULTA, c.CODIGO_CONSULTA);
            operation.AddStringParam(DB_COL_PALABRA, c.PALABRA);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CONSULTA_PALABRA_PR" };

            var c = (ConsultasPalabras)entity;
            operation.AddIntParam(DB_COL_CODIGO_REGISTRO, c.CODIGO_REGISTRO);
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
            var consulta = new ConsultasPalabras
            {
                CODIGO_REGISTRO = GetIntValue(row, DB_COL_CODIGO_REGISTRO),
                CODIGO_CONSULTA = GetStringValue(row, DB_COL_CODIGO_CONSULTA),
                PALABRA = GetStringValue(row, DB_COL_PALABRA),
            };
            return consulta;
        }
    }
}




