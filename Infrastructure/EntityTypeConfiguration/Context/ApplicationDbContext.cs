using CoreCRUD.Infrastructure.EntityTypeConfiguration.Concrete;
using CoreCRUD.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using CoreCRUD.Models.DTOs;

namespace CoreCRUD.Infrastructure.EntityTypeConfiguration.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            //.Net'te burada connection string ifademizi yazıyorduk. Artık burada connection string yazılmayacak.

            //ASP.Net Core bizleri devamlı Dependency Injection uygulamaya zorlamaktadır. Burada da bunu yapıyoruz. ApplicationDbContext uygulama içeriisnde herhangi bir yerde inject edildiğinde kullanıma hazırlanırken Startup.cs sınfıfındaki options yani özellikleri çağırarak "base" yani "DbContext.cs" sınıfına iletmektedir.

            //Core'da farklı veri tabanları ile çalışabilme imkanımız olduğundan burada bir esneklik temin edilmiştir. Artık connection string appsetting.json dosyasından startup.cs içerisindeki register edilmemiş ApplicationDbContext.cs tarafından çağıralcak ve işleme tabi olacaktır.
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CoreCRUD.Models.DTOs.UpdateCategoryDTO> UpdateCategoryDTO { get; set; }
    }
}
