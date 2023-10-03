using InteractiveNotes.Data.EF;
using InteractiveNotes.Data.Entities;
using InteractiveNotes.Data.Repositories;

namespace TestForPostgre
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var context = new InteractiveNotesDbContext();
            var repo = new NoteRepository(context);

            var data = await repo.GetAllNotesAsync();

            foreach (var item in data)
            {
                Console.WriteLine($"ID: {item.NoteId}, Title: {item.Title}, Content: {item.Content}, Last Modified: {item.CreatingDate:G}");
            }
        }
    }
}