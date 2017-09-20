using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SitesApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SitesApp.Pages
{
    public class AboutModel : PageModel
    {
        private readonly MemoryContext ctx;

        public string Message { get; set; }
        public List<Site> Sites { get; set; }

        public AboutModel(MemoryContext ctx)
        {
            this.ctx = ctx;
        }

        public void OnGet()
        {
            Message = "Sites and Sensors.";
            Sites = ctx.Sites.Include("Sensors.Observations").ToList();
        }
    }
}
