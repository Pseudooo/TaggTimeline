
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Exceptions;

namespace TaggTime.Service.Handlers;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{
    private readonly IBaseRepository<Category> _baseRepository;

    public GetCategoryByIdHandler(IBaseRepository<Category> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _baseRepository.GetById(request.Id);

        if(category is null)
            throw new EntityNotFoundException($"Couldn't find Category with id:{request.Id}");

        return category;
    }
}
