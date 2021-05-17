using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSCHAR.Data;
using POSCHAR.Models;

namespace POSCHAR.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ordenes de venta
        /// </summary>
        /// <returns></returns>
        ///
        public IActionResult SalePos()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name");

            ViewData["StatusValue"] = new List<SelectListItem>{
                new SelectListItem { Value = "Pendiete", Text = "Pendiente" },
                new SelectListItem { Value = "Pagada", Text = "Pagada" }
            };

            ViewData["Products"] = _context.Product.Where(s=>s.Status=="Activo").OrderBy(s=>s.Name).ToListAsync();
            return View();
        }

        /// <summary>
        /// Renderizar y obtener informacion extra
        /// </summary>
        /// <returns></returns>
        ///
        // GET:api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetSingleProduct(int? id)
        {
            
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            List<Product> products = new List<Product>();
            products.Add(product);

            return products;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sale.Include(s=>s.Customer).ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Index(string search,DateTime date)
        {
            if (date == null) date = DateTime.Today;
            var sale = _context.Sale.Include(s=>s.Customer).Where(s=>s.SaleOrderDate==date)
                .OrderByDescending(s=>s.SaleOrderDate)
                .ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                sale = _context.Sale.Where(s => s.Customer.Name.ToLower().Contains(search)
                                       || s.Description.ToLower().Contains(search)
                                       || s.Status.ToLower().Contains(search)
                                       || s.SaleOrderDate.ToString().Contains(search)
                                       && s.SaleOrderDate == date
                                       ).Include(s=>s.Customer)
                                       .OrderByDescending(s => s.SaleOrderDate)
                                       .ToListAsync();
            }
            return View(await sale);
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name");

            ViewData["StatusValue"] = new List<SelectListItem>{
                new SelectListItem { Value = "Activo", Text = "Activo" },
                new SelectListItem { Value = "Inactivo", Text = "Inactivo" }
            };

            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Description,SaleOrderDate,SaleDeliveryDate,Status,CustomerId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", sale.CustomerId);
            ViewData["StatusValue"] = new SelectList(_context.Vendor, "Value", "Text", sale.Status);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.FindAsync(id);
            
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", sale.CustomerId);
            ViewData["StatusValue"] = new List<SelectListItem>{
                new SelectListItem { Value = "Activo", Text = "Activo" },
                new SelectListItem { Value = "Inactivo", Text = "Inactivo" }
            };
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Description,SaleOrderDate,SaleDeliveryDate,Status,CustomerId")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", sale.CustomerId);
            ViewData["StatusValue"] = new SelectList(_context.Vendor, "Value", "Text", sale.Status);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sale.FindAsync(id);
            _context.Sale.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sale.Any(e => e.Id == id);
        }
    }
}
