using AutoMapper;
using InteractiveNotes.Data.Entities;
using InteractiveNotes.Data.Repositories;

namespace InteractiveNotes.Data
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly Mapper _mapper;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;

            var configNote = new MapperConfiguration(cfg => cfg.CreateMap<Note, NoteDto>().ReverseMap());
            _mapper = new Mapper(configNote);
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            return notes.Select(note => _mapper.Map<NoteDto>(note));
        }

        public async Task<NoteDto> GetNoteByIdAsync(int id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            return _mapper.Map<NoteDto>(note);
        }

        public async Task AddNoteAsync(NoteDto note)
        {
            await _noteRepository.AddNoteAsync(_mapper.Map<Note>(note));
        }

        public async Task UpdateNoteAsync(NoteDto note)
        {
            await _noteRepository.UpdateNoteAsync(_mapper.Map<Note>(note));
        }

        public async Task DeleteNoteAsync(int id)
        {
            await _noteRepository.DeleteNoteAsync(id);
        }
    }
}
