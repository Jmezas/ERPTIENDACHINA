﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIS.Business;
using SIS.Entity;
using SIS.Principal.Models;
namespace SIS.Principal.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult Index()
        {
            return View();
        }
    }
}