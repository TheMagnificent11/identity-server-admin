using System;
using IdentityServer.Common.Constants.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(i => i.Id);

            builder.Property(i => i.GiveName)
                .IsRequired()
                .HasMaxLength(UserFieldMaxLengths.GivenName);

            builder.Property(i => i.Surname)
                .IsRequired()
                .HasMaxLength(UserFieldMaxLengths.Surname);

            builder.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(UserFieldMaxLengths.Email);

            builder.Property(i => i.UserName)
                .IsRequired()
                .HasMaxLength(UserFieldMaxLengths.Email);
        }
    }
}
