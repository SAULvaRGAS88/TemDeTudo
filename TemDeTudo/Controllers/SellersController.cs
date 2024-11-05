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
            //Filtra os vendedores que ganham menos de 10k
            var trainers = sellers.Where(x => x.Salary <= 10000).ToList();

            // retorna lista de vendedores ordenadamento pelo nome e por salario decrescente
            var trainersPLus = sellers.OrderBy(x => x.Name).ThenBy(x => x.Salary).ToList();
            return View(trainersPLus);
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

        public IActionResult Reports()
        {
            //popular a lista de objeto vendedores, trazendfo as infos do departamento de cada vendedor
            var sellers = _context.Seller.Include("Depatment").ToList();

            ViewData["TotalFolhaPagamento"] = sellers.Sum(s => s.Salary );

            ViewData["MaiorSalario"] = sellers.Max(s => s.Salary);

            ViewData["MenorSalario"] = sellers.Min(s => s.Salary);

            ViewData["MediaSalarios"] = sellers.Average(s => s.Salary);

            ViewData["Ricos"] = sellers.Count(s => s.Salary > 30000);

            return View();
        }
    }
}
