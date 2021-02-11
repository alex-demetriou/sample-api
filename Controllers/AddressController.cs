using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly AddressContext _context;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        [Route("address")]
        [HttpGet]
        public IActionResult GetAddress([FromQuery] string postCode)
        {
            if (!ValidatePostcode(postCode)) return new BadRequestResult();

            return new OkObjectResult(_context.HackneyAddresses.Where(a => a.Postcode == postCode));
        }

        private bool ValidatePostcode(string postCode)
        {
            const string postCodeRegex = @"[A-Za-z][1-9][1-9][A-Za-z]{2}";

            return Regex.Match(postCode, postCodeRegex, RegexOptions.None).Success;
        }
    }
}