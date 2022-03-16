using Application.AuthorMediator.Add;
using Application.AuthorMediator.Delete;
using Application.AuthorMediator.Get;
using Application.AuthorMediator.Update;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.DTOs.Author;
using static Application.Status;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Presentation.Api.Constants;

namespace Quotes.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;


    public AuthorsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(Status200OK)]
    public async Task<ActionResult<List<AuthorDto>>> List()
    {
        var response = await _mediator.Send(new GetAuthorsListQuery());
        return response.Status switch
        {
            Success => Ok(_mapper.Map<List<AuthorDto>>(response.Result)),
            _ => throw OperationArgumentException
        };
    }

    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<AuthorDto>> Create([FromBody] AuthorCreationDto authorCreationDto)
    {
        var response = await _mediator.Send(new AddAuthorCommand(authorCreationDto.Name));
        return response.Status switch
        {
            BadInput => BadRequest("Wrong input"),
            KeyIsOccupied => BadRequest("Author name is occupied!"),
            SuccessCreated => CreatedAtAction(nameof(Create), _mapper.Map<AuthorDto>(response.Result)),
            _ => throw OperationArgumentException
        };
    }

    [HttpPut("{id}")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult<AuthorDto>> Edit([FromRoute] Guid id, [FromBody] AuthorEditionDto authorEditionDto)
    {
        var response = await _mediator.Send(new UpdateAuthorCommand(id, authorEditionDto.Name));
        return response.Status switch
        {
            DoesNotExist => NotFound(),
            KeyIsOccupied => BadRequest(),
            Success => Ok(_mapper.Map<AuthorDto>(response.Result)),
            _ => throw OperationArgumentException
        };
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteAuthorCommand(id));
        return response switch
        {
            DoesNotExist => NotFound(),
            OkWithoutContent => NoContent(),
            _ => throw OperationArgumentException
        };
    }
}
