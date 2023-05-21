using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entity
{
    public class Note: BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public int IdUser { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
