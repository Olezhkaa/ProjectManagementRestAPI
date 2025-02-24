namespace ProjectManagementRestAPI.Model
{
    public abstract class BaseEntity
    {
        public DateTime DateCreate { get; set; }
        public DateTime DateChange { get; set; }
    }
}
