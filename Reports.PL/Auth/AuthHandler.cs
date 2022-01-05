using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reports.DAL.Context;
using Reports.DAL.Entities;

namespace Reports.PresentationLayer.Auth
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string SchemeName = "Employee";
        private readonly ReportsContext _context;

        public AuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, ReportsContext context) : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Endpoint endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (!authHeader.Scheme.Equals(SchemeName, StringComparison.InvariantCultureIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header Scheme");
            }
            if (!Guid.TryParse(authHeader.Parameter, out Guid employeeId))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header Parameter");
            }

            EmployeeEntity employee = await _context.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null) {
                return AuthenticateResult.Fail("Invalid Employee Identity");
            }

            Claim[] claims = {
                new (ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new (ClaimTypes.Name, employee.Name),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}