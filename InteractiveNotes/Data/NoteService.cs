using AutoMapper;
using InteractiveNotes.Data.Entities;
using InteractiveNotes.Data.Repositories;
using InteractiveNotes.DTO;

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
            var notes = await _noteRepository.GetAllNotesAsync() ?? throw new InvalidOperationException("Retrieved notes collection is null.");
            return notes.Select(note => _mapper.Map<NoteDto>(note) ?? throw new InvalidOperationException("Mapping operation returned null."));
        }

        public async Task<NoteDto> GetNoteByIdAsync(int id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            if (note == null)
                throw new KeyNotFoundException($"Note with ID {id} not found.");

            return _mapper.Map<NoteDto>(note) ?? throw new InvalidOperationException("Mapping operation returned null.");
        }


        public async Task AddNoteAsync(NoteDto noteDto)
        {
            if (noteDto == null)
                throw new ArgumentNullException(nameof(noteDto), "Input noteDto cannot be null.");

            var noteEntity = _mapper.Map<Note>(noteDto) ?? throw new InvalidOperationException("Mapping operation returned null.");
            await _noteRepository.AddNoteAsync(noteEntity);
        }

        public async Task UpdateNoteAsync(NoteDto noteDto)
        {
            if (noteDto == null)
                throw new ArgumentNullException(nameof(noteDto), "Input noteDto cannot be null.");

            if (noteDto.NoteId <= 0)
                throw new ArgumentException("Note ID must be greater than zero.", nameof(noteDto.NoteId));

            var existingNote = await _noteRepository.GetNoteByIdAsync(noteDto.NoteId);
            if (existingNote == null)
                throw new KeyNotFoundException($"Note with ID {noteDto.NoteId} not found.");

            var noteEntity = _mapper.Map<Note>(noteDto);
            await _noteRepository.UpdateNoteAsync(noteEntity);
        }

        public async Task DeleteNoteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Note ID must be greater than zero.", nameof(id));

            var existingNote = await _noteRepository.GetNoteByIdAsync(id);
            if (existingNote == null)
                throw new KeyNotFoundException($"Note with ID {id} not found.");

            await _noteRepository.DeleteNoteAsync(id);
        }
    }
}
