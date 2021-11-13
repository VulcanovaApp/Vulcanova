using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vulcanova.Core.Data;

namespace Vulcanova.Features.Grades
{
    public class GradesRepository : IGradesRepository
    {
        private readonly AppDbContext _appDbContext;

        public GradesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IReadOnlyList<Grade>> GetGradesForPupilAsync(int accountId, int pupilId)
        {
            return (await _appDbContext.Grades.Where(g => g.PupilId == pupilId && g.AccountId == accountId)
                    .ToListAsync())
                .AsReadOnly();
        }

        public async Task UpdatePupilGradesAsync(int accountId, int pupilId, IEnumerable<Grade> newGrades)
        {
            var previousGrades = await GetGradesForPupilAsync(accountId, pupilId);
            _appDbContext.Grades.RemoveRange(previousGrades);

            newGrades = newGrades.ToArray();

            foreach (var grade in newGrades)
            {
                _appDbContext.ChangeTracker.TrackGraph(
                    grade, node =>
                    {
                        var keyValue = node.Entry.Property("Id").CurrentValue;
                        var entityType = node.Entry.Metadata;

                        var existingEntity = node.Entry.Context.ChangeTracker.Entries()
                            .FirstOrDefault(
                                e => Equals(e.Metadata, entityType)
                                     && Equals(e.Property("Id").CurrentValue, keyValue));

                        if (existingEntity == null)
                        {
                            node.Entry.State = EntityState.Added;
                        }
                    });
            }
            
            await _appDbContext.SaveChangesAsync();
        }
    }
}