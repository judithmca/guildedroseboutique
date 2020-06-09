using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GildedRoseOnlineStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GildedRoseOnlineStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender; //

        public string EmailStatusMessage { get; set; }

        public HomeController(IProductRepository productRepository, IOrderRepository orderRepository, IEmailSender emailSender) //ILogger<HomeController> logger, 
        {
          //  _logger = logger;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var result = _productRepository.GetAll();
            return View(result);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Blog()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var result = _productRepository.GetProductById(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product item)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _productRepository.Add(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var result = _productRepository.GetProductById(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Products/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product item)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _productRepository.Update(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Purchase(int id)
        {
            var result = _productRepository.GetProductById(id);

            if(result.Quantity>0)
            {
                try 
                {
                    var order = _orderRepository.PlaceOrder(id, this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    _productRepository.Deplete(id);
                    OnPostAsync(User.Identity.Name,"Your order at Gilded Rose have being placed: 1-" + result.Name,"Thank you for buying with us. Your order traking number is "+ order.Id.ToString(),"Purchase");
                }
                catch(Exception)
                {


                }
                return View(result);

            }
            else
                TempData["ErrorText"] = String.Format("The product {0} requested is temporally out of stock", result.Name.Trim());
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OnPostAsync(string email,string subject, string message, string page)
        {
            await _emailSender.SendEmailAsync(email, subject, message);

            EmailStatusMessage = "Send test email was successful.";

            return RedirectToPage(page);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _productRepository.GetProductById(id);
            return View(result);
        }
                
        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _productRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
