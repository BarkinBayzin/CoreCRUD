using CoreCRUD.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreCRUD.Infrastructure.EntityTypeConfiguration.Abstract
{
    //IEntityTypeConfiguration.cs arayüzünü bize temin edecek yapı Ef Core'dur. Ef Coru2'u bu adımda yükleyebilriiz. Lakin Ef Core, bu proje kullanacağımız veri tabanının paketi içerisinde de bulunmaktadır. Bu tüzden birden fazla paket indirmemek için biz direk çalışacağımız veri tabanının paketini indireceğiz. Böylelikle içerisinde Ef Core'da gelecektir.
    // Microsoft.EntitiyFrameworkCore.SqlServer 5.0.17 aldık. Bu projede SQL Server ile çalışacapğıomızdan bu paketi yükledik. Bu paket ile Ef Core'da geldi, artık kullanabiliriz.,

    //Asp.Net Core'da hep Interface'leri çağıracağız.
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity //BaseMap e göstermiş olduğum generic tipin (<T>) ne olduğunu yada ne olacağını bildirebilirim.
    {
        //Asp.Net projelerinde bu configuration işlemini ctor method içerisinde yapıyorduk. Burada sınıfımızın IENtitytypeConfiguration.cs arayüzünden kalıtım aldığından aşağıdaki methodu implement etmek ve BaseEntity.cs sınıfının konfigürasyonlarını yapmak zorundaydık.
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //Primary key ve idenditity işaretlemek için kullanılır.
           builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime2")
                .IsRequired(); //Asp.Net Projelerinde IsRquired ve IsOptional methodları vardı, burada ISOptional kullanılmamaktadır. IsRequired() methodu boolean True ve False değerleri alarak zorunlu yada boş geçilebilir hale getirmektedir.

            builder.Property(x => x.UpdateDate)
                .HasColumnName("Updatedate")
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(x => x.DeleteDate)
               .HasColumnName("DeleteDate")
               .HasColumnType("datetime2")
               .IsRequired(false);

            builder.Property(x => x.Status)
               .HasColumnName("Status")
               .IsRequired(true);
        }
    }
}
