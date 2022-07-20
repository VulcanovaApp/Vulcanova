using System;
using System.Threading.Tasks;

namespace Vulcanova.Features.LuckyNumber;

public interface ILuckyNumberService
{
    Task<LuckyNumber> GetLuckyNumberAsync(int accountId, DateTime dateTime);
}