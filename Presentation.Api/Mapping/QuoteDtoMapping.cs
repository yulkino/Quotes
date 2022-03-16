using AutoMapper;
using Domain;
using Presentation.Api.DTOs.Quote;

namespace Quotes.Mapping;

public sealed class QuoteDtoMapping : Profile
{
    public QuoteDtoMapping()
    {
        CreateMap<Quote, QuoteDto>()
            .ForMember(dto => dto.Id, o => o.MapFrom(q => q.Id))
            .ForMember(dto => dto.Text, o => o.MapFrom(q => q.Text));
    }
}
