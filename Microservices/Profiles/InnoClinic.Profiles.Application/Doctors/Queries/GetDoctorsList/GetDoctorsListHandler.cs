using InnoClinic.Profiles.Domain.Enums;
using InnoClinic.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;

public class GetDoctorsListHandler
{
    public static async Task<IEnumerable<DoctorsListDto>> Handle(
        GetDoctorsListQuery query,
        ProfileDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        var dbQuery = dbContext.Doctors
            .Include(d => d.Specialization)
            .Include(d => d.Office)
            .AsNoTracking()
            .AsQueryable();

        dbQuery = dbQuery.Where(d => d.Status == DoctorStatus.AtWork);

        if (!string.IsNullOrWhiteSpace(query.SearchByName))
        {
            var search = query.SearchByName.ToLower();
            dbQuery = dbQuery.Where(d => d.FullName.ToLower().Contains(search));
        }

        if (query.SpecializationId.HasValue)
            dbQuery = dbQuery.Where(d => d.SpecializationId == query.SpecializationId.Value);

        if (query.OfficeId.HasValue)
            dbQuery = dbQuery.Where(d => d.OfficeId == query.OfficeId.Value);

        return await dbQuery.Select(d => new DoctorsListDto (
            d.Id,
            d.FullName,
            d.Specialization != null ? d.Specialization.SpecializationName : "No Specialization",
            d.SpecializationId,
            d.Office != null ? d.Office.Address : "No Office",
            d.OfficeId,
            timeProvider.GetUtcNow().Year - d.CareerStartYear + 1,
            d.AccountId
            )).ToListAsync(cancellationToken);
    }
}

//private readonly ProfileDbContext _dbContext;
//private readonly TimeProvider _timeProvider;

//public GetDoctorsListHandler(ProfileDbContext dbContext, TimeProvider timeProvider)
//{
//    _dbContext = dbContext;
//    _timeProvider = timeProvider;
//}
//public async Task<IEnumerable<DoctorsListDto>> Handle(GetDoctorsListQuery request, CancellationToken cancellationToken)
//{
//    var query = _dbContext.Doctors
//        .Include(d => d.Specialization)
//        .Include(d => d.Office)
//        .AsNoTracking()
//        .AsQueryable();

//    query = query.Where(d => d.Status == DoctorStatus.AtWork);

//    if (!string.IsNullOrWhiteSpace(request.SearchByName))
//    {
//        var search = request.SearchByName.ToLower();
//        query = query.Where(d => d.FullName.ToLower().Contains(search));
//    }    

//    if (request.SpecializationId.HasValue)
//        query = query.Where(d => d.SpecializationId == request.SpecializationId.Value);

//    if (request.OfficeId.HasValue)
//        query = query.Where(d => d.OfficeId == request.OfficeId.Value);

//    var result = await query.Select(d => new DoctorsListDto(
//        d.Id,
//        $"{d.LastName} {d.FirstName} {d.MiddleName}".Trim(),
//        d.Specialization != null ? d.Specialization.SpecializationName : "No Specialization",
//        d.Office != null ? d.Office.Address : "No Office",
//        _timeProvider.GetUtcNow().Year - d.CareerStartYear + 1,
//        d.AccountId
//        )).ToListAsync(cancellationToken);


//    return result;
//}