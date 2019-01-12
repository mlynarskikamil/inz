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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

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
            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist).OrderByDescending(s => s.Like);
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
            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist).OrderByDescending(s => s.Like);
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
            var userId = User.Identity.GetUserId();

            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist).OrderByDescending(s => s.Like);
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
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(new CreateViewModel());
            else
                return View(new CreateViewModel());
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
                    string fileName = null;

                    if (createViewModel.Name_mp3 != null)
                    {
                        fileName = createViewModel.Name_mp3.FileName;
                        var resultFileName = _context.Song
                            .Where(i => i.UrlAzure == fileName)
                            .Count();
                        if (resultFileName < 1)
                        {
                            if (fileName.EndsWith(".mp3"))
                            {
                                IFormFile file = createViewModel.Name_mp3;
                                BlobsController blobsController = new BlobsController();
                                blobsController.UploadBlob(fileName, file, "mp3");
                            }
                            else
                            {
                                ViewData["Extension"] = "Podany plik nie jest w formacie MP3!";
                                if (isAjax)
                                    return PartialView("Create");
                                else
                                    return View("Create");
                            }
                        }
                        else
                        {
                            ViewData["Extension"] = "Podany plik istnieje w bazie!";
                            if (isAjax)
                                return PartialView("Create");
                            else
                                return View("Create");
                        }
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

                    song.ReleaseSong = createViewModel.ReleaseSong;

                    var url = createViewModel.VideoUrl;
                    if (createViewModel.VideoUrl != null)
                    {
                        url = createViewModel.VideoUrl.Replace("watch?v=", "embed/");
                    }
                    song.VideoUrl = url;

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
                UrlAzure = song.UrlAzure,
                ReleaseSong = song.ReleaseSong,
                VideoUrl = song.VideoUrl,
                TextSong = song.TextSong
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

                    song.UrlAzure = fileName;
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

                song.ReleaseSong = createViewModel.ReleaseSong;

                var url = createViewModel.VideoUrl;
                if (createViewModel.VideoUrl != null)
                {
                    url = createViewModel.VideoUrl.Replace("watch?v=", "embed/");
                }
                song.VideoUrl = url;

                song.TextSong = createViewModel.TextSong;

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
            while (_context.Opinion.Where(x => x.ID_Song == id).Count() != 0)
            {
                var opinion = _context.Opinion.Where(x => x.ID_Song == id).FirstOrDefault();
                _context.Opinion.Remove(opinion);
                _context.SaveChanges();
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgAlbum(IFormFile ImgAlbum, int idAlbum)
        {
            BlobsController blobsController = new BlobsController();
            string fileName = null;

            if (ImgAlbum != null)
            {
                fileName = ImgAlbum.FileName;
                IFormFile file = ImgAlbum;
                blobsController.UploadBlob(fileName, file, "imgalbum");
            }

            Album album = _context.Album.FirstOrDefault(s => s.ID_Album == idAlbum);

            blobsController.DeleteBlob(album.ImgAlbumUrl, "imgalbum");


            album.ImgAlbumUrl = fileName;
            _context.Album.Update(album);
            _context.SaveChanges();


            return RedirectToAction("Index", new { all = "all" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImgArtist(IFormFile ImgArtist, int idArtist)
        {
            string fileName = null;

            if (ImgArtist != null)
            {
                fileName = ImgArtist.FileName;
                IFormFile file = ImgArtist;
                BlobsController blobsController = new BlobsController();
                blobsController.UploadBlob(fileName, file, "imgartist");
            }

            Artist artist = _context.Artist.FirstOrDefault(s => s.ID_Artist == idArtist);

            artist.ImgArtistUrl = fileName;
            _context.Artist.Update(artist);
            _context.SaveChanges();


            return RedirectToAction("Index", new { all = "all" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgArtist(IFormFile ImgArtist, int idArtist)
        {
            BlobsController blobsController = new BlobsController();
            string fileName = null;

            if (ImgArtist != null)
            {
                fileName = ImgArtist.FileName;
                IFormFile file = ImgArtist;
                blobsController.UploadBlob(fileName, file, "imgartist");
            }

            Artist artist = _context.Artist.FirstOrDefault(s => s.ID_Artist == idArtist);

            blobsController.DeleteBlob(artist.ImgArtistUrl, "imgartist");


            artist.ImgArtistUrl = fileName;
            _context.Artist.Update(artist);
            _context.SaveChanges();


            return RedirectToAction("Index", new { all = "all" });
        }

        public async Task<IActionResult> Like(int idSong, bool direction)
        {
            var userId = User.Identity.GetUserId();

            var result = _context.Opinion.Where(o => o.Id == userId && o.ID_Song == idSong).Count();

            if (result <= 0)
            {

                Song song = _context.Song.FirstOrDefault(s => s.ID_Song == idSong);

                Opinion opinion = new Opinion();
                opinion.ID_Song = idSong;
                opinion.Id = userId;
                if (direction == true)
                {
                    int like = _context.Song.Where(l => l.ID_Song == idSong).Select(l => l.Like).First();
                    like = like + 1;
                    song.Like = like;
                    opinion.Direction = true;
                }
                else
                {
                    int unlike = _context.Song.Where(l => l.ID_Song == idSong).Select(l => l.Unlike).First();
                    unlike = unlike + 1;
                    song.Unlike = unlike;
                    opinion.Direction = false;
                }
                _context.Opinion.Update(opinion);
                _context.SaveChanges();

                _context.Song.Update(song);
                _context.SaveChanges();
            }
            else
            {
                string chcked;
                bool check = _context.Opinion.Where(o => o.Id == userId && o.ID_Song == idSong).Select(o => o.Direction).First();
                if (check == true)
                {
                    chcked = "lubisz ten utwór";
                }
                else
                {
                    chcked = "nie lubisz tego utworu";
                }

                TempData["checked"] = "Już wybrałeś opcję że " + chcked;
            }

            return RedirectToAction("Index", new { all = "all" });
        }

        [HttpGet]
        public async Task<IActionResult> EditArtist(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .SingleOrDefaultAsync(m => m.ID_Artist == id);

            var artistViewModel = new ArtistViewModel()
            {
                Name = artist.Name,
                Surname = artist.Surname,
                Birthday = artist.Birthday
            };

            if (artist == null)
            {
                return NotFound();
            }

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(artistViewModel);
            else
                return View(artistViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArtist(int id, ArtistViewModel artistViewModel)
        {
            if (ModelState.IsValid)
            {
                Artist artist = _context.Artist.FirstOrDefault(s => s.ID_Artist == id);

                artist.Name = artistViewModel.Name;
                artist.Surname = artistViewModel.Surname;
                artist.Birthday = artistViewModel.Birthday;

                _context.Artist.Update(artist);
                _context.SaveChanges();

                return RedirectToAction("Index", new { all = "all" });
            }
            return View(artistViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditAlbum(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .SingleOrDefaultAsync(m => m.ID_Album == id);

            var albumViewModel = new AlbumViewModel()
            {
                InfoAlbum = album.InfoAlbum,
                ReleaseDate = album.ReleaseDate
            };

            if (album == null)
            {
                return NotFound();
            }

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView(albumViewModel);
            else
                return View(albumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAlbum(int id, AlbumViewModel albumViewModel)
        {
            if (ModelState.IsValid)
            {
                Album album = _context.Album.FirstOrDefault(s => s.ID_Album == id);

                album.InfoAlbum = albumViewModel.InfoAlbum;
                album.ReleaseDate = albumViewModel.ReleaseDate;

                _context.Album.Update(album);
                _context.SaveChanges();

                return RedirectToAction("Index", new { all = "all" });
            }
            return View(albumViewModel);
        }
    }
}
