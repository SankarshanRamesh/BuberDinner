using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrasructure;

var builder = WebApplication.CreateBuilder(args);
{

    // Add services to the container.
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrasructure(builder.Configuration);



    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>(); //via middleware
    app.UseExceptionHandler("/error");//via error endpoint and custom problem details factory 
    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
