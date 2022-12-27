using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface INoteRepository
    {
        Task Delete(int ID);
        Task<IEnumerable<Note>> GetAll();
        Task<Note> Insert(Note entity);
        Task<Note> Update(Note entity);
        Task<Note> GetById(int ID);
    }
}
