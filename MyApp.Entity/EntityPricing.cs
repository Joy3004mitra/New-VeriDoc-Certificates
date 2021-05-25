using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
    public class EntityPricing
    {


        public Int32 DTLS_PACKAGE_KEY { get; set; }
        public String PACKAGE_NAME { get; set; }
        public String PACKAGE_DESC { get; set; }
        public Double PACKAGE_AMOUNT { get; set; }
        public String MONTHLY_PACKAGE { get; set; }


        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }


        public EntityPricing() { }
    }
}
