using GCRUD.Application.DTOs;
using GCRUD.Application.Services;
using GCRUD.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahsolController
        : GenericController<MahsolDto, CreateMahsolDto, MahsolDto, Mahsol, Guid>
    {
        public MahsolController(
            IGenericService<MahsolDto, CreateMahsolDto, MahsolDto, Mahsol, Guid> service)
            : base(service)
        {
        }
    }
}