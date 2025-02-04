// AuthController.cs
using Microsoft.AspNetCore.Mvc;
using System;


    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginRequest request)
        {
            var existingUser = _authService.AuthenticateUser(request.Username, request.Password);
            if (existingUser != null)
                return BadRequest("El usuario ya existe");

            var newUser = _authService.RegisterUser(request.Username, request.Password);
            return Ok(new { Message = "Usuario registrado correctamente", newUser.Username });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _authService.AuthenticateUser(request.Username, request.Password);
            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            var token = _authService.GenerateJwtToken(user.Username);

            // Guardamos el token en una cookie segura
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,  // La cookie no es accesible por JavaScript
                Secure = true,    // Solo se envía por HTTPS
                SameSite = SameSiteMode.Strict, // Solo se envía en el mismo sitio
                Expires = DateTime.UtcNow.AddMinutes(90) // Expira en 90 minutos
            });

            return Ok(new { Message = "Inicio de sesión exitoso" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return Ok(new { Message = "Sesión cerrada exitosamente" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

