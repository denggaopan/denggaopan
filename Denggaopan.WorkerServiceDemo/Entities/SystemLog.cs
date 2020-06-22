using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.WorkerServiceDemo.Entities
{
    public class SystemLog
    {
        public string Id { get; set; }
        public string Module { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
    }

    public class SystemLogConfiguration : IEntityTypeConfiguration<SystemLog>
    {
        public void Configure(EntityTypeBuilder<SystemLog> builder)
        {
            builder.ToTable("SystemLog");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(128);
            builder.Property(x => x.Level).HasMaxLength(50);
            builder.Property(x => x.Message).HasMaxLength(150);
            builder.Property(x => x.Module).HasMaxLength(50);
        }
    }
}
