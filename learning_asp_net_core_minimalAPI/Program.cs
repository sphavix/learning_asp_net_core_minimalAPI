using learning_asp_net_core_minimalAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection Container
builder.Services.AddDbContext<ActivityDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeD")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (ActivityDbContext db) =>
await db.Activities.ToListAsync());

app.MapGet("/todoitems/complete", async (ActivityDbContext db) =>
await db.Activities.Where(x => x.IsComplete).ToListAsync());

app.MapGet("/todoitems", async (int id, ActivityDbContext db) =>
await db.Activities.FindAsync(id)
    is Activity activity ?
    Results.Ok(activity) :
    Results.NotFound());

//Post Activity
app.MapPost("/todoitems", async (Activity activity, ActivityDbContext db) =>
{
    db.Activities.Add(activity);
    await db.SaveChangesAsync();
});

//Put Activity
app.MapPut("/todoitems/{id}", async (int id, Activity inputactivity, ActivityDbContext db) =>
{
    var activity = await db.Activities.FindAsync(id);

    if (activity is null) return Results.NotFound();

    activity.Name = inputactivity.Name;
    activity.IsComplete = inputactivity.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

//Delete Activity
app.MapDelete("/todoitems/{id}", async (int id, ActivityDbContext db) =>
{
    if (await db.Activities.FindAsync(id) is Activity activity)
    {
        db.Activities.Remove(activity);
        await db.SaveChangesAsync();
        return Results.Ok(activity);
    }

    return Results.NotFound();
});

app.Run();


