using Hackaton2025.Infrastructure.Abstractions;

namespace Hackaton2025.Infrastructure.Services;

public class Paginator : IPaginator
{
    public int GetSkip(int page, int pageSize)
    {
        return (page - 1) * pageSize;
    }
}