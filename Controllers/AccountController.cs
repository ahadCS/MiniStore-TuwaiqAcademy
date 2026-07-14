using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mini_store.ViewModels;
using System.Threading.Tasks;
using mini_store.Models;
namespace mini_store.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;  //   تسجيل الدخول

         private readonly UserManager<ApplicationUser> _userManager; 

        public AccountController(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = "/") 
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, 
                    model.Password, 
                    model.RememberMe, 
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                
                    return RedirectToAction("Index", "Products");
                }
                
             
                ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو كلمة المرور غير صحيحة.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
}
       
        [HttpGet]
            public IActionResult Register()
            {
                return View();
            }


     [HttpPost]

     public async Task<IActionResult> Register(RegisterViewModel  register)
        {
            if(ModelState.IsValid)
            {

                    ApplicationUser user=new ApplicationUser
                    {
                    
                    UserName=register.Email,
                    Email=register.Email,
                    FirstName=register.FirstName,
                    LastName=register.LastName,
                    };


                    IdentityResult result=await _userManager.CreateAsync(user,register.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                
            }


           
            return View(register);
        }

        
          
    }


    
}