using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Entity
{
    public class EntityBlog
    {

        public Int32 MAST_BLOG_KEY { get; set; }
        public String MAST_BLOG_TAG_KEY { get; set; }
        public String MAST_BLOG_CATEGORY_KEY { get; set; }
        public String BLOG_HEADING { get; set; }
        public String DESCRIPTION { get; set; }
        public String BLOG_QUESTION { get; set; }
        public String META_DESC { get; set; }
        public String META_KEYWORD { get; set; }
        public String BLOG_IMAGE { get; set; }
        public DateTime BLOG_DATE { get; set; }



        public Int32 ENT_USER_KEY { get; set; }
        public DateTime ENT_DATE { get; set; }
        public DateTime ENT_TIME { get; set; }
        public Int32 EDIT_USER_KEY { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public DateTime EDIT_TIME { get; set; }
        public Byte TAG_ACTIVE { get; set; }
        public Byte TAG_DELETE { get; set; }

        public EntityBlog() { }
    }
}
