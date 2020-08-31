using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using CursoNetCore.DTO;
using CursoNetCore.Model;
using CursoNetCore.Repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CursoNetCore.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _config = config;
        }
        [Authorize]
        [HttpGet]
        public ActionResult GetUsuarios()
        {
            var LstUsuario = _usuarioRepository.GetUsuarios();
            var LstUsuarioDTO = new List<UsuarioDTO>();

            foreach (var list in LstUsuario)
            {
                LstUsuarioDTO.Add(_mapper.Map<UsuarioDTO>(list));
            }

            return Ok(LstUsuarioDTO);
        }
        [Authorize]
        [HttpGet("{IdUsuario:int}", Name = "GetUsuario")]
        public ActionResult GetUsuario(int IdUsuario)
        {
            var usuario = _usuarioRepository.GetUsuario(IdUsuario);
            if (usuario == null)
            {
                return NotFound();
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar(UsuarioAuthDTO DatosRegistro)
        {
            if (_usuarioRepository.ExisteUsuario(DatosRegistro.ClienteId.ToUpper()))
            {
                return BadRequest("El Nombre de usuario " + DatosRegistro.ClienteId + "no esta disponible");
            }

            var UsuarioRegistrado = new Usuario
            {
                ClientId = DatosRegistro.ClienteId
            };
            var result = _usuarioRepository.Registrar(UsuarioRegistrado, DatosRegistro.Password);
            return Ok(result);
        }

        [HttpPost("Login")]
        public IActionResult Login(UsuarioAuthLoginDTO Credenciales)
        {
            var usuarioCredencial = _usuarioRepository.Login(Credenciales.ClienteId, Credenciales.Password);

            if (usuarioCredencial == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,usuarioCredencial.ClientId.ToString()),
            new Claim(ClaimTypes.Name, usuarioCredencial.ClientId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TokenKey").Value));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var DescriptionToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credencial
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            var token = tokenHandle.CreateToken(DescriptionToken);
            return Ok(new
            {
                token = tokenHandle.WriteToken(token)
            });
        }

    }
}
