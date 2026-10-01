using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace impresion3d.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ImpresionController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "-----HOLA-----";
        }
     
            
        [HttpPost]
        public string Post()
        {
            return "-----Este metodo guarda informacion-----";
        }
    }
}
