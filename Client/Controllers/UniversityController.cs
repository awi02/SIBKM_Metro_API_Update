using API.Models;
using Client.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;
[Authorize]
public class UniversityController : Controller
{
    private readonly UniversityRepository repository;

    public UniversityController(UniversityRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        //localhost/university/
        var Results = await repository.Get();
        var universities = new List<University>();

        if (Results != null)
        {
            universities = Results.Data.ToList();
        }

        return View(universities);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(University universiy)
        {
            var result = await repository.Post(universiy);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return View();
        }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        //localhost/university/
        var Results = await repository.Get(id);
        //var universities = new University();

        //if (Results != null)
        //{
        //    universities = Results.Data;
        //}

        return View(Results.Data);
    }
//edit-id
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var Results = await repository.Get(id);
        var university = new University();

        if (Results.Data?.Id is null)
        {
            return View(university);
        }
        else
        {
            university.Id = Results.Data.Id;
            university.Name = Results.Data.Name;
        }
        return View(university);
    }
 //edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(University university)
    {
        if (ModelState.IsValid)
        {
            var result = await repository.Put(university.Id, university);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 500)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }

        return View();
    }
//delete
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await repository.Get(id);
        var university = result?.Data;

        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        var result = await repository.Delete(id);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data berhasil dihapus";
            return RedirectToAction(nameof(Index));
        }

        var university = await repository.Get(id);
        return View("Delete", university?.Data);
    }
}
