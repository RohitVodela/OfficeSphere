
namespace OfficeSphere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register services in DI container
            builder.Services.AddScoped<OfficeSphere.Services.Interfaces.IBranchService, OfficeSphere.Services.Implementations.BranchService>();
            builder.Services.AddScoped<OfficeSphere.Services.Interfaces.IDepartmentService, OfficeSphere.Services.Implementations.DepartmentService>();
            builder.Services.AddScoped<OfficeSphere.Services.Interfaces.ITeamService, OfficeSphere.Services.Implementations.TeamService>();
            builder.Services.AddScoped<OfficeSphere.Services.Interfaces.IOfficeService, OfficeSphere.Services.Implementations.OfficeService>();
            // Register EmployeeService last since it depends on TeamService
            builder.Services.AddScoped<OfficeSphere.Services.Interfaces.IEmployeeService, OfficeSphere.Services.Implementations.EmployeeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
