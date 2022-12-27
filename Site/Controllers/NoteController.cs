using BLL.Base;
using BLL.Interface;
using DAL;
using DAL.Interface;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Site.Models;
using System.Text.RegularExpressions;

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
        public async Task<ActionResult<Note>> GetAll(int ID)
        {
            var request = await _noteService.GetAll(ID);
            return request.Error ? BadRequest(request.Message) : Ok(request.Entities);
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult<Note>> Update(int ID, NoteDTO note)
        {
            var request = await _noteService.Update(Mapping(note), ID);
            return request.Error ? BadRequest(request.Message) : Ok(request.Entity);
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Note>> Delete(int ID)
        {
            var request = await _noteService.Delete(ID);
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
