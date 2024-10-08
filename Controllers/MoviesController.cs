﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTestCRUDMethodsForDB.Core;
using MovieTestCRUDMethodsForDB.Data;
using MovieTestCRUDMethodsForDB.Models;
using MovieTestCRUDMethodsForDB.ViewModel;

namespace MovieTestCRUDMethodsForDB.Controllers
{
    public enum SortMovieState
    {
        AskTitle,
        DescTitle
    }
    public enum Filter
    {
        Ask,
        Desc
    }
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        private const int pageSize = 3;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        //public async Task<IActionResult> Index(Filter? selectPriceFilter,Filter? selectFilter, string movieGenre,string searchNameString)
        //{
        //    var genres = from g in _context.Movies
        //                 orderby g.Genre
        //                 select g.Genre;
        //    var movies = from m in _context.Movies
        //                 select m;
        //    if(!string.IsNullOrEmpty(searchNameString))
        //    {
        //        movies = movies.Where(movie => movie.Title.ToUpper().Contains(searchNameString.ToUpper()));
        //    }

        //    if(selectFilter is not null)
        //    {
        //        if(selectFilter == Filter.Ask)
        //        {
        //            movies = movies.OrderBy(movie => movie.Title);
        //        }
        //        else
        //        {
        //            movies = movies.OrderByDescending(movie => movie.Title);
        //        }
        //    }

        //    if (selectPriceFilter is not null)
        //    {
        //        if (selectPriceFilter == Filter.Ask)
        //        {
        //            movies = movies.OrderBy(movie => movie.Price);
        //        }
        //        else
        //        {
        //            movies = movies.OrderByDescending(movie => movie.Price);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(movieGenre))
        //    {
        //        movies = movies.Where(movie => movie.Genre == movieGenre);
        //    }
        //    var movieGenreVM = new MovieGenreViewModel
        //    {
        //        Genres = new SelectList(await genres.Distinct().ToListAsync()),
        //        Movies = await movies.ToListAsync()
        //    };
        //    return View(movieGenreVM);
        //}

        public async Task<IActionResult> Index(string selectedMovieGenre, string selectedMovieTitle, SortMovieState sortMovieState, int page = 1)
        {
            IQueryable<Movie> movies = _context.Movies;
            if(!string.IsNullOrEmpty(selectedMovieTitle))
            {
                movies = movies.Where(m => m.Title.ToUpper().Contains(selectedMovieTitle.ToUpper()));
            }

            if (!string.IsNullOrEmpty(selectedMovieGenre))
            {
                movies = movies.Where(m => m.Genre == selectedMovieGenre);
            }

            /* Продвинутый вариант */
            movies = sortMovieState switch
            {
                SortMovieState.AskTitle => movies.OrderBy(m => m.Title),
                SortMovieState.DescTitle => movies.OrderByDescending(m => m.Title),
                _ => movies // по умолчанию 
            };

            /* Вариант обычный*/
            //if(sortMovieState is not null)
            //{
            //    if(sortMovieState.Value == SortMovieState.AskTitle)
            //    {
            //        movies.OrderBy(m => m.Title);
            //    }
            //    else
            //    {
            //        movies.OrderByDescending(m => m.Title);
            //    }
            //}

            var genres = new SelectList(_context.Movies.Select(m => m.Genre).Distinct());

            var moviesViewModel = new MoviesViewModel
            {
                SortViewModel = new(sortMovieState),
                FilteredViewModel = new(genres,selectedMovieGenre,selectedMovieTitle),
                Movies = await PaginationList<Movie>.CreateAsync(movies,page,pageSize)
            };

            return View(moviesViewModel);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
