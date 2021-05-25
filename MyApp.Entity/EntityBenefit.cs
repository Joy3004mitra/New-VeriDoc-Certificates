using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
    public class EntityBenefit
    {

        public Int32 MAST_BENEFITS_KEY { get; set; }
        public String HEADING { get; set; }
        public String DESCRIPTION { get; set; }
        public String BENEFITS_IMG { get; set; }




        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }


        public EntityBenefit() { }
    }
}
