using CentralValleyBikes.Data;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Helpers;
using CentralValleyBikes.Domain.JWT;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Services;
using CentralValleyBikes.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace CentralValleyBikes.Api
{
    public class Startup
    {
        public IConfiguration config { get; }

        public Startup(IConfiguration configuration)
        {
            config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BikeStoresContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("BikeStoresContext")));

            services.AddCors(opt => opt.AddPolicy("CorsPolicy", c =>
            {
                c.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));


            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMvc()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddMvcCore().AddNewtonsoftJson();

            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAuthenticationService<User, Guid>, AuthenticationService>();

            services.AddScoped<IUserRepository<User, Guid>, UserRepository>();
            services.AddScoped<IUserService<User, Guid>, UserService>();

            services.AddScoped<ICustomerRepository<Customer, int>, CustomerRepository>();
            services.AddScoped<ICustomerService<Customer, int>, CustomerService>();

            services.AddScoped<IProductRepository<Product, int>, ProductRepository>();
            services.AddScoped<IProductService<Product, int>, ProductService>();

            services.AddScoped<IOrderRepository<Order, int>, OrderRepository>();
            services.AddScoped<IOrderService<Order, int>, OrderService>();

            services.AddScoped<IBrandRepository<Brand, int>, BrandRepository>();
            services.AddScoped<IBrandService<Brand, int>, BrandService>();

            services.AddScoped<ICategoryRepository<Category, int>, CategoryRepository>();
            services.AddScoped<ICategoryService<Category, int>, CategoryService>();

            services.AddHttpContextAccessor();

            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            //services.AddScoped<IOrderRepository<Order, int>, OrderRepository>();
            //services.AddScoped<IOrderService<Order, int>, OrderService>();

            //services.AddScoped<IUserRepository<User, Guid>, UserRepository>();
            //services.AddScoped<IUserService<User, Guid>, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
