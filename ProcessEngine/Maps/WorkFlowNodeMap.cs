using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessEngine.Domain.WokrFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Maps
{
    public class WorkFlowNodeMap : IEntityTypeConfiguration<WorkFlowNode>
    {
        public void Configure(EntityTypeBuilder<WorkFlowNode> builder)
        {
            builder.ToTable("WFN_WorkFlowNode");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("wfn_id");
            builder.Property(s => s.WorkFlowId).HasColumnName("wfn_workflowid");
            builder.Property(s => s.NodeName).HasColumnName("wfn_nodename");
            builder.Property(s => s.WorkFlowNodeType).HasColumnName("wfn_nodetype");
            builder.Property(s => s.PreviousNode).HasColumnName("wfn_previous_node");
            builder.Property(s => s.NodeProperty).HasColumnName("wfn_node_property");
            builder.Property(s => s.CreateTime).HasColumnName("wf_createtime");
        }
    }
}
