using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> d)
        {
            d.ToTable("Employee");

            d.Property(c => c.FirstName).HasMaxLength(100);
            d.Property(c => c.MiddleName).HasMaxLength(100);
            d.Property(c => c.LastName).HasMaxLength(100);
            d.Property(c => c.Telephone).HasMaxLength(50);

            d.HasOne(c => c.ManagerLink)
                .WithMany(c => c.Employees)
                .HasForeignKey("ManagerId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            var employees = GenerateData();

            d.HasData(employees);
        }

        public List<Employee> GenerateData()
        {
            var list = new List<Employee>();

            var faker = new Faker();
            Randomizer.Seed = new Random(111403);

            for (int i = 1; i <= 100; i++)
            {
                var employee = new Employee();
                employee.EmployeeId = i;
                employee.FirstName = faker.Name.FullName();
                employee.MiddleName = faker.Name.LastName();
                employee.LastName = faker.Name.LastName();
                if (i > 20) employee.ManagerId = faker.Random.Number(1, 20);
                employee.Telephone = faker.Phone.PhoneNumber();
                employee.RegionId = faker.Random.Number(1, 6);

                list.Add(employee);
            }

            return list;
        }
    }
}
