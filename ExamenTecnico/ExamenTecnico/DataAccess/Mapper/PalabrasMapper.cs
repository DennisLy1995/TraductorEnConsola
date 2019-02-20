using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class PalabrasMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_PALABRA = "PALABRA";
        private const string DB_COL_NOMBRE_IDIOMA = "NOMBRE_IDIOMA";
        private const string DB_COL_PALABRA_PRIMER_REGISTRO = "PALABRA_PRIMER_REGISTRO";
        private const string DB_COL_TIPO = "TIPO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_Palabra_PR" };

            var c = (Palabras)entity;
            operation.AddStringParam(DB_COL_PALABRA, c.PALABRA);
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);
            operation.AddStringParam(DB_COL_PALABRA_PRIMER_REGISTRO, c.PALABRA_PRIMER_REGISTRO);
            operation.AddStringParam(DB_COL_TIPO, c.TIPO);

            return operation;
        }

        internal SqlOperation GetCreatePrimeraPalabraStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_PRIMERA_PALABRA_PR" };

            var c = (Palabras)entity;
            operation.AddStringParam(DB_COL_PALABRA_PRIMER_REGISTRO, c.PALABRA_PRIMER_REGISTRO);
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);
            operation.AddStringParam(DB_COL_TIPO, c.TIPO);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_Palabra_PR" };

            var c = (Palabras)entity;
            operation.AddStringParam(DB_COL_PALABRA, c.PALABRA);

            return operation;
        }


        public SqlOperation GetRetrivePrimeraPalabraStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_PRIMERA_PALABRA_PR" };

            var c = (Palabras)entity;
            operation.AddStringParam(DB_COL_PALABRA_PRIMER_REGISTRO, c.PALABRA);

            return operation;
        }

        public SqlOperation GetRetrivePalabraIdiomStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_PALABRA_EN_IDIOMA_PR" };

            var c = (Palabras)entity;
            operation.AddStringParam(DB_COL_PALABRA_PRIMER_REGISTRO, c.PALABRA);
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);

            return operation;
        }

        

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_Palabras_PR" };
            return operation;
        }

        public SqlOperation GetRetriveAllPrimerasPalabrasStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PRIMERAS_PALABRAS_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_Palabra_PR" };

            var c = (Palabras)entity;
            operation.AddStringParam(DB_COL_PALABRA, c.PALABRA);
            operation.AddStringParam(DB_COL_NOMBRE_IDIOMA, c.NOMBRE_IDIOMA);
            operation.AddStringParam(DB_COL_PALABRA_PRIMER_REGISTRO, c.PALABRA_PRIMER_REGISTRO);
            operation.AddStringParam(DB_COL_TIPO, c.TIPO);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_Palabra_PR" };

            var c = (Palabras)entity;
            operation.AddStringParam(DB_COL_PALABRA, c.PALABRA);
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
            var palabra = new Palabras
            {
                PALABRA = GetStringValue(row, DB_COL_PALABRA),
                NOMBRE_IDIOMA = GetStringValue(row, DB_COL_NOMBRE_IDIOMA),
                PALABRA_PRIMER_REGISTRO = GetStringValue(row, DB_COL_PALABRA_PRIMER_REGISTRO),
                TIPO = GetStringValue(row, DB_COL_TIPO),
            };

            return palabra;
        }
    }
}



