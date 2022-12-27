using BLL.Response;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface INoteService
    {
        Task<GenericResponse<Note>> GetAll(int id);
        Task<GenericResponse<Note>> GetById(int id);
        Task<GenericResponse<Note>> Insert(Note note);
        Task<GenericResponse<Note>> Update(Note note, int id);
        Task<GenericResponse<Note>> Delete(int id);
    }
}
