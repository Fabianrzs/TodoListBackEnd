using Entity;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class NoteDTO
    {    
        public string Title { get; set; }
      
        public string Description { get; set; }

        public int IdUser { get; set; }
    }
}
