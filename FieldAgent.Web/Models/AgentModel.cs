using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Web.Models
{
    public class AgentModel
    {
        public int AgentId { get; set; }

        [Required(ErrorMessage = "Agent first name is required")]
        [StringLength(50, ErrorMessage = "Agent first name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Agent last name is required")]
        [StringLength(50, ErrorMessage = "Agent last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Agent date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Agent height is required")]
        //May want to add limit on height error message
        public decimal Height { get; set; }
    }
}
