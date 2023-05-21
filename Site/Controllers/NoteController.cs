using BLL.Interface;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Models;

namespace Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;


        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost("Insertar")]
        public async Task<ActionResult<Note>> Insert(NoteDTO note)
        {

            var request = await _noteService.Insert(Mapping(note));
            return request.Error ? BadRequest(request.Message) : Ok(request.Entity);
        }


        [HttpGet("list/{id}")]
        public async Task<ActionResult<Note>> GetAll(int id)
        {
            var request = await _noteService.GetAll(id);
            return request.Error ? BadRequest(request.Message) : Ok(request.Entities);
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult<Note>> Update(int id, NoteDTO note)
        {
            var request = await _noteService.Update(Mapping(note), id);
            return request.Error ? BadRequest(request.Message) : Ok(request.Entity);
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Note>> Delete(int id)
        {
            var request = await _noteService.Delete(id);
            return request.Error ? BadRequest(request.Message) : Ok(request.Entity);
        }


        private Note Mapping(NoteDTO noteDto)
        {
            var note = new Note();
            note.Title = noteDto.Title;
            note.Description = noteDto.Description;
            note.IdUser = noteDto.IdUser;
            return note;
        }
    }
}
