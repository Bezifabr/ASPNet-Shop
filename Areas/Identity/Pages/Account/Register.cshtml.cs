using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LoginPage.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using LoginPage.Data;

namespace LoginPage.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly LoginPageDBContext _loginPageContext;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            LoginPageDBContext loginPageContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _loginPageContext = loginPageContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Nazwa użytkownika")]
            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej {2} znaków, a maksymalnie {1}).", MinimumLength = 3)]
            public string Username { get; set; }

            [Required(ErrorMessage = "Email jest wymagany")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Hasło jest wymagane")]
            [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej {2} znaków, a maksymalnie {1}.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potwierdź hasło")]
            [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Imię")]
            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej {2} znaków, a maksymalnie {1}).", MinimumLength = 1)]
            public string FirstName { get; set; }

            [Display(Name = "Nazwisko")]
            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej {2} znaków, a maksymalnie {1}).", MinimumLength = 1)]
            public string LastName { get; set; }
            
            [Display(Name = "Adres")]
            public Address Address { get; set; }


            [Display(Name = "Wykształcenie")]
            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej {2} znaków, a maksymalnie {1}).", MinimumLength = 1)]
            public string Education { get; set; }

            [Display(Name = "Zainteresowania")]
            public string Hobbies { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = Input.Username, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, Address = Input.Address, Education = Input.Education, Hobbies = Input.Hobbies };
                var result = await _userManager.CreateAsync(user, Input.Password);
                Address address = new Address { City = Input.Address.City, Street = Input.Address.Street, ZipCode = Input.Address.ZipCode};
                address.Users.Add(user);
                _loginPageContext.Add(address);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Potwierdź adres email",
                        $"Potwierdź swój adres email klikając <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>tutaj</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
