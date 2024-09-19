using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTestCRUDMethodsForDB.Controllers;
using MovieTestCRUDMethodsForDB.Models;

namespace MovieTestCRUDMethodsForDB.ViewModel
{
    public class MovieGenreViewModel
    {
        public string? MovieGenre { get; set; }
        public string? SearchNameString { get; set; }
        public Filter SelectPriceFilter { get; set; }
        public required List<Movie> Movies { get; set; }
        public required SelectList Genres { get; set; }
        public Filter Selectfilter { get; set; }
    }
}
