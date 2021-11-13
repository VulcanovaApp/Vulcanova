using System;
namespace Vulcanova.Features.LuckyNumber
{
    public interface ILuckyNumberRepository
    {
        LuckyNumber FindForAccountAndConstituent(int accountId, int constituentId, DateTime date);
        void Add(LuckyNumber luckyNumber);
    }
}