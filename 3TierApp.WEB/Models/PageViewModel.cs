namespace _3TierApp.WEB.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        //private string _sortOrder;
        //public string sortOrder
        //{
        //    get
        //    {
        //        return _sortOrder;
        //    }

        //    set
        //    {
        //        if (value == "Name") {
        //            _sortOrder = _sortOrder == "Name" ? "Name_desc" : "Name";
        //        }
        //        else if (value == "Birth")
        //        {
        //            _sortOrder = _sortOrder == "Birth" ? "Brith_desc" : "Birth";
        //        }
        //    }
        //}

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
