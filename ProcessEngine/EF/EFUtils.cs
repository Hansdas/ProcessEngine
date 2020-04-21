using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.EF
{
   public static class EFUtils
    {
        public static IList<string> GetColumnNames(this DBContext dBContext,string className)
        {
           var entityType= dBContext.Model.FindEntityType(className);
            IList<string> columnNames = new List<string>();
            foreach (var item in entityType.GetProperties())
                columnNames.Add(item.Name);
            return columnNames;
        }
    }
}
