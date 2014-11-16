using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform
{
    public class Globals
    {
        public struct WorkflowParam {
            public static string User = "User";
            public static string Title = "Title";
            public static string Body = "Body";
            public static string BlogId = "BlogId";
        }

        public struct Forms {
            public static string CreateBlogPostForm = "CreateBlogPostForm";
        }
    }
}