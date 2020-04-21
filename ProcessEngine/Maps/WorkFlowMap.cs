using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessEngine.Domain.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Maps
{
    public class WorkFlowMap : IEntityTypeConfiguration<WorkFlow>
    {
        public void Configure(EntityTypeBuilder<WorkFlow> builder)
        {
            builder.ToTable("WF_WorkFlow");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("wf_id");
            builder.Property(s => s.Name).HasColumnName("wf_name");
            builder.Property(s => s.DomainName).HasColumnName("wf_domain");
            builder.Property(s => s.IsDisable).HasColumnName("wf_disable");
            builder.Property(s => s.Version).HasColumnName("wf_version");
            builder.Property(s => s.CreateTime).HasColumnName("wf_createtime");

        }
    }
}
