using System.ComponentModel.DataAnnotations.Schema;

namespace Group3BitirmeProjesi.DAL.Entities.Abstract
{
    public class BaseEntity
    {
        [Column(Order =1)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? ModdifiedDate { get; set;}
    }
}
