using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini_store.Models
{
    public class Product
    {
        [Key]
        public int Id {get ; set;}
        
        [Required(ErrorMessage ="يجب ادخال اسم المنتج")]
        [StringLength(20,ErrorMessage ="اسم المنتج طويل  ")]
        public string Name { get; set; }

         [Required(ErrorMessage = "يجب إدخال السعر")]
         [Range(0.01, 10000.00, ErrorMessage = "يجب أن يكون السعر بين 0.01 و 10000")]
         public decimal Price { get; set; }
        
        [Required(ErrorMessage ="يجب ادخال رابط الصورة ")]
        public string Images { get; set; }

        [Required(ErrorMessage ="يجب الادخال  ")]
        public int CategoryId {get ; set ;}  
     
        [NotMapped]
        public IFormFile? ImageFile {get ; set ;} 


       [ForeignKey("CategoryId")]
       public virtual Categories? Categories { get; set; }
       
         
    }


    
}