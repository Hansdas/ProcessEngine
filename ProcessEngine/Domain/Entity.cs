using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Domain
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
