using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
   public class EntityHowItWorkUser
    {

        public String HEADING_ONE { get; set; }
        public String DESC_ONE { get; set; }
        public String IMAGE_ONE { get; set; }

        public String HEADING_TWO { get; set; }
        public String DESC_TWO { get; set; }
        public String IMAGE_TWO { get; set; }

        public String HEADING_THREE { get; set; }
        public String DESC_THREE { get; set; }
        public String IMAGE_THREE { get; set; }

        public String HEADING_FOUR { get; set; }
        public String DESC_FOUR { get; set; }
        public String IMAGE_FOUR { get; set; }



        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }


        public EntityHowItWorkUser() { }
    }
}
