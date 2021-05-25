using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
   public class EntitySiteSetting
    {

        public String BANNER_IMAGE { get; set; }
        public String BANNER_HEADING_1 { get; set; }
        public String BANNER_HEADING_2 { get; set; }
        public String BANNER_HEADING_3 { get; set; }


        public String CONTACT_NO { get; set; }
        public String MAIL { get; set; }
        public String ADDRESS { get; set; }

        public String FOOTER_NOTE { get; set; }
        public String LOGO_NAME { get; set; }
        public String FOOTER_LOGO { get; set; }

        public String FACEBOOK_LINK { get; set; }
        public String TWITTER_LINK { get; set; }
        public String LINKEDIN_LINK { get; set; }

        public String INSTAGRAM_LINK { get; set; }
        public String TELEGRAM_LINK { get; set; }

        public String PRINTEREST_LINK { get; set; }
        public String MEDIUM_LINK { get; set; }
        public String FULL_ADDRESS { get; set; }

        public String TECHNOLOGY_IMAGE { get; set; }


        public Int32 DTLS_PAGE_SETTING_KEY { get; set; }
        public Int32 MAST_PAGE_KEY { get; set; }
        public String PAGE_NAME { get; set; }
        public String PAGE_TITLE { get; set; }
        public String META_DESCRIPTION { get; set; }
        public String META_KEYWORD { get; set; }

        public Int32 MAST_TECHNOLOGY_KEY { get; set; }
        public String TECHNOLOGY_DESC { get; set; }


        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }


        public EntitySiteSetting() { }
    }
}
