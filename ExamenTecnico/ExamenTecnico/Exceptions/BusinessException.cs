using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class BusinessException : Exception
    {
        public int ExceptionId;
        public ApplicationMessage AppMessage { get; set; }

        public BusinessException()
        {

        }

        public BusinessException(int idException)
        {
            ExceptionId = idException;
        }

        public BusinessException(int idException, Exception innerException)
        {
            ExceptionId = idException;
        }
    }
}
