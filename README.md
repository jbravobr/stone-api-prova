# Stone API - Desafio Stone

Esta é uma solução para o Desafio Stone.

## Sobre o solução

Para o desafio fora projetada uma WEB API, utilizando *.NET CORE* como framework base do desenvolvimento, sendo escolhido e mantido
ao máximo o princípio KISS (Keep It Simple, Stupid) afim de manter coesão, simplicidade e um toque de novidade utilizando algumas coisas bem
legais.

Como core de arquitetura, foi utilizado 1 WEB API, sendo esta alcançada por um ENDPOINT portando de 3 controllers distintas, cada
uma para um modelo do nosso domínio. Apresentamos também uma solução de Repositório Genérico, exemplificando como utilizamos este e demos
exemplo de especializações necessárias de acordo com as entidades do nosso domínio (ex: IClienteRepository). Atráves de repositório Genérico
temos acesso ao banco de dados, onde aqui utilizamos uma instância remota do MongoDB (utilizamos uma instância Sandbox alocada via mLab).

Para melhor separar nossos conceitos, implementei um projeto de IoC, nos auxiliando com o desacoplamento das soluções através da Injeção
de dependência, nos controllers e nos Application Services, essa "camada fake" nos auxilia a aumentar a testabilidade e
desacoplamento entre os controllers e a camada de infraestrutura (MongoDB).

Inseri também uma camada de autenticação através da geração de um JWT (JSON Web Token), controlando assim os acessos utilizando (se necessário)
de mecanismos como TOKEN e REFRESH TOKEN para autorização e re-autorização de acessos.

Existe também uma extensão da WEB API para tratar fluxos de excessão não tratados, este ocorre através da interface *IExceptionFilter*
implementado pela classe CustomExceptionFilter.

Exite também uma implementação da ferramenta SWAGGER, facilitando assim a leitura e utilização da API.

A solução contempla a criação da imagem via Docker e esta está rodando em um serviço de container na AWS.

Por fim também inseri um projeto de testes, demonstrando testes utilizando xUnit, Moq e FluentAssertions para dar melhor leitura nos
Asserts dos testes.

## Pré-requisitos

Para compilação do projeto necessário .NET Core instalado em sua última versão.

## Endereço público de acesso 

```
http://ec2-13-58-212-161.us-east-2.compute.amazonaws.com/swagger/
```

Para utlização inicial usar ec2-13-58-212-161.us-east-2.compute.amazonaws.com/swagger/#!/Cliente/ApiClienteLoginPost
com o usuário teste@teste e a senha 1234

## Nugets utilzados

| Plug-ins|
| ------------------- |
|Swagger|
|Autofac|
|FluentAssertions|
|Moq|
|MongoDB Driver for C#|
|JSON.Net|
|RestEase|

## Construído com

* [.NET CORE](https://www.microsoft.com/net/core) 

## Autor

* **Rodrigo Amaro**