using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Response
{
    public class GenericResponse<TEntity> where TEntity : BaseEntity
    {

        public IEnumerable<TEntity> Entities { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
        public TEntity Entity { get; set; }

        public GenericResponse(IEnumerable<TEntity> entities)
        {
            Entities = entities;
            Error = false;
        }

        public GenericResponse(TEntity entity)
        {
            Entity = entity;
            Error = false;
        }

        public GenericResponse(string message)
        {
            Message = message;
            Error = true;
        }

    }
}
