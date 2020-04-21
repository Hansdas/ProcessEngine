using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessEngine.Domain.WorkFlow;
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
            builder.Property(s => s.OrderId).HasColumnName("wfn_orderid");
            builder.Property(s => s.WorkFlowId).HasColumnName("wfn_workflowid");
            builder.Property(s => s.NodeName).HasColumnName("wfn_nodename");
            builder.Property(s => s.Type).HasColumnName("wfn_nodetype");
            builder.Property(s => s.PreviousNodeIds).HasColumnName("wfn_previous_nodes");
            builder.Property(s => s.NodeProperty).HasColumnName("wfn_node_property");
            builder.Property(s => s.ReturnIds).HasColumnName("wfn_return_nodes");
            builder.Property(s => s.Style).HasColumnName("wfn_style");
            builder.Property(s => s.CreateTime).HasColumnName("wfn_createtime");
        }
    }
}
                                                                                                       