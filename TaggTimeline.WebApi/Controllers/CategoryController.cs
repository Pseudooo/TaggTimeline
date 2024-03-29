
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaggTime.Service.Commands;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:Guid?}")]
    public async Task<ActionResult<CategoryModel>> GetCategory(Guid id)
    {
        var query = new GetCategoryByIdQuery()
        {
            Id = id,
        };
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CategoryPreviewModel>>> GetAllCategories()
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryModel>> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Created("GetTagg", result);
    }

    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<CategoryPreviewModel>>> SearchForCategory([FromBody] SearchForCategoriesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

}
