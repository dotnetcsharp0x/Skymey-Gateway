using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Skymey_Gateway.Data;
using Skymey_Gateway.Interfaces.JWT;
using Skymey_Gateway.Models;
using System.Diagnostics;

namespace Skymey_Gateway.Controllers
{
    [ApiController]
    [Route("api/Proc")]
    public class GatewayController : ControllerBase
    {
        private readonly JWTSettings _options;
        private readonly ILogger<GatewayController> _logger;
        private readonly ApplicationContext _context;
        private readonly ITokenService _tokenService;
        private readonly IOptions<JWTSettings> _config;

        public GatewayController(ILogger<GatewayController> logger, IOptions<JWTSettings> optAccess, ApplicationContext context, ITokenService tokenService, IOptions<JWTSettings> config)
        {
            _logger = logger;
            _options = optAccess.Value;
            _context = context;
            _tokenService = tokenService;
            _config = config;
        }

        [HttpPost]
        [Route("Run")]
        public bool Run(ProcessesList pl)
        {
            if (pl.Agruments == "None")
            {
                pl.Agruments = "";
            }
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pl.Directory+pl.FileName,
                    Arguments = pl.Agruments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                }
            };
            proc.Start();
            return true;
        }

        [HttpGet]
        [Route("getlist")]
        public int[] getlist()
        {
            Process[] processlist = Process.GetProcessesByName("Skymey_Binance_Prices");
            int[] processes = new int[processlist.Length];
            int i = 0;
            foreach(Process theprocess in processlist)
            {
                Console.WriteLine(@"Process: "+theprocess.ProcessName+" ID: "+theprocess.Id+"");
                processes[i] = theprocess.Id;
                i++;
            }
            return processes;
        }
        [HttpPost]
        [Route("kill")]
        public bool kill(ProcessesList pl)
        {
            pl.FileName = pl.FileName.Replace(".exe", "");
            Process[] processlist = Process.GetProcessesByName(pl.FileName);
            int[] processes = new int[processlist.Length];
            if (processlist.Length > 0)
            {
                int i = 0;
                foreach (Process theprocess in processlist)
                {
                    Console.WriteLine(@"Process: " + theprocess.ProcessName + " ID: " + theprocess.Id + "");
                    processes[i] = theprocess.Id;
                    theprocess.Kill();
                    i++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
