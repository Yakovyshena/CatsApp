using CatsWebApplication.Controllers;
using CatsWebApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace CatsWebApplication.Tests
{
    public class CatBreedsControllerTest
    {
        readonly WebApplication _app;

        [Fact]
        public void AssertTest()
        {
            Assert.Equal(0, 0);
        }
        [Fact]
        public void GetCatCountPerCatBreedTest()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddDbContext<CatsAPIContext>(option => option.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));
            WebApplication _app = builder.Build();

            Assert.NotNull(_app);

            using var scope = _app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatsAPIContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Assert.False((context.CatBreeds?.Any()).GetValueOrDefault());
            Assert.False((context.Cats?.Any()).GetValueOrDefault());

            CatBreed catBreed = new CatBreed();
            catBreed.Name = "TestBreed";
            catBreed.Description = "TestBreedDescription";
            context.CatBreeds.Add(catBreed);
            context.SaveChanges();
            Assert.True((context.CatBreeds?.Any()).GetValueOrDefault());

            Cat cat = new Cat();
            cat.Name = "TestCat";
            cat.CatBreedId = 1;
            context.Cats.Add(cat);
            context.SaveChanges();
            Assert.True((context.Cats?.Any()).GetValueOrDefault());

        }
    }
}