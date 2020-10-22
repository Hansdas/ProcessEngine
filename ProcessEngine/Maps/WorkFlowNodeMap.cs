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
            builder.ToTable("PE_WorkFlowNode");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("wfn_id");
            builder.Property(s => s.OrderId).HasColumnName("wfn_order_num");
            builder.Property(s => s.WorkFlowId).HasColumnName("wfn_workflow_id");
            builder.Property(s => s.NodeName).HasColumnName("wfn_node_name");
            builder.Property(s => s.OperationName).HasColumnName("wfn_operation_name");
            builder.Property(s => s.Type).HasColumnName("wfn_node_type");
            builder.Property(s => s.PreviousNodeIds).HasColumnName("wfn_previous_nodes");
            builder.Property(s => s.NodeProperty).HasColumnName("wfn_node_property");
            builder.Property(s => s.ReturnIds).HasColumnName("wfn_return_nodes");
            builder.Property(s => s.Style).HasColumnName("wfn_style");
            builder.Property(s => s.ConditionXml).HasColumnName("wfn_condition_xml");
            builder.Property(s => s.PreviousAllFinish).HasColumnName("wfn_previous_all_finish");
            builder.Property(s => s.CreateTime).HasColumnName("wfn_createtime");
        }
    }
}
                                                                                                       