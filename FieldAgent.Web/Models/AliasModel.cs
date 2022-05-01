using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Web.Models
{
    public class AliasModel
    {
        public int AliasId { get; set; }

        [Required(ErrorMessage = "Alias name is required")]
        [StringLength(50, ErrorMessage = "Alias name cannot exceed 50 characters")]
        public string AliasName { get; set; }
        public Guid? InterpolId { get; set; }
        public string? Persona { get; set; }

        //Many-to-one
        [Required(ErrorMessage = "Associated AgentId is required")]
        public int AgentId { get; set; }
    }
}
