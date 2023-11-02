using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Skymey_Gateway.Data;
using Skymey_Gateway.Interfaces.JWT;

namespace Skymey_Gateway.Controllers
{
    [ApiController]
    [Route("api/User")]
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

        [HttpGet]
        [Route("GetUsers")]
        public string Index()
        {
            return "ok";
        }
    }
}
