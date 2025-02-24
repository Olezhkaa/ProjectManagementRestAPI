using System.ComponentModel.DataAnnotations;

namespace ProjectManagementRestAPI.Model
{
    public class StatusTask
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
