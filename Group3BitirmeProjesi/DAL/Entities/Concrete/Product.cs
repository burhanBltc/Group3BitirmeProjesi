using Group3BitirmeProjesi.DAL.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group3BitirmeProjesi.DAL.Entities.Concrete
{
    public class Product : BaseEntity
    {
        public double Price { get; set; }
        public string? Currency { get; set; }  // Döviz cinsi (USD, EUR, vs.)
        public string? ImagePath { get; set; }
        public short? Stock { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
