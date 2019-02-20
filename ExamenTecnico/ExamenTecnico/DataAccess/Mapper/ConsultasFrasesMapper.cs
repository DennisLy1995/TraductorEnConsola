using DataAccess.Dao;
using Entities_POJO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class ConsultasFrasesMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO_CONSULTA = "CODIGO_CONSULTA";
        private const string DB_COL_NOMBRE_CEDULA = "CEDULA";
        private const string DB_COL_FRASE = "FRASE";
        private const string DB_COL_TRADUCCION_ESPANOL = "TRADUCCION_ESPANOL";
        private const string DB_COL_CANTIDAD_PALABRAS = "CANTIDAD_PALABRAS";
        private const string DB_COL_FECHA_CONSULTA = "FECHA_CONSULTA";
        private const string DB_COL_POPULARIDAD = "POPULARIDAD";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CONSULTA_FRASES_PR" };

            var c = (ConsultasFrases)entity;
            operation.AddStringParam(DB_COL_CODIGO_CONSULTA, c.CODIGO_CONSULTA);
            operation.AddIntParam(DB_COL_NOMBRE_CEDULA, c.CEDULA);
            operation.AddStringParam(DB_COL_FRASE, c.FRASE);
            operation.AddStringParam(DB_COL_TRADUCCION_ESPANOL, c.TRADUCCION_ESPANOL);
            operation.AddIntParam(DB_COL_CANTIDAD_PALABRAS, c.CANTIDAD_PALABRAS);
            operation.AddStringParam(DB_COL_FECHA_CONSULTA, c.FECHA_CONSULTA);
            operation.AddIntParam(DB_COL_POPULARIDAD, c.POPULARIDAD);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONSULTA_FRASES_PR" };

            var c = (ConsultasFrases)entity;
            operation.AddStringParam(DB_COL_CODIGO_CONSULTA, c.CODIGO_CONSULTA);

            return operation;
        }

        public SqlOperation GetRetriveCounterStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONSULTA_FRASES_CANTIDAD_PR" };

            return operation;
        }


        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CONSULTA_FRASES_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONSULTA_FRASES_PR" };

            var c = (ConsultasFrases)entity;
            operation.AddStringParam(DB_COL_CODIGO_CONSULTA, c.CODIGO_CONSULTA);
            operation.AddIntParam(DB_COL_NOMBRE_CEDULA, c.CEDULA);
            operation.AddStringParam(DB_COL_FRASE, c.FRASE);
            operation.AddStringParam(DB_COL_TRADUCCION_ESPANOL, c.TRADUCCION_ESPANOL);
            operation.AddIntParam(DB_COL_CANTIDAD_PALABRAS, c.CANTIDAD_PALABRAS);
            operation.AddStringParam(DB_COL_FECHA_CONSULTA, c.FECHA_CONSULTA);
            operation.AddIntParam(DB_COL_POPULARIDAD, c.POPULARIDAD);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CONSULTA_FRASES_PR" };

            var c = (ConsultasFrases)entity;
            operation.AddStringParam(DB_COL_CODIGO_CONSULTA, c.CODIGO_CONSULTA); ;
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
            var consulta = new ConsultasFrases
            {
                CODIGO_CONSULTA = GetStringValue(row, DB_COL_CODIGO_CONSULTA),
                CEDULA = GetIntValue(row, DB_COL_NOMBRE_CEDULA),
                FRASE = GetStringValue(row, DB_COL_FRASE),
                TRADUCCION_ESPANOL = GetStringValue(row, DB_COL_TRADUCCION_ESPANOL),
                CANTIDAD_PALABRAS = GetIntValue(row, DB_COL_CANTIDAD_PALABRAS),
                FECHA_CONSULTA = GetStringValue(row, DB_COL_FECHA_CONSULTA),
                POPULARIDAD = GetIntValue(row, DB_COL_POPULARIDAD),
            };
            return consulta;
        }
    }
}



