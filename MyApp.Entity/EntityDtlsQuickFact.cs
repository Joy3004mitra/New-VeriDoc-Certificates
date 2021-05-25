using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
   public class EntityDtlsQuickFact
    {
        public Int32 QUICK_FACT_KEY { get; set; }
        public String QUICK_FACT_IMG { get; set; }
        public String QUICK_FACT_DESC { get; set; }


        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }

        public EntityDtlsQuickFact() { }
    }
}
