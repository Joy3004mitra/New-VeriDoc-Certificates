using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
  public  class EntitySysUser
  {
        public Int32 USER_KEY { get; set; }
        public String USER_NAME { get; set; }
        public String USER_PASSWORD { get; set; }
        public Int32 USER_TYPE_KEY { get; set; }

        public Int32 MAST_HRD_PERSONNEL_KEY { get; set; }
        public Byte TAG_ACCESS_CHANGED { get; set; }
        public String TAG_COMPANY_ACCESS { get; set; }
        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }
        public Int16 COMPANY_KEY { get; set; }

        public String PERSONNEL_NAME { get; set; }
        public String USER_TYPE { get; set; }
        public String COMPANY_NAME { get; set; }
        public String COMPANY_TAG { get; set; }
        public String GSTIN_NO { get; set; }
        public String PROP_NAME { get; set; }
        public String ADDRESS { get; set; }
        public String PINNO { get; set; }
        public String PHONENO { get; set; }
        public String MOBILE { get; set; }
        public String EMAIL { get; set; }
        public String START_YEAR { get; set; }
        public String END_YEAR { get; set; }

        public DateTime ACCT_YEAR_START { get; set; }
        public DateTime ACCT_YEAR_END { get; set; }
        public String TAG_PAGE_REFRESH { get; set; }

        public EntitySysUser() { }
  }
}
