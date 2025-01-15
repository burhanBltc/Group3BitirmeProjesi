using Group3BitirmeProjesi.DAL.Entities.Abstract;

namespace Group3BitirmeProjesi.DAL.Entities.Concrete
{
    public class Category : BaseEntity
    {
      
        public Category()
        {
            Products = new List<Product>();
        }
        public List<Product>? Products { get; set; }
    }
}
