using System;
using System.Collections.Generic;
using System.Text;

namespace GlossaryData
{
    public class GlossaryTerm
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
