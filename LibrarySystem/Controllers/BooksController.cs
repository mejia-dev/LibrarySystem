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
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorFullName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book book)
    {
      if(!ModelState.IsValid)
      {
        ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorFullName");
        return View(book);
      }
      else
      {
        _db.Books.Add(book);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Book thisBook = _db.Books
          .Include(book => book.Author)
          .Include(book => book.JoinEntities)
          .ThenInclude(join => join.Patron)
          .FirstOrDefault(book => book.BookId == id);
      ViewBag.PageTitle = $"Book Details - {thisBook.BookTitle} ";
      return View(thisBook);
    }

    public ActionResult AddPatron(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronFullName");
      ViewBag.PageTitle = $"Add Patron to {thisBook.BookTitle}";
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddPatron(Book book, int patronId)
    {
      #nullable enable
      BookPatron? joinEntity = _db.BookPatrons.FirstOrDefault(join => (join.PatronId == patronId && join.BookId == book.BookId));
      #nullable disable
      if(joinEntity == null && patronId != 0)
      {
        _db.BookPatrons.Add(new BookPatron() { PatronId = patronId, BookId = book.BookId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = book.BookId });
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

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      BookPatron joinEntry = _db.BookPatrons.FirstOrDefault(entry => entry.BookPatronId == joinId);
      _db.BookPatrons.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}