using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Stone.API.ApplicationServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RestEase;
using Microsoft.AspNetCore.Authorization;
using Stone.API.Helpers;
using Stone.Domain.Entities;

namespace Stone.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class EstabelecimentoController : Controller
    {
        IEstabelecimentoApplicationService _EstabelecimentoService { get; }

        public EstabelecimentoController(IEstabelecimentoApplicationService EstabelecimentoService)
        {
            _EstabelecimentoService = EstabelecimentoService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]EstabelecimentoViewModel model)
        {
            if (model == null)
                return BadRequest("O CNPJ do estabelecimento precisa ser informado");

            try
            {
                var api = RestClient.For<IReceitaWS>("https://www.receitaws.com.br/v1");
                var estabelecimento = await api.GetEstabelecimentoAsync(model.CNPJ);
                if (estabelecimento.Status == "ERROR")
                    return BadRequest("Erro na API de ReceitaWS, tente novamente");

                await _EstabelecimentoService.Adicionar(estabelecimento);
                var newCustomer = await _EstabelecimentoService.ObterMaisRecente();

                return CreatedAtRoute("GetEstabelecimento", new { id = newCustomer.Id }, newCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Estabelecimento customer)
        {
            if (customer == null)
                return BadRequest("Customer precisa ser informado");

            try
            {
                var _id = customer.Id.ToString();
                var customerItem = await _EstabelecimentoService.ObterPorId(_id);

                if (customerItem == null)
                    return NotFound("Customer não encontrado");

                await _EstabelecimentoService.Atualizar(customer);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("É preciso informar o ID do Estabelecimento que será removido");

            var customer = await _EstabelecimentoService.ObterPorId(id);

            if (customer == null)
                return NotFound();

            await _EstabelecimentoService.Remover(id);
            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _EstabelecimentoService.BuscarTodos();

                if (customers != null && customers.Any())
                    return new ObjectResult(customers);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}", Name = "GetEstabelecimento")]
        public async Task<IActionResult> GetEstabelecimentoById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var customer = await _EstabelecimentoService.ObterPorId(id);

            if (customer == null)
                return NotFound();

            return new ObjectResult(customer);
        }
    }
}