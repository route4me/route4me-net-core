using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route4MeDB.ApplicationCore;

namespace Route4MeDB.Infrastructure.Data
{
    public partial class ConfigureEntity
    {
        public void ConfigureAudit(EntityTypeBuilder<Audit> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property<Guid>("Id")
               .IsConcurrencyToken(true)
               .IsRowVersion()
               .ValueGeneratedOnAdd();

            builder.Property<int>("ChangeType");

            builder.Property<string>("ColumnName");

            builder.Property<DateTime>("DateTime");

            builder.Property<string>("Entity");

            builder.Property<Guid?>("EntityId");

            builder.Property<string>("NewValue");

            builder.Property<string>("OldValue");

            builder.Property<string>("User");

            builder.HasKey("Id");

            builder.ToTable("Audits");
        }
    }
}
