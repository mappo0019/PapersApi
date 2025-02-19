using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PapersDb>(opt => opt.UseInMemoryDatabase("Papers"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

//Users

app.MapGet("/users", async (PapersDb db) =>
    await db.Users.ToListAsync());

app.MapGet("/users/creator", async (PapersDb db) =>
    await db.Users.Where(t => t.Rol).ToListAsync());

app.MapGet("/users/watcher", async (PapersDb db) =>
    await db.Users.Where(t => !t.Rol).ToListAsync());

app.MapGet("/users/{id}", async (int id, PapersDb db) =>
    await db.Users.FindAsync(id)
        is User user
            ? Results.Ok(user)
            : Results.NotFound());

app.MapPost("/users", async (User user, PapersDb db) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id}", async (int id, User inputUser, PapersDb db) =>
{
    var user = await db.Users.FindAsync(id);

    if (user is null) return Results.NotFound();

    user.Name = inputUser.Name;
    user.Rol = inputUser.Rol;
    user.Photo = inputUser.Photo;
    user.Published_Works = inputUser.Published_Works;
    user.Magazines = inputUser.Magazines;
    user.CoWorkers = inputUser.CoWorkers;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/users/{id}", async (int id, PapersDb db) =>
{
    if (await db.Users.FindAsync(id) is User user)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

//Projects

app.MapGet("/projects", async (PapersDb db) =>
    await db.Projects.ToListAsync());

app.MapGet("/projects/buscando_financiacion", async (PapersDb db) =>
    await db.Projects.Where(t => t.Estado==Estado.Buscando_Financiacion).ToListAsync());

app.MapGet("/projects/en_progreso", async (PapersDb db) =>
    await db.Projects.Where(t => t.Estado==Estado.En_Progreso).ToListAsync());

app.MapGet("/projects/finalizado", async (PapersDb db) =>
    await db.Projects.Where(t => t.Estado==Estado.Finalizado).ToListAsync());

app.MapGet("/projects/parado", async (PapersDb db) =>
    await db.Projects.Where(t => t.Estado==Estado.Parado).ToListAsync());

app.MapGet("/projects/{id}", async (int id, PapersDb db) =>
    await db.Projects.FindAsync(id)
        is Project project
            ? Results.Ok(project)
            : Results.NotFound());

app.MapPost("/projects", async (Project project, PapersDb db) =>
{
    db.Projects.Add(project);
    await db.SaveChangesAsync();

    return Results.Created($"/projects/{project.Id}", project);
});

app.MapPut("/projects/{id}", async (int id, Project inputProject, PapersDb db) =>
{
    var project = await db.Projects.FindAsync(id);

    if (project is null) return Results.NotFound();

    project.Name = inputProject.Name;
    project.Author = inputProject.Author;
    project.Estado = inputProject.Estado;
    project.Descripcion = inputProject.Descripcion;
    project.Participantes = inputProject.Participantes;
    project.Presupuesto = inputProject.Presupuesto;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/projects/{id}", async (int id, PapersDb db) =>
{
    if (await db.Projects.FindAsync(id) is Project project)
    {
        db.Projects.Remove(project);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.Run();