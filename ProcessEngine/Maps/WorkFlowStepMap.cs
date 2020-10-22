using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessEngine.Domain.WorkFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Maps
{
    public class WorkFlowStepMap : IEntityTypeConfiguration<WorkFlowStep>
    {
        public void Configure(EntityTypeBuilder<WorkFlowStep> builder)
        {
            builder.ToTable("PE_WorkFlowStep");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("wfn_id");
            builder.Property(s => s.WorkFlowId).HasColumnName("wfs_workflow_node_id");
            builder.Property(s => s.WorkFlowNodeId).HasColumnName("wfs_workflow_id");
            builder.Property(s => s.DomainName).HasColumnName("wfs_domain_name");
            builder.Property(s => s.DomainId).HasColumnName("wfs_domain_id");
            builder.Property(s => s.Actor).HasColumnName("wfs_actor");
            builder.Property(s => s.ActorTime).HasColumnName("wfs_actor_time");
            builder.Property(s => s.ActorComment).HasColumnName("wfs_actor_comment");
            builder.Property(s => s.CreateTime).HasColumnName("wfn_step_name");
            builder.Property(s => s.CreateTime).HasColumnName("wfn_createtime");
        }
    }
}
