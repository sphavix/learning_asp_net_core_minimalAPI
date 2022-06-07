using learning_asp_net_core_minimalAPI.Models;
using learning_asp_net_core_minimalAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection Container
builder.Services.AddDbContext<ActivityDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeD")));
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", ([FromServices] IRepositoryService db) => db.GetAllActivities());

app.MapGet("/todoitems/complete", ([FromServices] IRepositoryService db) => db.GetCompletedActivity());

app.MapGet("/todoitems", (int id, [FromServices] IRepositoryService db) =>
{
    var activity = db.GetActivity(id);
});

//Post Activity
app.MapPost("/todoitems", (Activity activity, [FromServices] IRepositoryService db) =>
{
    db.AddActivity(activity);   
});

//Put Activity
app.MapPut("/todoitems/{id}", (Activity activity, [FromServices] IRepositoryService db) =>
{
    db.UpdateActivity(activity);
});

//Delete Activity
app.MapDelete("/todoitems/{id}", (int id, [FromServices] IRepositoryService db) =>
{
    db.DeleteActivity(id);
});

app.Run();


