using CoreCRUD.Models.Entities.Abstract;

namespace CoreCRUD.Models.Entities.Concrete
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
