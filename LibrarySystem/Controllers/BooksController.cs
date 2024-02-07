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
      return View(_db.Books.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add a Book";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book book)
    {
      _db.Books.Add(book);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}