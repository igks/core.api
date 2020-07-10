using System.Collections.Generic;

namespace CORE.API.Controllers.Dto
{
    public class SaveRoleGroupDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<int> ModulesReadId { get; set; }
        public ICollection<int> ModulesWriteId { get; set; }
        public bool isUpdate { get; set; }
    }

    public class ViewRoleGroupDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<int> ModulesReadId { get; set; }
        public ICollection<int> ModulesWriteId { get; set; }
    }
}