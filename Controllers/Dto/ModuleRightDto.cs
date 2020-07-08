namespace CORE.API.Controllers.Dto
{
    public class SaveModuleDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool isUpdate { get; set; } = false;
    }

    public class ViewModuleDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}