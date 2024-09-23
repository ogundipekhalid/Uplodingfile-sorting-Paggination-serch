namespace SercPag_sorting.Models
{
    public class UserViewModel
    {
        public IQueryable<User> user { get; set; }
        public string EmailSortingOrd { get; set; }
        public string NameSortingOrd { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string OrderBy { get; set; }
        public string Term { get; set; }
    }
}
