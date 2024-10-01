using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemDeTudo.Data;
using TemDeTudo.Models;

namespace TemDeTudo.Controllers
{
    public class SellersController : Controller
    {

        private readonly TemDeTudoContext _context;

        public SellersController(TemDeTudoContext context) { _context = context; }
         
        public IActionResult Index()
        {
            var sellers = _context.Seller.Include("Depatment").ToList();
            return View(sellers);
        }

        public IActionResult Create()
        {

            return View();  

        }

        [HttpPost]
        public IActionResult Create(Seller seller)
        {
            if (seller == null)
            {
                return NotFound();
            }

            seller.Depatment = _context.Depatment.FirstOrDefault();
            seller.DepatmentId = seller.Depatment.Id;

            //add obj vendedor ao banco
            _context.Seller.Add(seller);

            //persistir os dados
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
