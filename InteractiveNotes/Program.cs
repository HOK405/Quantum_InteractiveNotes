using InteractiveNotes.Data;
using AutoMapper;
using InteractiveNotes.Data.Repositories;
using InteractiveNotes.Data.EF;
using Microsoft.EntityFrameworkCore;
using InteractiveNotes.Mapping;

namespace InteractiveNotes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<InteractiveNotesDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<NoteService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new NoteMappingProfile()); 
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}