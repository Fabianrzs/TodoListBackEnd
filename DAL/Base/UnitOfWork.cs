using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoListContext Context;

        public UnitOfWork(TodoListContext context) => Context = context;

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public TodoListContext _Context()
        {
            return Context;
        }

    }
}
