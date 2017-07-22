using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CrossCutting.Identity.Authorization;
using Microsoft.AspNetCore.Authorization;
using Stone.API.ApplicationServices;

namespace Stone.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : Controller
    {
        IClienteApplicationService _clienteService { get; }
        private ICustomJwtSecurityToken _customJwtSecurityToken { get; }
        private IUsuarioApplicationService _usuarioService { get; }


        public ClienteController(IClienteApplicationService clienteService, ICustomJwtSecurityToken customJwtSecurityToken, IUsuarioApplicationService usuarioService)
        {
            _clienteService = clienteService;
            _customJwtSecurityToken = customJwtSecurityToken;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Insert([FromBody] ClienteViewModel cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente precisa ser informado");

            try
            {
                var cli = new Cliente
                {
                    CPF = cliente.CPF,
                    DataNascimento = cliente.DataNascimento,
                    Email = cliente.Email,
                    Nome = cliente.Nome,
                    NumeroCartao = cliente.NumeroCartao,
                    Sobrenome = cliente.Sobrenome
                };
                await _clienteService.Adicionar(cli);
                var newCustomer = await _clienteService.ObterMaisRecente();
                await CriarUsuario(cliente, newCustomer.Id);
                return CreatedAtRoute("GetCliente", new { id = newCustomer.Id }, newCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task CriarUsuario(ClienteViewModel cliente, string clienteId)
        {
            try
            {
                var usuario = new Usuario(cliente.Email, cliente.Senha, clienteId);
                await _usuarioService.Adicionar(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cliente customer)
        {
            if (customer == null)
                return BadRequest("Cliente precisa ser informado");

            try
            {
                var _id = customer.Id.ToString();
                var customerItem = await _clienteService.ObterPorId(_id);

                if (customerItem == null)
                    return NotFound("Cliente não encontrado");

                await _clienteService.Atualizar(customer);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("É preciso informar o ID do cliente que será removido");

            var customer = await _clienteService.ObterPorId(id);

            if (customer == null)
                return NotFound();

            await _clienteService.Remover(id);
            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _clienteService.BuscarTodos();

                if (customers != null && customers.Any())
                    return new ObjectResult(customers);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<IActionResult> GetClienteyId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var customer = await _clienteService.ObterPorId(id);

            if (customer == null)
                return NotFound();

            return new ObjectResult(customer);
        }



        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var usuario = new Usuario(login.Email, login.Senha, "");
            var user = await _usuarioService.Login(usuario);
            if (user == null)
                return BadRequest("Login ou senha inválido");
            var token = await GetJwtSecurityToken(user);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        private async Task<JwtSecurityToken> GetJwtSecurityToken(Usuario user)
        {

            var cliente = await _clienteService.ObterPorId(user.ClienteId);

            var clains = new List<Claim>();
            clains.Add(new Claim("Nome", cliente.Nome));
            clains.Add(new Claim("Email", cliente.Email));
            clains.Add(new Claim("Id", user.Id));
            var token = await _customJwtSecurityToken.GerarToken(clains);
            return token;
        }
    }

    public struct Login
    {
        public string Senha { get; set; }
        public string Email { get; set; }
    }

}