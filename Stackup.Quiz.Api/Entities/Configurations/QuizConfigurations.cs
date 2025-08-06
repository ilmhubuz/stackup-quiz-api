using Microsoft.EntityFrameworkCore;
namespace Stackup.Quiz.Api.Entities.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stackup.Quiz.Api.Entities;

public class QuizConfigurations : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Title).IsUnique();
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.Password).HasMaxLength(6).IsFixedLength();
    }
}