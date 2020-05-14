namespace CORE.API.Controllers.Dto
{
    public class SaveDepartmentDto
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public bool isUpdate { get; set; } = false;
    }

    public class ViewDepartmentDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}