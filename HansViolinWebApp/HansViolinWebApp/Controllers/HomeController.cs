﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HansViolinWebApp.Models;

namespace HansViolinWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HansViolinWebContext _context;


        public HomeController(ILogger<HomeController> logger, HansViolinWebContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/about")]
        public async Task<IActionResult> About()
        {
            BusinessDetail businessDetail;
            try
            {
                businessDetail = _context.BusinessDetails.FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            if(businessDetail == null)
            {
                businessDetail = new BusinessDetail();
                await _context.BusinessDetails.AddAsync(businessDetail);
                await _context.SaveChangesAsync();
            }

            return View(businessDetail);
        }

        [Route("/contact")]
        public async Task<IActionResult> Contact()
        {
            BusinessDetail businessDetail;
            try
            {
                businessDetail = _context.BusinessDetails.FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            if (businessDetail == null)
            {
                businessDetail = new BusinessDetail();
                await _context.BusinessDetails.AddAsync(businessDetail);
                await _context.SaveChangesAsync();
            }

            return View(businessDetail);
        }

        
    }
}
