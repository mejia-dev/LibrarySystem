using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LibrarySystem.Controllers
{
  public class PatronsController : Controller
  {

    private readonly LibrarySystemContext _db;

    public PatronsController(LibrarySystemContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Patrons";
      List<Patron> model = _db.Patrons.ToList();
      return View(model);

    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add a Patron";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron patron)
    {
      _db.Patrons.Add(patron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Patron thisPatron = _db.Patrons
          .Include(patron => patron.JoinEntities)
          .ThenInclude(join => join.Book)
          .FirstOrDefault(patron => patron.PatronId == id);
      ViewBag.PageTitle = $"Patron Details - {thisPatron.PatronName} ";
      return View(thisPatron);
    }

    public ActionResult Edit(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      ViewBag.PageTitle = $"Edit Patron - {thisPatron.PatronName}";
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookTitle");  
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron patron)
    {
      _db.Patrons.Update(patron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      Patron thisPatron = _db.Patrons
          .Include(patron => patron.JoinEntities)
          .ThenInclude(join => join.Book)
          .FirstOrDefault(patrons => patrons.PatronId == id);
          ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookTitle");
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult AddBook(Patron patron, int bookId)
    {
      #nullable enable
      BookPatron? joinEntity = _db.BookPatrons.FirstOrDefault(join => (join.BookId == bookId && join.PatronId == patron.PatronId));
      #nullable disable
      if (bookId != 0 && joinEntity == null)
      {
        BookPatron bookLoan = new BookPatron() { BookId = bookId, PatronId = patron.PatronId, Checkout = DateTime.Now };
        bookLoan.GetReturnDate();
        _db.BookPatrons.Add(bookLoan);

        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = patron.PatronId });
    }

    public ActionResult Delete(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      ViewBag.PageTitle = $"Delete Patron - {thisPatron.PatronName}";
      return View(thisPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      _db.Patrons.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult DeleteJoin(int joinId)
    {
      BookPatron joinEntry = _db.BookPatrons.FirstOrDefault(entry => entry.BookPatronId == joinId);
      _db.BookPatrons.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Search()
    {
      ViewBag.PageTitle = "Search Patrons";
      return View();
    }

    [HttpPost]
    public ActionResult Search(string searchName)
    {
      List<Patron> model = _db.Patrons.Where(patrons => patrons.PatronName.Contains(searchName)).ToList();
      return View("Index", model);
}
  }
}