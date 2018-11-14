﻿using System.Web.Mvc;
using BookStore.BLL.DTO;
using BookStore.BLL.Interfaces;

namespace BookStore.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin 
        private IBookService _bookService;
        private IAuthorService _authorService;
        private ICategoryService _categoryService;

        public AdminController(IBookService bookService, IAuthorService authorService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _authorService = authorService;
            _bookService = bookService;
        }

        public ActionResult Main()
        {
            var books = _bookService.GetBooks(null, null);
            return View(books);
        }

        public ActionResult GetAllAuthors()
        {
            var authors = _authorService.GetAllAuthors();


            return PartialView("ShowAuthors",authors);
        }

        public ActionResult GetAllBooks()
        {
            var books = _bookService.GetBooks();
            return PartialView("ShowBooks", books);
        }
        public JsonResult GetById(int id)
        {
            var book = _bookService.GetBook(id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var book = _bookService.GetBook(id);
             _bookService.DeleteBook(id);
            var books = _bookService.GetBooks(null, null);
            return PartialView("ShowBooks", books);
            
        }

        public ActionResult Edit(int id)
        {
            SelectList authors = new SelectList(_authorService.GetAllAuthors(), "Id", "Name");
            SelectList categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName");
            ViewBag.authors = authors;
            ViewBag.categories = categories;
            var book = _bookService.GetBook(id);
            
            return PartialView(book);
        }

        [HttpPost]
        public ActionResult Edit(BookDTO book)
        {
            _bookService.Update(book);
            var books = _bookService.GetBooks(null,null);

            return RedirectToAction("Main");
        }

        public ActionResult Create()
        {
            SelectList authors = new SelectList(_authorService.GetAllAuthors(), "Id", "Name");
            SelectList categories = new SelectList(_categoryService.GetCategories(), "Id", "CategoryName");
            ViewBag.authors = authors;
            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public ActionResult Create(BookDTO book)
        {
            _bookService.CreateBook(book);
            return RedirectToAction("Main");
        }
    }
}