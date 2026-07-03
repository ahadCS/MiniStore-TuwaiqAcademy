using System.ComponentModel.DataAnnotations;

namespace mini_store.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "الاسم الأول")]
        [Required(ErrorMessage ="الإسم الأول مطلوب")]
        [StringLength(50,ErrorMessage =" بيانات الحقل طويلة جداً  الطول لا يتجاوز 50 حرف")]
        public string FirstName { get; set; }

        [Display(Name = "الاسم الثاني")]
        [Required(ErrorMessage ="الإسم الثاني مطلوب")]
        [StringLength(50,ErrorMessage =" بيانات الحقل طويلة جداً  الطول لا يتجاوز 50 حرف")]
        public string LastName { get; set; }

        [Display(Name = "البريد الإلكتروني")]
        [Required(ErrorMessage =" البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "يرجى إدخال بريد الإلكتروني بشكل صحيح ")]
        public string Email { get; set; }

        [Display(Name = "كلمة المرور")]
        [Required(ErrorMessage ="كلمة المرور مطلوبة ")]
        [StringLength(100,ErrorMessage ="يجب ان تكون على الأقل بطول 8 رموز", MinimumLength =8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تأكيد كلمة المرور")]
        [Required(ErrorMessage ="  تأكيد كلمة المرور ")]
        [Compare("Password",ErrorMessage ="كلمات المرور غير متطابقتين")]
        [DataType(DataType.Password)]
        public string ConfirmPaasword { get; set; }
    }
}