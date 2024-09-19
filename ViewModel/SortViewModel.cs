using MovieTestCRUDMethodsForDB.Controllers;

namespace MovieTestCRUDMethodsForDB.ViewModel
{
    public class SortViewModel(SortMovieState sortMovieState)
    {
        public SortMovieState? MovieTitleSort { get; set; } = sortMovieState;
    }
}
