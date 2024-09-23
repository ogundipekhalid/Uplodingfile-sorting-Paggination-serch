namespace SercPag_sorting.Models
{
    public class PagUSerViewModel
    {
        public IQueryable<User> user { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }
        public string SearchTerm { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
