using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vulcanova.Features.LuckyNumber;

namespace Vulcanova.Core.Data.Configuration
{
    public class LuckyNumberConfiguration : IEntityTypeConfiguration<LuckyNumber>
    {
        public void Configure(EntityTypeBuilder<LuckyNumber> builder)
        {
            builder.HasKey("_id");
        }
    }
}