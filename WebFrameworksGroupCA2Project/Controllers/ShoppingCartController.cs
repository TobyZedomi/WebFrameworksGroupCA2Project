using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers
{
    public class ShoppingCartController : Controller
    {


        private readonly WebFrameworksGroupCA2ProjectContext _context;
        private List<ShoppingCartItem> _cartItems;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment environment;


        public ShoppingCartController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _cartItems = new List<ShoppingCartItem>();
            _userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckoutSuccess()
        {

            return View();
        }

        public IActionResult AddToCart(int id)
        {
            var vinylToAdd = _context.Vinyl.Find(id);

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingCartItem = cartItems.FirstOrDefault(item => item.Vinyl.Id == id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                cartItems.Add(new ShoppingCartItem
                {
                    Vinyl = vinylToAdd,
                    Quantity = 1

                });
            }


            HttpContext.Session.Set("Cart", cartItems);

            TempData["CartMessage"] = $"{vinylToAdd.VinylName} added to cart "; /// writing temp data

            return RedirectToAction("ViewCart");
        }


        public IActionResult ViewCart()
        {

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalPrice = (decimal?)cartItems.Sum(item => item.Vinyl.ListPrice * item.Quantity)
            };

            ViewBag.CartMessage = TempData["CartMessage"]; //reading temp data

            return View(cartViewModel);
        }



        public IActionResult RemoveItem(int id)
        {


            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToRemove = cartItems.FirstOrDefault(item => item.Vinyl.Id == id);

            /// writing temp data
            TempData["CartMessage"] = $"{itemToRemove.Vinyl.VinylName} removed from cart ";


            if (itemToRemove != null) {
                if(itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;

                }
                else
                {
                  
                    cartItems.Remove(itemToRemove);
                }

            }

            HttpContext.Session.Set("Cart", cartItems);


            return RedirectToAction("ViewCart");
        }

       
        public IActionResult PurchaseItems()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var userid = _userManager.GetUserId(HttpContext.User);

            foreach (var item in cartItems)
            {
                // save each item as a purchase

                _context.Purchases.Add(new Purchase
                {

                    VinylId = item.Vinyl.Id,
                    Quantity = item.Quantity,
                    PurchaseDate = DateTime.Now,
                    Total = (decimal?)(item.Vinyl.ListPrice * item.Quantity),
                    UserId = userid
                });

                /// writing temp data

                TempData["CartPurchase"] = $"{item.Vinyl.VinylName} was purchased with a total price of {(decimal?)(item.Vinyl.ListPrice * item.Quantity)}"; 

            }


            // save chanegs to the database

            _context.SaveChanges();


            // clear cart

            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());

            ViewBag.CartPurchase = TempData["CartPurchase"]; //reading temp data

            return RedirectToAction("CheckoutSuccess", "ShoppingCart");
        }




    }
}
