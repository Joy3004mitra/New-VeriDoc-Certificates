using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
   public class EntityRegister
    {
        public Int32 DTLS_REGISTER_KEY { get; set; }
        public String FIRSTNAME { get; set; }
        public String LASTNAME { get; set; }
        public String EMAIL { get; set; }
        public String PHONE { get; set; }
        public String COMPANY_NAME { get; set; }
        public String COMPANY_ADDRESS { get; set; }
        public Int32 DTLS_PACKAGE_KEY { get; set; }
        public String COUNTRY { get; set; }
        public String STATE { get; set; }
        public String CITY { get; set; }
        public String ZIP { get; set; }

        public String ALTER_NAME { get; set; }
        public String ALTER_EMAIL { get; set; }
        public String ALTER_PHONE { get; set; }
        public String ALTER_COMPANY_NAME { get; set; }
        public String ALTER_COMPANY_ADDRESS { get; set; }
        public String ALTER_POSITION { get; set; }
        public String ALTER_COUNTRY { get; set; }
        public String ALTER_STATE { get; set; }
        public String ALTER_CITY { get; set; }
        public String ALTER_ZIP { get; set; }
        public String CUSTOMER_ID { get; set; }



        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }


        public EntityRegister() { }
    }
}
