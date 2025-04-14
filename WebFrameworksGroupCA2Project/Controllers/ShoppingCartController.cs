using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.DTOs;
using WebFrameworksGroupCA2Project.Models;

namespace WebFrameworksGroupCA2Project.Controllers {
    public class ShoppingCartController : Controller {
        private readonly WebFrameworksGroupCA2ProjectContext _context;
        private List<ShoppingCartItem> _cartItems;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment environment;


        public ShoppingCartController(WebFrameworksGroupCA2ProjectContext context, UserManager<AppUser> userManager,
            IWebHostEnvironment environment) {
            _context = context;
            _cartItems = new List<ShoppingCartItem>();
            _userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult CheckoutSuccess() {
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult AddToCart(int id) {
            var vinylToAdd = _context.Vinyl.Find(id);

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingCartItem = cartItems.FirstOrDefault(item => item.Vinyl.Id == id);

            if (existingCartItem != null) {
                existingCartItem.Quantity++;
            }
            else {
                cartItems.Add(new ShoppingCartItem {
                    Vinyl = vinylToAdd,
                    Quantity = 1
                });
            }


            HttpContext.Session.Set("Cart", cartItems);

            /// writing temp data

            TempData["CartMessage"] = $"{vinylToAdd.VinylName} vinyl added to cart ";

            return RedirectToAction("ViewCart");
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult ViewCart() {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartViewModel = new ShoppingCartViewModel {
                CartItems = cartItems,
                TotalPrice = (decimal?)cartItems.Sum(item => item.Vinyl.ListPrice * item.Quantity)
            };

            ViewBag.CartMessage = TempData["CartMessage"]; //reading temp data

            return View(cartViewModel);
        }


        [Authorize(Roles = "User, Admin")]
        public IActionResult RemoveItem(int id) {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToRemove = cartItems.FirstOrDefault(item => item.Vinyl.Id == id);

            /// writing temp data
            TempData["CartMessage"] = $"{itemToRemove.Vinyl.VinylName} vinyl removed from cart ";


            if (itemToRemove != null) {
                if (itemToRemove.Quantity > 1) {
                    itemToRemove.Quantity--;
                }
                else {
                    cartItems.Remove(itemToRemove);
                }
            }

            HttpContext.Session.Set("Cart", cartItems);


            return RedirectToAction("ViewCart");
        }


        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public IActionResult UpdateQuantity(int id, int quantity) {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToUpdate = cartItems.FirstOrDefault(item => item.Vinyl.Id == id);

            if (itemToUpdate != null) {
                itemToUpdate.Quantity = quantity;
            }

            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");
        }
        
        [Authorize(Roles = "User, Admin")]
        public IActionResult ClearCart() {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            cartItems.Clear();

            HttpContext.Session.Set("Cart", cartItems);

            TempData["CartMessage"] = "All items removed from cart";

            return RedirectToAction("ViewCart");
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult PurchaseItems() {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var userid = _userManager.GetUserId(HttpContext.User);


            List<OrderItems> orderItems = new List<OrderItems>();

            decimal totalPrice = 0;

            foreach (var item in cartItems) {
                totalPrice = (decimal)(totalPrice + item.Quantity * (decimal?)item.Vinyl.ListPrice);

                orderItems.Add(new OrderItems {
                    Quantity = item.Quantity,
                    PricePerUnit = (int?)(decimal?)item.Vinyl.ListPrice,
                    VinylId = item.Vinyl.Id
                });
            }

            Purchase purchase = new Purchase() {
                PurchaseDate = DateTime.Now,
                Total = totalPrice,
                UserId = userid,
                OrderItems = orderItems
            };

            _context.Purchases.Add(purchase);


            // save chanegs to the database

            _context.SaveChanges();


            // clear cart

            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());

            return RedirectToAction("CheckoutSuccess", "ShoppingCart");
        }
    }
}