using CoreCRUD.Infrastructure.EntityTypeConfiguration.Context;
using CoreCRUD.Infrastructure.EntityTypeConfiguration.Repositories.Concrete;
using CoreCRUD.Infrastructure.EntityTypeConfiguration.Repositories.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCRUD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Core projelerinde iki prensibe uymak zorundayız. Bunlar DIP ve IoC prensipleri. Buradaki mehot içerisinde IoC Container'ını yöneteceğiz. Asp.Net Core'da IoC Prensibini rahatça uygulamak için built-in olarka IoC Container'ı Core içerisine gömülmüştür. Burada uygulama içerisinde bağımlılığa neden olacak sınfıılarımız register ve resolve edeceğiz. Örneğin LibraryProject'te ApplicationDbContext.cs sınıfımız her bir entity type repository içerisinde new'lemek zorunda kalmıştık. Bu duruma repository nesnemiz ile context nesnemiz arasında tight couple ilişki oluşmaktaydır. Artık Core'da context sınıfımızı buradaki container'ımın içerisine ekleyeceğiz. new'leme ihtiyacı duyduğumuz yerlerde is DI deseniyle ilgili sınıflara inject edeceğiz. Başka bir örnek vermek gerekirse, yine LibraryProject'te içerisinde bulunan repository sınıflarımızı kullanmak istediğimizde onları new'lemek zorunda kalıyorduk. Core'da ise gene ihtiyacç duyulan yerde DI kullanarak interface'sini inject edeceğiz ve kontrolü tersine çevirerek bu bize concrete olan repository nesnesini teslim edecek.
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Uygulama içeriisnde bağımlılığa neden olan bu sınıfları nı built-in IoC Caontainer içeriisnde resolve ediyorum. Böylelikle CategoryReposity.cs sınıfına ihtiyacç duyduğum yerlerde CategoryRepository.cs sınıfımı new'lemek yerine onun atası olan ICategoryRepository.cs sınıfını inject edeceğiz. Buradaki resolve işlemi vasıtasıyla uygulamaya ICategoryRepository.cs arayüzünü inject ettiğpimde bana işlemini yapmayıp ICategoryReposiroty.cs arayüzünü controller içerisinde inject edip uygulamamızı çalıştırdığımızda hata alacaktır.
            //Asp .Net Core içerisinde gömülü olarak bulunan IoC Container bize 3 tane servis life time manager temin edir. Bunlar, AddSingleton(), AddScoped(), AddTransient(). Bu life time manager'larını ihtiyaçlarımıza göre tercih etmeliyiz. Aralarındaki farkları ilerleyen projelerde anlayacağız.
            //AddScoped() => One per request mantığı ile çalışır yani her request başına bir nesne üretir. Talep sonlandığında ürettiği nesneti dispose eder. Kullanıcı başına gelecek farklı taleplerde kullanılabilinir.
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Category}/{action=List}/{id?}");
            });
        }
    }
}
