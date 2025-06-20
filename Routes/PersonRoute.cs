using Microsoft.EntityFrameworkCore;
using WebAPI_Person.Data;
using WebAPI_Person.Models;

namespace WebAPI_Person.Routes
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {
            var route = app.MapGroup("person");

            route.MapPost("", async (PersonRequest req, PersonContext context) =>
            {
                var person = new PersonModel(req.name);
                await context.AddAsync(person);
                await context.SaveChangesAsync(); // Commit
                return Results.Ok(person);
            });

            route.MapGet("", async (PersonContext context) => 
            {
                var people = await context.People.ToListAsync();
                return Results.Ok(people);
            });

            route.MapPut("{id:guid}", async (Guid id, PersonRequest req, PersonContext context) =>
            {
                var person = await context.People.FirstOrDefaultAsync(x => x.Id == id); 

                if (person == null)
                {
                    return Results.NotFound();
                } else
                {
                    person.ChangeName(req.name);
                    await context.SaveChangesAsync();
                    return Results.Ok(person);
                }
            });

            route.MapDelete("{id:guid}", async (Guid id, PersonContext context) =>
            {
                var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                if (person == null)
                {
                    return Results.NotFound();
                }

                context.People.Remove(person);
                await context.SaveChangesAsync();
                return Results.Ok(person);
            });
        }
    }
}
