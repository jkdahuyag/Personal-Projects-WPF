using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> d)
    {
        d.ToTable("Region");

        d.Property(c => c.Name).HasMaxLength(20);

        var regions = GenerateData();

        d.HasData(regions);

    }

    private List<Region> GenerateData()
    {
        var list = new List<Region>();

        var available = new List<string>{"Northwest(NW)", "Southwest(SW)", "Midwest North(MN)", " Midwest South(MS)", "Northeast(NE)", "Southeast(SE)"};
        for (int i = 1; i <= 6; i++)
        {
            var region = new Region();
            region.RegionId = i;
            region.Name = available[i-1];
            list.Add(region);
        }

        return list;
    }
}