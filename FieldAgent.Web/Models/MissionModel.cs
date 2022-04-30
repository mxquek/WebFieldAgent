using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Web.Models
{
    public class MissionModel
    {
        public int MissionId { get; set; }

        [Required(ErrorMessage = "Mission code name is required")]
        [StringLength(50, ErrorMessage = "Mission code name cannot exceed 50 characters")]
        public string CodeName { get; set; }
        
        [Required(ErrorMessage = "Mission start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Mission projected end date is required")]
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        [Required(ErrorMessage = "Mission operational cost is required")]
        public decimal OperationalCost { get; set; }
        public string? Notes { get; set; }

        //Many-to-one Agency
        [Required(ErrorMessage = "Associated AgencyId is required")]
        public int AgencyId { get; set; }
    }
}
