using DataAccess.Dao;
using Entities_POJO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class UsuariosMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CEDULA = "CEDULA";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_APELLIDO = "APELLIDO";
        private const string DB_COL_NOMBRE_USUARIO = "NOMBRE_USUARIO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_USUARIO_PR" };

            var c = (Usuarios)entity;
            operation.AddIntParam(DB_COL_CEDULA, c.CEDULA);
            operation.AddStringParam(DB_COL_NOMBRE, c.NOMBRE);
            operation.AddStringParam(DB_COL_APELLIDO, c.APELLIDO);
            operation.AddStringParam(DB_COL_NOMBRE_USUARIO, c.NOMBRE_USUARIO);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_USUARIO_PR" };

            var c = (Usuarios)entity;
            operation.AddStringParam(DB_COL_NOMBRE_USUARIO, c.NOMBRE_USUARIO);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_USUARIOS_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USUARIO_PR" };

            var c = (Usuarios)entity;
            operation.AddIntParam(DB_COL_CEDULA, c.CEDULA);
            operation.AddStringParam(DB_COL_NOMBRE, c.NOMBRE);
            operation.AddStringParam(DB_COL_APELLIDO, c.APELLIDO);
            operation.AddStringParam(DB_COL_NOMBRE_USUARIO, c.NOMBRE_USUARIO);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_REQUERIMIENTO_PR" };

            var c = (Usuarios)entity;
            operation.AddIntParam(DB_COL_CEDULA, c.CEDULA);
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
            var usuario = new Usuarios
            {
                CEDULA = GetIntValue(row, DB_COL_CEDULA),
                NOMBRE = GetStringValue(row, DB_COL_NOMBRE),
                APELLIDO = GetStringValue(row, DB_COL_APELLIDO),
                NOMBRE_USUARIO = GetStringValue(row, DB_COL_NOMBRE_USUARIO),
            };

            return usuario;
        }
    }
}


