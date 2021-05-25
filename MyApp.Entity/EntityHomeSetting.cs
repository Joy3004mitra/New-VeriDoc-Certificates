using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
   public class EntityHomeSetting
    {


        public String BANNER_HEADING { get; set; }
        public String BANNER_DESC { get; set; }
        public String BANNER_DESC_1 { get; set; }
        public String BANNER_IMG { get; set; }
        public String BANNER_NOTE { get; set; }
        public String GOOGLEPLAY_LINK { get; set; }
        public String PATENTED_HEADING { get; set; }

        public String PATENTED_IMG { get; set; }
        public String APPSTORE_LINK { get; set; }

        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public Int32 ENT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }

        public EntityHomeSetting() { }


    }
}
