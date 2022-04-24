using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vulcanova.Features.Homeworks
{
    public interface IHomeworksRepository
    {
        Task<IEnumerable<Homework>> GetHomeworksForPupilAsync(int accountId, int pupilId);
        Task UpdateHomeworksEntriesAsync(IEnumerable<Homework> entries, int accountId);
    }
}