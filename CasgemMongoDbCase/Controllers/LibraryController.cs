using CasgemMongoDbCase.DAL;
using CasgemMongoDbCase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq.Expressions;
using System.Security.Cryptography.Pkcs;

namespace CasgemMongoDbCase.Controllers
{
    public class LibraryController : Controller
    {
        MongoDbContext db = new MongoDbContext();
        Writer writer = new Writer();
        public IActionResult Index()
        {

            #region MongoDbVeriListelemeJson
            //var getBooksWithWriters = db.Books.Aggregate().Lookup("Writers", "WriterId", "_id", "WriterDetails").ToList();//json tipinde veri döner
            //foreach (var book in getBooksWithWriters)
            //{
            //    var writerDetails = book.GetValue("WriterDetails").AsBsonArray.First();
            //    var writerName = writerDetails["Name"].AsString;
            //    var writerAge = writerDetails["Age"].AsInt32;
            //}
            #endregion

            #region MongoDbVeriListeleme

            var getBooksWithWritersWithModels = db.Books.Aggregate().Lookup<Book, Writer, BookWithWriter>(db.Writers, x => x.WriterId, y => y.Id, z => z.Writers).ToList();
            return View(getBooksWithWritersWithModels);

            #endregion

        }
        public IActionResult AddBook()
        {
            var writers = db.Writers.Find(FilterDefinition<Writer>.Empty).ToList();

            List<SelectListItem> values = (from x in writers
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.Id.ToString()
                                           }).ToList();

            ViewBag.WriterList = values;

            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BookViewModel model)
        {
            var writerId = ObjectId.Parse(model.WriterId);
            Book book = new Book() //kitap ekleme
            {
                BookName = model.BookName,
                WriterId = writerId,
                PageNumber = model.PageNumber,
                Type = model.Type,
                Point = model.Point,
            };
            db.Books.InsertOne(book);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateBook(string id)
        {
            var getBooksWithWritersWithModels = db.Books.Aggregate().Lookup<Book, Writer, BookWithWriter>(db.Writers, x => x.WriterId, y => y.Id, z => z.Writers).ToList();

            var getById = getBooksWithWritersWithModels.Find(x => x.Id == id);

            #region SelectListItem
            var writers = db.Writers.Find(FilterDefinition<Writer>.Empty).ToList();

            List<SelectListItem> values = (from x in writers
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.Id.ToString()
                                           }).ToList();

            ViewBag.WriterList = values;
            #endregion

            return View(getById);
        }
        [HttpPost]
        public IActionResult UpdateBook(BookViewModel model)
        {
            var writerId = ObjectId.Parse(model.WriterId);
            Book book = new Book()
            {
                Id = model.Id,
                BookName = model.BookName,
                PageNumber = model.PageNumber,
                Point = model.Point,
                WriterId = writerId,
                Type = model.Type,
            };

            db.Books.FindOneAndReplace(x => x.Id == model.Id, book);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteBook(string id)
        {
            db.Books.DeleteOne(x => x.Id == id);
            return RedirectToAction("Index");
        }
        public IActionResult ListWriters()
        {
            var getWritersWithCities = db.Writers.Aggregate().Lookup<Writer, City, WriterWithCity>(db.Cities, x => x.CityId, y => y.Id, z => z.Cities).ToList();
            return View(getWritersWithCities);
        }

        public IActionResult AddWriter()
        {
            var cities = db.Cities.Find(FilterDefinition<City>.Empty).ToList();

            List<SelectListItem> values = (from x in cities
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.Id.ToString()
                                           }).ToList();

            ViewBag.CityList = values;
            return View();
        }

        [HttpPost]
        public IActionResult AddWriter(WriterViewModel model)
        {
            var cityId = ObjectId.Parse(model.CityId);
            Writer writer = new Writer()
            {
                Name = model.Name,
                Age = model.Age,
                CityId = cityId
            };
            db.Writers.InsertOne(writer);

            return RedirectToAction("ListWriters");
        }

        public IActionResult UpdateWriter(string id)
        {
            var getWritersWithCities = db.Writers.Aggregate().Lookup<Writer, City, WriterWithCity>(db.Cities, x => x.CityId, y => y.Id, z => z.Cities).ToList();
            var getById = getWritersWithCities.Find(x => x.Id == id);

            var cities = db.Cities.Find(FilterDefinition<City>.Empty).ToList();
            List<SelectListItem> values = (from x in cities
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.Id.ToString()
                                           }).ToList();

            ViewBag.CityList = values;
            return View(getById);
        }

        [HttpPost]
        public IActionResult UpdateWriter(WriterViewModel model)
        {
            var cityId = ObjectId.Parse(model.CityId);
            Writer writer = new Writer()
            {
                Id = model.Id,
                Name = model.Name,
                Age = model.Age,
                CityId = cityId
            };

            db.Writers.FindOneAndReplace(x => x.Id == model.Id, writer);

            return RedirectToAction("ListWriters");
        }

        public IActionResult DeleteWriter(string id)
        {
            db.Writers.DeleteOne(x => x.Id == id);
            return RedirectToAction("ListWriters");
        }

        public IActionResult ListCities()
        {
            var values = db.Cities.Find(FilterDefinition<City>.Empty).ToList();
            return View(values);
        }

        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCity(CityViewModel model)
        {
            City city = new City()
            {
                Name = model.Name
            };
            db.Cities.InsertOne(city);
            return RedirectToAction("ListCities");
        }

        public IActionResult UpdateCity(string id)
        {
            var values = db.Cities.Find(FilterDefinition<City>.Empty).ToList();
            var getById = values.Find(x => x.Id == id);
            return View(getById);
        }

        [HttpPost]
        public IActionResult UpdateCity(CityViewModel model)
        {
            City city = new City()
            {
                Id = model.Id,
                Name = model.Name
            };
            db.Cities.FindOneAndReplace(x => x.Id == model.Id, city);
            return RedirectToAction("ListCities");
        }
        public IActionResult DeleteCity(string id)
        {
            db.Cities.DeleteOne(x => x.Id == id);
            return RedirectToAction("ListWriters");
        }
    }
}
