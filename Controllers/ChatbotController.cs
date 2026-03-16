using Microsoft.AspNetCore.Mvc;
using HealthAids.Services;

namespace HealthAids.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly ChatbotService _chatbotService;

        public ChatbotController(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpGet("start")]
        public IActionResult Start()
        {
            _chatbotService.Reset();
            var node = _chatbotService.GetCurrentNode();
            return Ok(new
            {
                success = true,
                node
            });
        }

        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            var node = _chatbotService.GetCurrentNode();
            return Ok(new
            {
                success = true,
                node
            });
        }

        [HttpPost("select")]
        public IActionResult SelectOption([FromBody] SelectOptionRequest request)
        {
            var isAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;

            var node = _chatbotService.SelectOption(request.OptionId, isAuthenticated);

            if (node == null)
                return BadRequest(new { success = false, message = "Opción inválida" });

           
            string? action = null;

            // Buscar en las opciones del nodo la opción con el ID que el usuario selecciono
            if (node.Options != null)
            {
                var selectedOption = node.Options.FirstOrDefault(o => o.Id == request.OptionId);
                if (selectedOption != null && !string.IsNullOrEmpty(selectedOption.Action))
                {
                    action = selectedOption.Action;
                }
            }

            // Si no se encontro en las opciones del nodo actual, verificar en los IDs conocidos
            if (string.IsNullOrEmpty(action))
            {
                switch (request.OptionId)
                {
                    case 20: // "Ir a mis citas"
                    case 22: // "Sí, agendar cita"
                        action = "schedule";
                        break;
                    case 17: // "Iniciar sesion"
                        action = "login";
                        break;
                    case 18: // "Registrarme"
                        action = "register";
                        break;
                }
            }

            

            return Ok(new
            {
                success = true,
                node,
                action = action
            });
        }
        [HttpPost("getID")]
        private int? GetUserIdFromToken()
        {
            // Implementa la logica para extraer el userId del token JWT
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return null;

            var token = authHeader.Substring("Bearer ".Length);
    
            return 1;
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _chatbotService.Reset();
            var node = _chatbotService.GetCurrentNode();
            return Ok(new
            {
                success = true,
                node
            });
        }

        [HttpGet("node/{nodeId}")]
        public IActionResult GoToNode(int nodeId)
        {
            var node = _chatbotService.GoToNode(nodeId);
            if (node == null)
                return NotFound(new { success = false, message = "Nodo no encontrado" });

            return Ok(new
            {
                success = true,
                node
            });
        }
    }

    public class SelectOptionRequest
    {
        public int OptionId { get; set; }
    }
}