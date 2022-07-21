
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.WebApi.Controllers;

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
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory()
    {
        throw new NotImplementedException();
    }

    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<Category>>> SearchForCategory()
    {
        throw new NotImplementedException();
    }

}