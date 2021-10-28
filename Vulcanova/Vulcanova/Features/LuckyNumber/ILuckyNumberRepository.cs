using System;
using System.Threading.Tasks;

namespace Vulcanova.Features.LuckyNumber
{
    public interface ILuckyNumberRepository
    {
        Task<LuckyNumber> FindForAccountAndConstituentAsync(int accountId, int constituentId, DateTime date);
        Task AddAsync(LuckyNumber luckyNumber);
    }
}