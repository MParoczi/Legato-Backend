using System.Collections.Generic;
using Legato.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Legato.Contexts.Configuration
{
    /// <summary>
    ///     Configuration for User model on database creating
    /// </summary>
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        /// <summary>
        ///     Configure user model on database creating
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an IMutableEntityType.</param>
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(e => e.Genres)
                .HasConversion(
                    genres => JsonConvert.SerializeObject(genres,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }),
                    genres => JsonConvert.DeserializeObject<List<string>>(genres,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
        }
    }
}