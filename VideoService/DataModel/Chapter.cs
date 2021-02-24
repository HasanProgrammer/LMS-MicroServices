using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel
{
    public partial class Chapter
    {
        public string Id     { get; set; } /*Guid*/
        public string TermId { get; set; } /*Guid*/
        public string Title  { get; set; }
    }
    
    public partial class Chapter
    {
        public virtual ICollection<Video> Videos { get; set; }
    }
    
    public partial class Chapter : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable("Chapters");
            
            /*-------------------------------------------------------*/

            builder.HasKey(Chapter => Chapter.Id);
            
            /*-------------------------------------------------------*/

            builder.Property(Chapter => Chapter.Id)    .IsRequired();
            builder.Property(Chapter => Chapter.TermId).IsRequired();
            builder.Property(Chapter => Chapter.Title) .IsRequired().HasMaxLength(300);
            
            /*-------------------------------------------------------*/
            
            builder.HasMany(Chapter => Chapter.Videos).WithOne(Video => Video.Chapter).HasForeignKey(Video => Video.ChapterId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}