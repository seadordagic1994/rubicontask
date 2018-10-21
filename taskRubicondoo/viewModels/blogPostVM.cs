using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taskRubicondoo.viewModels
{
    public class blogPostVM
    {
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public string[] tagList { get; set; }

        public bool CheckRequired()
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(body) || String.IsNullOrEmpty(description))
                return true;
            return false;
        }
    }
}