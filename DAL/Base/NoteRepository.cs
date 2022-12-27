using DAL.Interface;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base
{
    public class NoteRepository:  INoteRepository
    {

        private readonly TodoListContext todoListContext;

        public NoteRepository(TodoListContext todoListContext) => this.todoListContext = todoListContext;


        public async Task Delete(int ID)
        {
            var entity = await GetById(ID);

            if (entity == null)

                throw new Exception("The entity is null");
            todoListContext.Notes.Remove(entity);
        }

        public async Task<IEnumerable<Note>> GetAll()
        {
            return await todoListContext.Notes.ToListAsync();
        }

        public async Task<Note> GetById(int id)
        {
            return await todoListContext.Notes.FindAsync(id);
        }

        public async Task<Note> Insert(Note entity)
        {
            todoListContext.Notes.Add(entity);
            return entity;
        }

        public async Task<Note> Update(Note entity)
        {
            todoListContext.Entry(entity).State = EntityState.Modified;
            return entity;

        }


    }
}
