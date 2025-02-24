using System.ComponentModel.DataAnnotations;

namespace ProjectManagementRestAPI.Model
{
    public class UsersProject
    {
        [Key]
        public int Id { get; set; }
        public int ID_Project { get; set; }
        public int ID_User { get; set; }
    }
}
