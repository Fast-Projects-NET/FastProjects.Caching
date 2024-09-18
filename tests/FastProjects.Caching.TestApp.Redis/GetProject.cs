using FastEndpoints;
using FastProjects.Endpoints;
using FastProjects.ResultPattern;
using FastProjects.SharedKernel;
using MediatR;

namespace FastProjects.Caching.TestApp.Redis;

// Presentation layer
public sealed class GetProjectRequest
{
    public int Id { get; set; }
}

public sealed class GetProjectEndpoint(IMediator mediator)
    : FastEndpoint<GetProjectRequest,
        ProjectModel,
        GetProjectQuery,
        Result<ProjectModel>,
        ProjectModel>(mediator)
{
    public override void Configure()
    {
        Get("/projects/{Id:int}");
        Version(0);
        AllowAnonymous();

        Summary(s =>
        {
            s.ExampleRequest = new GetProjectRequest { Id = 1 };
            s.ResponseExamples[200] = new ProjectModel(1, "Project 1");
        });
    }

    protected override GetProjectQuery CreateMediatorCommand(GetProjectRequest request) => new(request.Id);

    protected override ProjectModel CreateResponse(ProjectModel data) => data;
}

// Application layer
public sealed record GetProjectQuery(int Id)
    : ICachedQuery<ProjectModel>
{
    public string CacheKey { get; init; } = $"project:{Id}";
    public TimeSpan? Expiration { get; init; } = TimeSpan.FromSeconds(30);
}

public sealed class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, ProjectModel>
{
    public Task<Result<ProjectModel>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = new ProjectModel(request.Id, $"Project {request.Id}");
        return Task.FromResult(Result.Success(project));
    }
}
