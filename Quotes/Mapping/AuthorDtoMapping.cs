using AutoMapper;
using Domain;
using Presentation.Api.DTOs.Author;

namespace Quotes.Mapping;

public sealed class AuthorDtoMapping : Profile
{
    public AuthorDtoMapping()
    {
        CreateMap<Author, AuthorDto>()
            .ForMember(dto => dto.Id, o => o.MapFrom(a => a.Id))
            .ForMember(dto => dto.Name, o => o.MapFrom(a => a.Name));
    }
}
