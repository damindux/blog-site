using blog_backend.Data;
using blog_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_backend.Endpoints;

public static class Endpoints
{
    public static (RouteGroupBuilder, RouteGroupBuilder) MapEndpoints(this WebApplication app)
    {
        var postGroup = app.MapGroup("posts");
        var userGroup = app.MapGroup("users");

        // GET /posts
        postGroup.MapGet("/", async (BlogDbContext dbContext) =>
        {
            var posts = await dbContext.BlogPosts.ToListAsync();
            return Results.Ok(posts);
        });

        // GET /posts/{id}
        postGroup.MapGet("/{id}", async (BlogDbContext dbContext, int id) =>
        {
            var post = await dbContext.BlogPosts.FindAsync(id);
            if (post == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(post);
        });

        // POST /posts
        postGroup.MapPost("/", async (BlogDbContext dbContext, BlogPost post) =>
        {
            dbContext.BlogPosts.Add(post);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/posts/{post.Id}", post);
        });

        // PUT /posts/{id}
        postGroup.MapPut("/{id}", async (BlogDbContext dbContext, int id, BlogPost post) =>
        {
            var existingPost = await dbContext.BlogPosts.FindAsync(id);
            if (existingPost == null)
            {
                return Results.NotFound();
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.UpdatedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return Results.Ok(existingPost);
        });

        // DELETE /posts/{id}
        postGroup.MapDelete("/{id}", async (BlogDbContext dbContext, int id) =>
        {
            var post = await dbContext.BlogPosts.FindAsync(id);
            if (post == null)
            {
                return Results.NotFound();
            }

            dbContext.BlogPosts.Remove(post);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // GET /users
        userGroup.MapGet("/", async (BlogDbContext dbContext) =>
        {
            var users = await dbContext.Users.ToListAsync();
            return Results.Ok(users);
        });

        // GET /users/{id}
        userGroup.MapGet("/{id}", async (BlogDbContext dbContext, int id) =>
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        });

        // POST /users
        userGroup.MapPost("/", async (BlogDbContext dbContext, User user) =>
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/users/{user.Id}", user);
        });

        // PUT /users/{id}
        userGroup.MapPut("/{id}", async (BlogDbContext dbContext, int id, User user) =>
        {
            var existingUser = await dbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return Results.NotFound();
            }

            existingUser.Username = user.Username;
            existingUser.ProfilePicture = user.ProfilePicture;

            await dbContext.SaveChangesAsync();
            return Results.Ok(existingUser);
        });

        // DELETE /users/{id}
        userGroup.MapDelete("/{id}", async (BlogDbContext dbContext, int id) =>
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });
        
        return (postGroup, userGroup);
    }
}