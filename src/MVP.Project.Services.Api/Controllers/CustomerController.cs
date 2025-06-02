using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MVP.Project.Application.Interfaces;
using MVP.Project.Application.ViewModels;
using MVP.Project.Application.EventSourcedNormalizers;

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
        [HttpGet("customer/get")]
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
        [HttpPatch("customer/update/{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _customerAppService.Update(id, customerViewModel);
            return CustomResponse(result);
        }

        [AllowAnonymous]
        [HttpDelete("customer/remove")]
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
