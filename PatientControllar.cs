using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using HOspitalManagment.Model;

public class PatientController : Controller
{
    private readonly HospitalContext _context;

    public PatientController(HospitalContext context)
    {
        _context = context;
    }

    // GET: Patient
    public async Task<IActionResult> Index()
    {
        return View(await _context.Patients.ToListAsync());
    }

    // GET: Patient/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(m => m.PatientId == id);
        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    // GET: Patient/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Patient/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Age,Gender,Address,Phone,MedicalHistory")] Patient patient)
    {
        if (ModelState.IsValid)
        {
            _context.Add(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(patient);
    }
}