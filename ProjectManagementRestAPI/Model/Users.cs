using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementRestAPI.Model
{
    public class Users : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
