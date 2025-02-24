using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementRestAPI.Model
{
    public class Task : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ID_Project { get; set; }
        public int ID_Status_Task { get; set; }

    }
}
