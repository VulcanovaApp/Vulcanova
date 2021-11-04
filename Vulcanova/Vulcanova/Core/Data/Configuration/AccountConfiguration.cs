using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vulcanova.Features.Auth.Accounts;

namespace Vulcanova.Core.Data.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasOne(a => a.Pupil);
            builder.HasOne(a => a.Unit);
            builder.HasOne(a => a.ConstituentUnit);
            builder.HasMany(a => a.Periods);
        }
    }
}