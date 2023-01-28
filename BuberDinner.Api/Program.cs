using BuberDinner.Api.Common.Errors;
using BuberDinner.Application;
using BuberDinner.Infrasructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplication();
    builder.Services.AddInfrasructure(builder.Configuration);
    builder.Services.AddControllers();
    //via exception filter attribute
    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilteAttribute>());

    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();

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

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
