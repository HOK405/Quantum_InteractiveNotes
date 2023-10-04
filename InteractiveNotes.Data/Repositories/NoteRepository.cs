using InteractiveNotes.Data.EF;
using InteractiveNotes.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNotes.Data.Repositories
{
    /// <summary>
    /// CRUD operations with Note table
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        private readonly InteractiveNotesDbContext _context;

        public NoteRepository(InteractiveNotesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task AddNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Note note)
        {
            var existingNote = await _context.Notes.FindAsync(note.NoteId);
            if (existingNote != null)
            {
                _context.Entry(existingNote).CurrentValues.SetValues(note);
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}
