using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini_store.Models
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "يجب إدخال وصف المنتج")]
        [StringLength(500, ErrorMessage = "الوصف لا يمكن أن يتجاوز 500 حرف")]
        public string Description { get; set; }

        [Required(ErrorMessage = "يجب تحديد بلد المنشأ")]
        public string MadeIn { get; set; }

        [Required(ErrorMessage = "يجب إدخال الكمية المتاحة في المخزن")]
        [Range(0, 10000, ErrorMessage = "الكمية يجب أن تكون بين 0 و 10000")]
        public int StockQuantity { get; set; }

        // اسم الصورة كنص (يُخزن بقاعدة البيانات)
        public string? Image { get; set; }

        // حقل مؤقت لاستقبال الملف المرفوع، غير مرتبط بقاعدة البيانات
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}