namespace Hackaton2025.Infrastructure.Abstractions;

public interface IPaginator
{
    int GetSkip(int page, int pageSize);
}