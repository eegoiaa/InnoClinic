using InnoClinic.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Application.Services.Queries.GetServices;

public static class GetServicesHandler
{
    public static async Task<ServicesResponce> Handle(
        GetServicesQuery query,
        ProfileDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var categoryName = query.Category.ToString();

        var category = await dbContext.ServiceCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == categoryName.ToLower(), cancellationToken);

        if (category == null) return new ServicesResponce();

        var servicesQuery = dbContext.Services
            .AsNoTracking()
            .Include(s => s.Category)
            .Include(s => s.Specialization)
            .Where(s => s.CategoryId == category.Id && s.IsActive);

        if (categoryName.Equals("consultations", StringComparison.OrdinalIgnoreCase))
        {
            var services = await servicesQuery
                .Where(s => s.Specialization != null && s.Specialization.IsActive)
                .ToListAsync(cancellationToken);

            var grouped = services
                .GroupBy(s => s.Specialization!.SpecializationName)
                .Select(g => new ServiceGroupDto(
                    g.Key,
                    g.Select(s => new ServiceDto(s.Id, s.ServiceName, s.Price))));

            return new ServicesResponce(GroupedServices: grouped);
        }

        var flat = await servicesQuery
            .Select(s => new ServiceDto(s.Id, s.ServiceName, s.Price))
            .ToListAsync(cancellationToken);

        return new ServicesResponce(FlatServices: flat);
    }    
}
