using MovieTestCRUDMethodsForDB.Core;
using MovieTestCRUDMethodsForDB.Models;

namespace MovieTestCRUDMethodsForDB.ViewModel
{
    public class MoviesViewModel
    {
        public required PaginationList<Movie> Movies { get; set; }

        public required FilteredViewModel FilteredViewModel { get; set; }
        public required SortViewModel SortViewModel { get; set; }
    }
}
