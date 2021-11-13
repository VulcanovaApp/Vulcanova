using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Features.Grades;

namespace Vulcanova.Core.Data.Configuration
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasOne<Account>()
                .WithMany()
                .HasForeignKey(g => g.AccountId);

            builder.HasOne(g => g.Column);
        }
    }
}