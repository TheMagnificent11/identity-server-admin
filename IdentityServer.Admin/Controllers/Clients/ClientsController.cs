using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.Admin.Authorization;
using IdentityServer.Common.Constants;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Admin.Controllers.Clients
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = Policies.ManageUsers)]
    public sealed class ClientsController : Controller
    {
        public ClientsController(ConfigurationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        private ConfigurationDbContext Context { get; }

        private IMapper Mapper { get; }

        [HttpPost]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Post(
            [FromBody]CreateClientRequest request,
            CancellationToken cancellationToken)
        {
            var existing = await Context.Clients.FirstOrDefaultAsync(i => i.ClientId == request.ClientId);
            if (existing != null)
            {
                ModelState.AddModelError(nameof(request.ClientId), "Client already exists");
                return BadRequest(ModelState);
            }

            var client = Mapper.Map<Client>(request);

            Context.Clients.Add(client.ToEntity());

            await Context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
