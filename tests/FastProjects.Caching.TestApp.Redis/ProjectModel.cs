using FastProjects.SharedKernel;

namespace FastProjects.Caching.TestApp.Redis;

public sealed class ProjectModel : EntityBase<int>, IAggregateRoot
{
    public ProjectModel(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    private ProjectModel()
    {
    }
    
    public string Name { get; set; } = string.Empty;
}
