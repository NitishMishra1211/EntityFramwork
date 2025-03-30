using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace EntityFramwork.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
           
        }
    }
}
