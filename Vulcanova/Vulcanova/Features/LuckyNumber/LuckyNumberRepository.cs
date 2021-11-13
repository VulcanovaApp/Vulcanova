using System;
using LiteDB;

namespace Vulcanova.Features.LuckyNumber
{
    public class LuckyNumberRepository : ILuckyNumberRepository
    {
        private readonly LiteDatabase _db;

        public LuckyNumberRepository(LiteDatabase db)
        {
            _db = db;
        }

        public LuckyNumber FindForAccountAndConstituent(int accountId, int constituentId,
            DateTime date)
        {
            return _db.GetCollection<LuckyNumber>().FindOne(l =>
                l.ConstituentId == constituentId 
                && l.AccountId == accountId 
                && date.Date == l.Date.Date);
        }

        public void Add(LuckyNumber luckyNumber)
        {
            _db.GetCollection<LuckyNumber>().Insert(luckyNumber);
        }
    }
}