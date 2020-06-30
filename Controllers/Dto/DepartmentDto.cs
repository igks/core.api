namespace CORE.API.Controllers.Dto
{
    public class SaveDepartmentDto
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? ManagerId { get; set; }
        public int? AssistantId { get; set; }
        public bool isUpdate { get; set; } = false;
    }

    public class ViewDepartmentDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? ManagerId { get; set; }
        public int? AssistantId { get; set; }
    }
}