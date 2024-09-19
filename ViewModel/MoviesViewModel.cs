﻿using MovieTestCRUDMethodsForDB.Models;

namespace MovieTestCRUDMethodsForDB.ViewModel
{
    public class MoviesViewModel
    {
        public required IEnumerable<Movie> Movies { get; set; }

        public required PageViewModel PageViewModel { get; set; }
        public required FilteredViewModel FilteredViewModel { get; set; }
        public required SortViewModel SortViewModel { get; set; }
    }
}
