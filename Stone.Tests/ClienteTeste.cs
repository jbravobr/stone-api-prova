using System;
using FluentAssertions;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Stone.Domain.Entities;
using Stone.Data.Interfaces;

namespace Stone.Tests
{
    public class ClienteTeste
    {
        [Fact]
        public async void Testa_Cliente_Adicionar_É_Chamado()
        {
            var mock = new Mock<IClienteRepository>();
            mock
                .Setup(x => x.Adicionar(It.IsAny<Cliente>()))
                .Returns(Task.FromResult(new Cliente()));

            var clienteService = mock.Object;
            await clienteService.Adicionar(new Cliente());

            mock.Verify(x => x.Adicionar(It.IsAny<Cliente>()), Times.AtLeastOnce());
        }

        [Fact]
        public async void Testa_Cliente_ObterTodos_Retorna_Lista_De_Clientes()
        {
            var mock = new Mock<IClienteRepository>();
            mock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync(new List<Cliente>());

            var clienteService = mock.Object;
            var clientes = await clienteService.BuscarTodos();

            clientes
                .Should()
                .BeOfType<List<Cliente>>();
        }

        [Fact]
        public async void Testa_Cliente_Exclusão_É_Chamado()
        {
            var mock = new Mock<IClienteRepository>();
            mock
                .Setup(x => x.Remover(It.IsAny<string>()))
                .Returns(Task.FromResult(""));

            var clienteService = mock.Object;
            await clienteService.Remover("597126328bd4a5041207bb0c");

            mock.Verify(x => x.Remover(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Fact]
        public async void Testa_Cliente_Atualizar_É_Chamado()
        {
            var mock = new Mock<IClienteRepository>();
            mock
                .Setup(x => x.Atualizar(It.IsAny<Cliente>()))
                .Returns(Task.FromResult(new Cliente()));

            var clienteService = mock.Object;
            await clienteService.Atualizar(new Cliente());

            mock.Verify(x => x.Atualizar(It.IsAny<Cliente>()), Times.AtLeastOnce());
        }

        [Fact]
        public async void Testa_Cliente_Retornar_Por_Id()
        {
            var mock = new Mock<IClienteRepository>();
            mock
                .Setup(x => x.ObterPorId(It.IsAny<string>()))
                .ReturnsAsync(new Cliente()
                {
                    CPF = "105.926.437-43",
                    DataNascimento = new DateTime(1983, 7, 21),
                    Nome = "Rodrigo Amaro",
                    Id = "597126328bd4a5041207bb0c",
                    NumeroCartao = "123"
                });

            var clienteService = mock.Object;
            var client = await clienteService.ObterPorId("597126328bd4a5041207bb0c");

            client
                .Should()
                .BeOfType<Cliente>();

            client
                .Should().Be(new Cliente()
                {
                    CPF = "105.926.437-43",
                    DataNascimento = new DateTime(1983, 7, 21),
                    Nome = "Rodrigo Amaro",
                    Id = "597126328bd4a5041207bb0c",
                    NumeroCartao = "123"
                });

        }
    }
}
