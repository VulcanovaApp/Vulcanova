using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vulcanova.Features.Exams
{
    public interface IExamsRepository
    {
        Task<IEnumerable<Exam>> GetExamsForPupilAsync(int accountId, DateTime from, DateTime to);

        Task UpdateExamsForPupilAsync(int accountId, IEnumerable<Exam> entries);
    }
}