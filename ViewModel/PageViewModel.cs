namespace MovieTestCRUDMethodsForDB.ViewModel
{
    public class PageViewModel(int count, int pageNumber, int pageSize)
    {
        public int PageNumber { get; } = pageNumber;
        public int TotalPage { get; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPage;
    }
}
