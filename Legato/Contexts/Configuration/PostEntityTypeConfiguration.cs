using Legato.Models.PostModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legato.Contexts.Configuration
{
    /// <summary>
    ///     Configuration for Post model on database creating
    /// </summary>
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        /// <summary>
        ///     Configure post model on database creating
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring an IMutableEntityType.</param>
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);
        }
    }
}