
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Exceptions;

namespace TaggTimeline.Service.Handlers;

public class AddCategoryToTaggHandler : IRequestHandler<AddCategoryToTaggCommand, TaggModel>
{
    private readonly IBaseRepository<Tagg> _taggRepository;
    private readonly IBaseRepository<Category> _categoryRepository;
    private readonly ITransactionWrapper _transactionWrapper;
    private readonly IMapper _mapper;

    public AddCategoryToTaggHandler(IBaseRepository<Tagg> taggRepository, IBaseRepository<Category> categoryRepository, ITransactionWrapper transactionWrapper, IMapper mapper)
    {
        _taggRepository = taggRepository;
        _categoryRepository = categoryRepository;
        _transactionWrapper = transactionWrapper;
        _mapper = mapper;
    }

    public async Task<TaggModel> Handle(AddCategoryToTaggCommand request, CancellationToken cancellationToken)
    {
        await using var t = await _transactionWrapper.Begin();

        var tagg = await _taggRepository.GetByIdWithNavigationProperties(request.TaggId, tagg => tagg.Instances, tagg => tagg.Categories);
        if(tagg is null)
            throw new EntityNotFoundException($"Couldn't find a Tagg with id:{request.TaggId}");

        var category = await _categoryRepository.GetById(request.CategoryId);
        if(category is null)
            throw new EntityNotFoundException($"Couldn't find a Category with id:{request.CategoryId}");

        tagg.Categories = tagg.Categories.Append(category).ToList();

        await _taggRepository.SaveChanges(CancellationToken.None);

        await t.Commit();

        var taggModel = _mapper.Map<TaggModel>(tagg);

        return taggModel;
    }
}
