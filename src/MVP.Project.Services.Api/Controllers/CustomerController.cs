using MVP.Project.Application.EventSourcedNormalizers;
using MVP.Project.Application.Interfaces;
using MVP.Project.Application.ViewModels;
using MVP.Project.Infra.CrossCutting.Identity.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVP.Project.Services.Api.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [AllowAnonymous]
        [HttpGet("customer")]
        public async Task<IEnumerable<CustomerViewModel>> Get()
        {
            return await _customerAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("customer/{id:guid}")]
        public async Task<CustomerViewModel> Get(Guid id)
        {
            return await _customerAppService.GetById(id);
        }

        [AllowAnonymous]
        [HttpPost("customer")]
        public async Task<IActionResult> Post([FromBody]CustomerViewModel customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.Register(customerViewModel));
        }

        [AllowAnonymous]
        [HttpPut("customer")]
        public async Task<IActionResult> Put([FromBody]CustomerViewModel customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.Update(customerViewModel));
        }

        [AllowAnonymous]
        [HttpDelete("customer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _customerAppService.Remove(id));
        }

        [AllowAnonymous]
        [HttpGet("customer/history/{id:guid}")]
        public async Task<IList<CustomerHistoryData>> History(Guid id)
        {
            return await _customerAppService.GetAllHistory(id);
        }
    }
}
