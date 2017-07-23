using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Stone.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stone.API.ApplicationServices;
using Stone.Domain.Entities;

namespace Stone.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PagamentoController : Controller
    {
        IPagamentoApplicationService _PagamentoService { get; }

        public PagamentoController(IPagamentoApplicationService PagamentoService)
        {
            _PagamentoService = PagamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PagamentoViewModel model)
        {
            if (model == null)
                return BadRequest("Pagamento precisa ser informado");

            try
            {
                await _PagamentoService.Adicionar(new Pagamento()
                {
                    ClienteID = model.ClientID,
                    Data = DateTime.Now,
                    EstabelecimentoID = model.EstabelecimentoID,
                    IsCanceled = false,
                    Valor = model.Valor
                });
                var newpayment = await _PagamentoService.ObterMaisRecente();

                return CreatedAtRoute("GetPagamento", new { id = newpayment.Id }, newpayment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Pagamento payment)
        {
            if (payment == null)
                return BadRequest("Pagamento precisa ser informado");

            try
            {
                var _id = payment.Id.ToString();
                var paymentItem = await _PagamentoService.ObterPorId(_id);

                if (paymentItem == null)
                    return NotFound("Pagamento não encontrado");

                await _PagamentoService.Atualizar(payment);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CancelarPagamento(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("É preciso informar o ID do Pagamento que será removido");

            var payment = await _PagamentoService.ObterPorId(id);

            if (payment == null)
                return NotFound();
            
            payment.IsCanceled = true;
            await _PagamentoService.Atualizar(payment);
            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllpayments()
        {
            try
            {
                var payments = await _PagamentoService.BuscarTodos();

                if (payments != null && payments.Any())
                    return new ObjectResult(payments);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{clienteid}", Name = "GetPagamentoByCliente")]
        public async Task<IActionResult> GetPagamentoByClienteId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            Expression<Func<Pagamento, bool>> filter = (x) => x.ClienteID == id;
            var payment = await _PagamentoService.Buscar(filter);

            if (payment == null)
                return NotFound();

            return new ObjectResult(payment);
        }

		[HttpGet("{estabelecimentoid}", Name = "GetPagamentoByCliente")]
		public async Task<IActionResult> GetPagamentoByEstabelecimentoId(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			Expression<Func<Pagamento, bool>> filter = (x) => x.EstabelecimentoID == id;
			var payment = await _PagamentoService.Buscar(filter);

			if (payment == null)
				return NotFound();

			return new ObjectResult(payment);
		}

        [HttpGet("{id}", Name = "GetPagamento")]
        public async Task<IActionResult> GetPagamentoById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var payment = await _PagamentoService.ObterPorId(id);

            if (payment == null)
                return NotFound();

            return new ObjectResult(payment);
        }
    }
}
