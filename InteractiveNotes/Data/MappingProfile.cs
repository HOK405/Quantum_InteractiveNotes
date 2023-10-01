using AutoMapper;
using InteractiveNotes.Data.Entities;

namespace InteractiveNotes.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>();
        }
    }
}
