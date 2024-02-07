using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LibrarySystem.Controllers
{
  public class BooksController : Controller
  {

    private readonly LibrarySystemContext _db;

    public BooksController(LibrarySystemContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Books";
                          List<Book> model = _db.Books
                          .Include(book => book.Author)
                          .ToList();
      return View(model);

    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add a Book";
      ViewBag.AuthorsList = new SelectList(_db.Authors, "AuthorId", "AuthorFullName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book book)
    {
      _db.Books.Add(book);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Book thisBook = _db.Books
          .Include(book => book.Author)
          .FirstOrDefault(book => book.BookId == id);
      ViewBag.PageTitle = $"Book Details - {thisBook.BookTitle} ";
      return View(thisBook);
    }

    public ActionResult Edit(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.PageTitle = $"Edit Author - {thisBook.BookTitle}";
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book)
    {
      _db.Books.Update(book);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.PageTitle = $"Delete Book - {thisBook.BookTitle}";
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}