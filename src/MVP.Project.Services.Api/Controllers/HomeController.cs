using Microsoft.AspNetCore.Mvc;
using MVP.Project.Services.Api.ViewModels;

namespace MVP.Project.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API em execução");
        }

        [HttpGet("error/{id:int}")]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    modelErro.Title = "Erro interno";
                    modelErro.Message = "Ocorreu um erro no servidor. Por favor, tente novamente mais tarde.";
                    break;
                    
                case 404:
                    modelErro.Title = "Página não encontrada";
                    modelErro.Message = "A página solicitada não existe.";
                    break;
                    
                case 403:
                    modelErro.Title = "Acesso negado";
                    modelErro.Message = "Você não tem permissão para acessar este recurso.";
                    break;
                    
                default:
                    return StatusCode(500, new ErrorViewModel 
                    {
                        Title = "Erro desconhecido",
                        Message = "Ocorreu um erro não identificado."
                    });
            }

            modelErro.ErrorCode = id;
            return Ok(modelErro);
        }
    }
}
