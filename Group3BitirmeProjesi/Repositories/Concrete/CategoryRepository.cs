using Group3BitirmeProjesi.DAL.DbContext;
using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Group3BitirmeProjesi.Repositories.Abstract;

namespace Group3BitirmeProjesi.Repositories.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BitProjeDbContext context) : base(context)
        {
        }





    }
}
