using DataModel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Status = DataModel.Enums.Video.Status;

namespace DataModel
{
    public partial class Video
    {
        public string Id        { get; set; } /*Guid*/
        public string UserId    { get; set; } /*Guid | Publisher*/
        public string TermId    { get; set; } /*Guid*/
        public string ChapterId { get; set; } /*Guid*/
        public string Title     { get; set; }
        public string Duration  { get; set; } /*مدت زمان فیلم برنامه نویسی*/
        public string VideoFile { get; set; } /*مسیر ذخیره شدن فایل فیلم برنامه نویسی*/
        public bool IsFree      { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public Status Status    { get; set; }
    }

    public partial class Video
    {
        public virtual Chapter Chapter { get; set; }
    }

    public partial class Video : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Videos");
            
            /*-------------------------------------------------------*/

            builder.HasKey(Video => Video.Id);
            
            /*-------------------------------------------------------*/

            builder.Property(Video => Video.Id)       .IsRequired();
            builder.Property(Video => Video.UserId)   .IsRequired();
            builder.Property(Video => Video.TermId)   .IsRequired();
            builder.Property(Video => Video.Title)    .IsRequired().HasMaxLength(300);
            builder.Property(Video => Video.Duration) .IsRequired().HasMaxLength(100);
            builder.Property(Video => Video.VideoFile).IsRequired();
            builder.Property(Video => Video.CreatedAt).IsRequired();
            builder.Property(Video => Video.UpdatedAt).IsRequired();
            builder.Property(Video => Video.Status)   .IsRequired().HasConversion(new EnumToNumberConverter<Status, int>());
            
            /*-------------------------------------------------------*/
            
            builder.HasOne(Video => Video.Chapter).WithMany(Chapter => Chapter.Videos).HasForeignKey(Video => Video.ChapterId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}