using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieTestCRUDMethodsForDB.ViewModel
{
    public class FilteredViewModel(SelectList genres,string selectedMovieGenre, string selectedMovieTitle)
    {
        public SelectList Genres { get; } = genres;
        public string SelectedMovieGenre { get; } = selectedMovieGenre;
        public string SelectedMovieTitle { get; } = selectedMovieTitle;

    }
}
