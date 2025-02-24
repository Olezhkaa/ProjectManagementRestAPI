using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementRestAPI.Model
{
    public class Comment : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int ID_Task { get; set; }
        

    }
}
