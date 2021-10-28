using System.Threading.Tasks;

namespace Vulcanova.Features.LuckyNumber
{
    public interface ILuckyNumberService
    {
        Task<int> GetLuckyNumberAsync(long constituentId, string restUri);
    }
}