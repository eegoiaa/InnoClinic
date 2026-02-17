using InnoClinic.Services.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Services.Application.Services.Queries.GetServices;

public static class GetServicesHandler
{
    public static async Task<ServicesResponse> Handle(
        GetServicesQuery query,
        ServicesDbContext dbContext,
        CancellationToken cancellationToken
        )
    {
        var categoryName = query.Category.ToString();
        var category = await dbContext.ServiceCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == categoryName.ToLower(), cancellationToken);

        if (category == null) return new ServicesResponse();

        var servicesQuery = dbContext.Services
            .AsNoTracking()
            .Where(s => s.CategoryId == category.Id && s.IsActive);

        if (query.Category == ServiceCategoryType.Consultations)
        {
            var services = await servicesQuery.ToListAsync(cancellationToken);

            var lookups = await dbContext.SpecializationReferences
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var grouped = services
                .GroupBy(s => lookups.FirstOrDefault(l => l.Id == s.SpecializationId)?.Name ?? "Other")
                .Select(g => new ServiceGroupDto(
                    g.Key,
                    g.Select(s => new ServiceDto(
                        s.Id,
                        s.ServiceName,
                        s.Price))
                    ));

            return new ServicesResponse(GroupedServices: grouped);
        }

        var flat = await servicesQuery
            .Select(f => new ServiceDto(f.Id, f.ServiceName, f.Price))
            .ToListAsync(cancellationToken);

        return new ServicesResponse(FlatServices: flat);
    }
}
