using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel
{
    /*Property | Fields*/
    public partial class Role : IdentityRole<string>
    {
        
    }

    /*Navigation Property | Relations in code*/
    public partial class Role
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
    
    /*Relation | Configs*/
    public partial class Role : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
                
            /*---------------------------------------------------*/
            
            builder.HasMany(Role => Role.UserRoles).WithOne(UR => UR.Role).HasForeignKey(UR => UR.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}