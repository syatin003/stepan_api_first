﻿using System.Web;
using System.Web.Mvc;
using stepan_api.Areas;
namespace stepan_api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomExceptionFilter());
        }
    }
}
