namespace CORE.API.Controllers.Dto
{
    public class SaveEmployeeDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class ViewEmployeeDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}