using Entities_POJO;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public interface IObjectMapper
    {
        List<BaseEntity> BuildObjects(List<Dictionary<string, object>> rows);
        BaseEntity BuildObject(Dictionary<string, object> row);
    }
}
