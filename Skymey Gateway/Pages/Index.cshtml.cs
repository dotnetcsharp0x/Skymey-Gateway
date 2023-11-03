using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Skymey_Gateway.Actions.XML;
using Skymey_Gateway.Data;
using Skymey_Gateway.Models;
using Skymey_Gateway.Models.Tables.User;
using System.Diagnostics;
using System.Xml;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        ApplicationContext _context;
        private readonly ILogger<IndexModel> _logger;
        public List<ProcessesListShow?> ProcessesList { get; private set; } = new();

        public IndexModel(ApplicationContext db, ILogger<IndexModel> logger)
        {
            _context = db;
            _logger = logger;
        }

        public void OnGet()
        {
            ProcessesList = new XMLSettings().GetXmlData();
        }
    }
}
