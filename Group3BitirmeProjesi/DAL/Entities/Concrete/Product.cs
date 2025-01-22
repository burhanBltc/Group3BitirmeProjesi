using Group3BitirmeProjesi.DAL.Entities.Abstract;
using Group3BitirmeProjesi.DAL.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group3BitirmeProjesi.DAL.Entities.Concrete
{
    public class Product : BaseEntity
    {
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat sıfırdan büyük olmalıdır.")]
        public decimal Price { get; set; }

        // Currency, Enum türünde
        public Currency Currency { get; set; } = Currency.TL;  // Varsayılan değer TL
        public string? ImagePath { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
        public int? Stock { get; set; }

  
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
