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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            var product = _context.Product.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                product = _context.Product.Where(s => s.Name.ToLower().Contains(search)
                                       //|| s.CostPrice.ToString().Contains(search)
                                       || s.Status.ToLower().Contains(search)
                                       //|| s.Price.ToString().Contains(search)
                                      // || s.Quantity.ToString().Contains(search)

                                       ).ToListAsync();
            }

            return View(await product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["StatusValue"] = new List<SelectListItem>{
                new SelectListItem { Value = "Activo", Text = "Activo" },
                new SelectListItem { Value = "Inactivo", Text = "Inactivo" }
            };
            ViewData["TypeValue"] = new List<SelectListItem>{
                new SelectListItem { Value = "Producto", Text = "Producto" },
                new SelectListItem { Value = "Servicio", Text = "Servicio" }
            };
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Type,Name,Price,CostPrice,Quantity,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Code = Guid.NewGuid();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusValue"] = new SelectList(_context.Product, "Value", "Text", product.Status);
            ViewData["TypeValue"] = new SelectList(_context.Product, "Value", "Text", product.Type);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            product.CostPrice = Convert.ToInt32(product.CostPrice);
            product.Price = Convert.ToInt32(product.Price);

            ViewData["StatusValue"] = new List<SelectListItem>{
                new SelectListItem { Value = "Activo", Text = "Activo" },
                new SelectListItem { Value = "Inactivo", Text = "Inactivo" }
            };
            ViewData["TypeValue"] = new List<SelectListItem>{
                new SelectListItem { Value = "Producto", Text = "Producto" },
                new SelectListItem { Value = "Servicio", Text = "Servicio" }
            };

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Type,Name,Price,CostPrice,Quantity,Status")] Product product)
        {
            ViewData["StatusValue"] = new SelectList(_context.Product, "Value", "Text", product.Status);
            ViewData["TypeValue"] = new SelectList(_context.Product, "Value", "Text", product.Type);
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
