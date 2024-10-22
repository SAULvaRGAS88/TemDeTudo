using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemDeTudo.Data;
using TemDeTudo.Models;
using TemDeTudo.Models.ViewModels;

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
            //Instanciar um SellerFormViewModel
            // Está instancia terá duas propriedades
            // 1. Seller e lista de departamentos
            var vielModel = new SellerFormVielModel();
            // Carregando o departamento do banco de dado
            vielModel.Depatments = _context.Depatment.ToList();
            return View(vielModel);  

        }

        [HttpPost]
        public IActionResult Create(Seller seller)
        {
            if (seller == null)
            {
                return NotFound();
            }

            //seller.Depatment = _context.Depatment.FirstOrDefault();
            //seller.DepatmentId = seller.Depatment.Id;


            //add obj vendedor ao banco
            _context.Seller.Add(seller);

            //persistir os dados
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller seller = _context.Seller.Include(x => x.Depatment).FirstOrDefault(x => x.Id == id);

            if (seller == null) { return NotFound(); }

            return View(seller);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            Seller seller = _context.Seller.Include(x => x.Depatment).FirstOrDefault(x => x.Id == id);

            if (seller == null) { return NotFound(); }

            return View(seller);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Seller seller = _context.Seller.FirstOrDefault(x => x.Id == id);

            _context.Seller.Remove(seller);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            Seller seller = _context.Seller.FirstOrDefault(x => x.Id == id);

            if (seller == null) { return NotFound(); }

            List<Depatment> depatments = _context.Depatment.ToList();

            SellerFormVielModel sellerFormVielModel = new SellerFormVielModel();

            sellerFormVielModel.Seller = seller;

            sellerFormVielModel.Depatments = depatments;
            
            return View(sellerFormVielModel);
        }

        [HttpPost]
        public IActionResult Edit(Seller seller)
        {
            _context.Seller.Update(seller);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
