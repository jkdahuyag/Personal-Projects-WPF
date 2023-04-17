using GlobalComputerSolutionsDb.Configuration;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalComputerSolutionsDb
{
    public class GcsDbContextLite : DbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<GcsTask> GcsTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCost> ProjectCosts { get; set; }
        public DbSet<ProjectSchedule> ProjectSchedules { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SkillInventory> SkillInventories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TaskSkill> TaskSkills { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder op)
        {
            if (!op.IsConfigured)
            {
                op.UseSqlite(
                    @"DataSource=GlobalComputerSolutionsDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new AssignmentConfiguration());
            mb.ApplyConfiguration(new BillConfiguration());
            mb.ApplyConfiguration(new CustomerConfiguration());
            mb.ApplyConfiguration(new EmployeeConfiguration());
            mb.ApplyConfiguration(new GcsTaskConfiguration());
            mb.ApplyConfiguration(new ProjectConfiguration());
            mb.ApplyConfiguration(new ProjectCostConfiguration());
            mb.ApplyConfiguration(new ProjectScheduleConfiguration());
            mb.ApplyConfiguration(new RegionConfiguration());
            mb.ApplyConfiguration(new SkillConfiguration());
            mb.ApplyConfiguration(new SkillInventoryConfiguration());
            mb.ApplyConfiguration(new TaskSkillConfiguration());
            mb.ApplyConfiguration(new WorkLogConfiguration());
        }
    }
}
