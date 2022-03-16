using Application.QuotesMediator.Add;
using Application.QuotesMediator.Delete;
using Application.QuotesMediator.Get;
using Application.QuotesMediator.Update;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.DTOs.Quote;
using static Application.Status;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Presentation.Api.Constants;

namespace Quotes.Controllers;

[ApiController]
[Route("/Authors/{authorId}/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public QuotesController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<List<QuoteDto>>> List([FromRoute] Guid authorId)
    {
        var response = await _mediator.Send(new GetQuotesListQuery(authorId));
        return response.Status switch
        {
            DoesNotExist => NotFound(),
            Success => Ok(_mapper.Map<List<QuoteDto>>(response.Result)),
            _ => throw OperationArgumentException
        };
    }

    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<QuoteDto>> Create([FromRoute] Guid authorId, [FromBody] QuoteCreationDto quoteCreationDto)
    {
        var response = await _mediator.Send(new AddQuoteCommand(authorId, quoteCreationDto.Text));
        return response.Status switch
        {
            DoesNotExist => NotFound(),
            SuccessCreated => CreatedAtAction(nameof(Create), _mapper.Map<QuoteDto>(response.Result)),
            _ => throw OperationArgumentException
        };
    }

    [HttpPut("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<QuoteDto>> Edit([FromRoute] Guid authorId, [FromRoute] Guid id, [FromBody] QuoteEditionDto quoteEditionDto)
    {
        var response = await _mediator.Send(new UpdateQuoteCommand(authorId, id, quoteEditionDto.Text));
        return response.Status switch
        {
            DoesNotExist => NotFound(),
            Success => Ok(_mapper.Map<QuoteDto>(response.Result)),
            _ => throw OperationArgumentException
        };
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] Guid authorId, [FromRoute] Guid id)
    {
        var responseStatus = await _mediator.Send(new DeleteQuoteCommand(authorId, id));
        return responseStatus switch
        {
            DoesNotExist => NotFound(),
            OkWithoutContent => NoContent(),
            _ => throw OperationArgumentException
        };
    }
}
