using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environmental_Monitoring.Model;

namespace Environmental_Monitoring.Model
{
    public class PagedResult
    {
        public DataTable Data { get; set; }
        public int TotalCount { get; set; }
    }
}
