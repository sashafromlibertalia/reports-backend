using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reports.BLL.Entities;
using Reports.BLL.Profiles;
using Reports.BLL.Services;
using Reports.DAL.Context;
using Reports.DAL.Repository.Comments;
using Reports.DAL.Repository.Employees;
using Reports.DAL.Repository.Reports;
using Reports.DAL.Repository.Sprints;
using Reports.DAL.Repository.Tasks;
using Reports.PresentationLayer.Auth;

namespace Reports.PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<ReportsContext>(options =>
            {
                options.UseSqlite("Data Source=Database.db");
            });
            services
                .AddAuthentication("EmployeeAuthorization")
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("EmployeeAuthorization", null);
            services.AddAutoMapper(typeof(SprintsMapper));
            services.AddAutoMapper(typeof(TaskMapper));
            services.AddAutoMapper(typeof(CommentMapper));
            services.AddAutoMapper(typeof(EmployeeMapper));
            services.AddAutoMapper(typeof(ReportsMapper));

            services.AddScoped<ISprintsRepository, SprintsRepository>();
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IReportsRepository, ReportsRepository>();

            services.AddScoped<ISprintsService, SprintsService>();
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IReportsService, ReportsService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}