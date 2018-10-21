using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taskRubicondoo.Helper
{
    public static class helperClasses
    {
        //create slug from post title
        public static string createSlug(string s)
        {
            string newSlug = new string(s.Where(c => !char.IsPunctuation(c)).ToArray()).ToString();
            return newSlug.Replace(" ", "-").ToLower();
        }


    }
}