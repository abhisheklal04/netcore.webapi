using System;

namespace WebApi.Models
{
    public class SortPageModel
    {
        private int _pageNumber = 10;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Page number must be greater than 0.");
                _pageNumber = value;
            }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Page size must be greater than 0.");
                _pageSize = value;
            }
        }

        public string SortCol { get; set; }

        public bool SortDesc { get; set; }
    }
}
