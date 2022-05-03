using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vulcanova.Features.Homework
{
    public interface IHomeworkRepository
    {
        Task<IEnumerable<Homework>> GetHomeworkForPupilAsync(int accountId, int pupilId);
        Task UpdateHomeworkEntriesAsync(IEnumerable<Homework> entries, int accountId);
    }
}