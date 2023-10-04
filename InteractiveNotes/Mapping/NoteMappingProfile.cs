using AutoMapper;
using InteractiveNotes.Data.Entities;
using InteractiveNotes.DTO;

namespace InteractiveNotes.Mapping
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>();
        }
    }
}
