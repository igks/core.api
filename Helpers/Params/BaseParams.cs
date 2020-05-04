using System.Collections.Generic;

namespace CORE.API.Helpers.Params
{
    public class BaseParams
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string FilterBy { get; set; }

        public string SortBy { get; set; }

        public string OrderBy { get; set; }

        public bool isDescending { get; set; } = false;
    }

    public class FilterBy
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }

    public class SortBy
    {
        public string Field { get; set; }
        public bool isDescending { get; set; } = false;
    }
}