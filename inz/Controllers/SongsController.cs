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
using Microsoft.AspNetCore.Http;

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

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(await artist.ToListAsync());
            else
                return View(await artist.ToListAsync());
        }

        public async Task<IActionResult> Album(string nameAlbum)
        {
            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist);
            var album = from a in _context.Song
                        select a;

            album = applicationDbContext
                        .Where(i => i.Album.Name_Album == nameAlbum);

            
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(await album.ToListAsync());
            else
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

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(await result.ToListAsync());
            else
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

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(song);
            else
                return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVM(CreateViewModel createViewModel)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (ModelState.IsValid)
            {
                string fileName = null;

                if (createViewModel.Name_mp3 != null)
                {
                    fileName = createViewModel.Name_mp3.FileName;
                    IFormFile file = createViewModel.Name_mp3;
                    BlobsController blobsController = new BlobsController();
                    blobsController.UploadBlob(fileName, file, "mp3");
                }

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

                Song song = new Song();
                var resultSong = _context.Song
                    .Where(i => i.Title == createViewModel.Title)
                    .Count();
                if (resultSong < 1)
                {
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

                    song.UrlAzure = fileName;

                    _context.Song.Add(song);
                    _context.SaveChanges();

                    return RedirectToAction("Index", new { all = "all" });
                }
                else
                {
                    ViewData["Exist"] = "Podany utwór już istnieje!";
                    if (isAjax)
                        return PartialView("Create");
                    else
                        return View("Create");
                    //return View("Create");
                }              
            }
            if (isAjax)
                return PartialView("Create");
            else
                return View("Create");
        }

        [HttpGet]
        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            var createViewModel = new CreateViewModel()
            {
                ID_Song = song.ID_Song,
                Title = song.Title,
                Name_Artist = song.Artist.Name_Artist,
                Name_Album = song.Album.Name_Album,
                Name_Producer = song.Producer.Name_Producer,
                UrlAzure = song.UrlAzure
            };

            if (song == null)
            {
                return NotFound();
            }

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(createViewModel);
            else
                return View(createViewModel);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                Song song = _context.Song.FirstOrDefault(s => s.ID_Song == id);

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

                string fileName = null;

                if (createViewModel.Name_mp3 != null)
                {
                    fileName = createViewModel.Name_mp3.FileName;
                    IFormFile file = createViewModel.Name_mp3;
                    BlobsController blobsController = new BlobsController();
                    blobsController.UploadBlob(fileName, file, "mp3");
                }

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

                song.UrlAzure = fileName;

                _context.Song.Update(song);
                _context.SaveChanges();

                return RedirectToAction("Index", new { all = "all" });
            }
            return View(createViewModel);
        }

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

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(song);
            else
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

            if (song.UrlAzure != null)
            {
                BlobsController blobsController = new BlobsController();
                blobsController.DeleteBlob(song.UrlAzure, "mp3");
            }

            return RedirectToAction("Index", new { all = "all" });
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.ID_Song == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImgAlbum(IFormFile ImgAlbum, int idAlbum)
        {
            string fileName = null;

            if (ImgAlbum != null)
            {
                fileName = ImgAlbum.FileName;
                IFormFile file = ImgAlbum;
                BlobsController blobsController = new BlobsController();
                blobsController.UploadBlob(fileName, file, "imgalbum");
            }

            Album album = _context.Album.FirstOrDefault(s => s.ID_Album == idAlbum);

            album.ImgAlbumUrl = fileName;
            _context.Album.Update(album);
            _context.SaveChanges();


            return RedirectToAction("Index", new { all = "all" });
        }
    }
}
