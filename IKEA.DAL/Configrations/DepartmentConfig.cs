global using IKEA.DAL.Models.Department;


namespace IKEA.DAL.Configrations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> D)
        {
            D.Property(d => d.id).UseIdentityColumn(10,10);
            D.Property(d => d.Name).HasMaxLength(100).HasColumnType("varchar(20)");
            D.Property(d => d.Description).HasMaxLength(500).HasColumnType("varchar(500)");
            D.Property(d => d.Code).HasMaxLength(20).HasColumnType("varchar(20)");

            D.Property(d=>d.CreatedOn).HasDefaultValueSql("GETDATE()");
            D.Property(d => d.UpdatedOn).HasComputedColumnSql("GETDATE()");//every time update happen the column affected 

            D.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e=>e.DepartmentID)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
