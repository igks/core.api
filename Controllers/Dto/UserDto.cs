namespace CORE.API.Controllers.Dto
{
    public class AddUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class EditUserDto
    {
        public string Fisrtname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
    }

    public class ViewUserDto
    {
        public int Id { get; set; }
        public string Fisrtname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
    }
}