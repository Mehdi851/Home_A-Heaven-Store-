﻿using System.Web;
using System.Web.Mvc;

namespace Home_A_Heaven
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}