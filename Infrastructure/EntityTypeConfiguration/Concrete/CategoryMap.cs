using CoreCRUD.Infrastructure.EntityTypeConfiguration.Abstract;
using CoreCRUD.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreCRUD.Infrastructure.EntityTypeConfiguration.Concrete
{
    public class CategoryMap:BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(300).IsRequired(true);
            base.Configure(builder);
        }
    }
}
