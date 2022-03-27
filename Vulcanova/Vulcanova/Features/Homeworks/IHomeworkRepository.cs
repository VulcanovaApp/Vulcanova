using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vulcanova.Features.Homeworks
{
    public interface IHomeworksRepository
    {
        Task<IEnumerable<HomeworkEntry>> GetHomeworksForPupilAsync(int accountId, int pupilId);
        Task UpdateHomeworksEntriesAsync(IEnumerable<HomeworkEntry> entries, int accountId);
    }
}