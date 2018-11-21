using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inz.Data;
using inz.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace inz.Controllers
{

    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Artist(string name)
        {
            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist);
            var artist = from a in _context.Song
                         select a;

            artist = applicationDbContext
                        .Where(i => i.Artist.Name_Artist == name);

            return View(await artist.ToListAsync());
        }

        public async Task<IActionResult> Album(string nameAlbum)
        {
            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist);
            var album = from a in _context.Song
                         select a;

            album = applicationDbContext
                        .Where(i => i.Album.Name_Album == nameAlbum);

            return View(await album.ToListAsync());
        }

        // GET: Songs
        public async Task<IActionResult> Index(string search, string all)
        {
            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist);
            var result = from a in _context.Song
                         select a;

            if (!string.IsNullOrEmpty(all))
            {
                ViewBag.ShowList = true;
                result = applicationDbContext;
            }
            else
            {
                if (!String.IsNullOrEmpty(search))
                {
                    result = applicationDbContext
                        .Where(i => i.Artist.Name_Artist == search
                    || i.Title == search
                    || i.Album.Name_Album == search);

                    if (!result.Any())
                    {
                        ViewBag.ShowList = false;
                    }
                    else
                    {
                        ViewBag.ShowList = true;
                    }
                }
            }
            return View(await result.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Producer)
                .SingleOrDefaultAsync(m => m.ID_Song == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["ID_Album"] = new SelectList(_context.Album, "ID_Album", "ID_Album");
            ViewData["ID_Artist"] = new SelectList(_context.Artist, "ID_Artist", "ID_Artist");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Song,Title,ID_Artist,ID_Album")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Album"] = new SelectList(_context.Album, "ID_Album", "ID_Album", song.ID_Album);
            ViewData["ID_Artist"] = new SelectList(_context.Artist, "ID_Artist", "ID_Artist", song.ID_Artist);
            return View(song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVM(CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {

                Artist artist = new Artist();
                var resultArtist = _context.Artist
                    .Where(i => i.Name_Artist == createViewModel.Name_Artist)
                    .Count();
                if (resultArtist < 1)
                {
                    artist.Name_Artist = createViewModel.Name_Artist;
                    _context.Artist.Add(artist);
                    _context.SaveChanges();
                }

                Album album = new Album();
                var resultAlbum = _context.Album
                    .Where(i => i.Name_Album == createViewModel.Name_Album)
                    .Count();
                if (resultAlbum < 1)
                {
                    album.Name_Album = createViewModel.Name_Album;
                    _context.Album.Add(album);
                    _context.SaveChanges();
                }

                Producer producer = new Producer();
                var resultProducer = _context.Producer
                    .Where(i => i.Name_Producer == createViewModel.Name_Producer)
                    .Count();
                if (resultProducer < 1)
                {
                    producer.Name_Producer = createViewModel.Name_Producer;
                    _context.Producer.Add(producer);
                    _context.SaveChanges();
                }

                Mp3 mp3 = new Mp3();
                using (var memoryStream = new MemoryStream())
                {
                    await createViewModel.Name_mp3.CopyToAsync(memoryStream);
                    mp3.Name_mp3 = memoryStream.ToArray();
                }
                _context.Mp3.Add(mp3);
                _context.SaveChanges();

                Song song = new Song();
                song.Title = createViewModel.Title;
                song.ID_Album = _context.Album
                    .Where(c => c.Name_Album.Equals(createViewModel.Name_Album))
                    .Select(c => c.ID_Album)
                    .First();

                song.ID_Artist = _context.Artist
                    .Where(c => c.Name_Artist.Equals(createViewModel.Name_Artist))
                    .Select(c => c.ID_Artist)
                    .First();

                song.ID_Producer = _context.Producer
                    .Where(c => c.Name_Producer.Equals(createViewModel.Name_Producer))
                    .Select(c => c.ID_Producer)
                    .FirstOrDefault();

                song.ID_Mp3 = _context.Mp3
                    //.Where(c => c.Name_mp3.Equals(createViewModel.Name_mp3))
                    .Select(c => c.ID_Mp3)
                    .FirstOrDefault();

                _context.Song.Add(song);
                _context.SaveChanges();

                return RedirectToAction("Index", new {all = "all" });
            }
            return View();
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var song = await _context.Song.SingleOrDefaultAsync(m => m.ID_Song == id);
            var song = await _context.Song
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Producer)
                .SingleOrDefaultAsync(m => m.ID_Song == id);

            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Song song)
        {
            if (id != song.ID_Song)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.ID_Song))
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
            return View(song);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditVM(CreateViewModel createViewModel)
        //{
        //    if (createViewModel.Name_Album == null)
        //    {
        //        return NotFound();
        //    }

        //    var song = await _context.Song.SingleOrDefaultAsync(m => m.ID_Song == id);
        //    if (song == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(song);
        //}

        [Authorize(Roles = "Admin")]
        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Producer)
                .SingleOrDefaultAsync(m => m.ID_Song == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.SingleOrDefaultAsync(m => m.ID_Song == id);
            _context.Song.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { all = "all" });
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.ID_Song == id);
        }
    }
}
