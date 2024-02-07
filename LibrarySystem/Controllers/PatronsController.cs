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
          .Include(patron => patron.JoinEntites)
          .ThenInclude(patron => patron.Book)
          .FirstOrDefault(patron => patron.PatronId == id);
      ViewBag.PageTitle = $"Patron Details - {thisPatron.PatronName} ";
      return View(thisPatron);
    }

    public ActionResult Edit(int id)
    {
      Patron thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      ViewBag.PageTitle = $"Edit Patron - {thisPatron.PatronName}";
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron patron)
    {
      _db.Patrons.Update(patron);
      _db.SaveChanges();
      return RedirectToAction("Index");
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

  }
}