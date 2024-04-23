

# DotnetApis

Este é um modelo de arquitetura desenvolvido para fins didáticos.
A ideia é ter classes de repositórios, serviços e controladores totalmente genéricos,
para que não seja necessário a implementação manual de todos os métodos de CRUD sempre que uma nova entidade for criada.

## Exercício Proposto: Construção de API de CRUD para Marketplace

**Contexto:**

Você faz parte de uma equipe especializada em desenvolvimento de APIs de alta performance. Sua empresa foi contratada por um marketplace renomado para criar uma API de CRUD básica para atender às necessidades do time de produtos desse marketplace. A equipe de produtos deseja gerenciar facilmente os produtos disponíveis na plataforma, incluindo adição, atualização, exclusão e consulta.

**Descrição do Exercício:**

1. **Objetivo:**
   Criar uma API de CRUD básica para gerenciamento de produtos no marketplace.

2. **Tecnologias Utilizadas:**
   - Linguagem de programação: C#
   - Plataforma: ASP.NET Core
   - Bibliotecas: FluentValidation, Swagger

3. **Requisitos da API:**
   - Implementar endpoints para adicionar, atualizar, excluir e listar produtos.
   - Validar os dados recebidos nos endpoints.
   - Documentar a API usando Swagger para facilitar o entendimento e o consumo.


### Como funciona

#### Entidades

As entidades devem herdar da classe abstrata `EntityBase`,
que possui as propriedades comuns a todas as entidades e também
uma implementação abstrata do método `ToDTO()`, que é o método de mapeamento da entidade em DTO.
Não foi utilizado AutoMapper ou qualquer outra biblioteca de mapeamento, pois a conversão implícita tem um desempenho melhor.
Todo repositório deverá ter um objeto do tipo `EntityBase` relacionado a ele para gerar as operações do CRUD.

#### DTOs

Os DTOs devem herdar da classe abstrata `DtoBase`,
que possui as propriedades comuns e também o método de conversão `ToEntity()` nos mesmos moldes das entidades.
Os DTOs também devem implementar o método `Validate`, que é responsável pelas validações básicas.

#### Validators

Para este modelo, estou utilizando a biblioteca FluentValidation, pois assim as classes de entidade e DTOs ficam mais limpas,
e também fica mais prático manter as validações no sistema.
A ideia é que toda classe de DTO tenha um Validator associado a ela para que as implementações genéricas funcionem corretamente.

#### BaseRepository

A classe `BaseRepository` mantém os métodos de CRUD genéricos (Insert, Update, Delete, GetById e GetAll) para funcionarem com todo objeto do tipo `EntityBase`.

#### BaseService

A classe `BaseService` mantém os métodos responsáveis por validar os objetos e chamar as classes de repositório.
Para adicionar uma nova classe de repositório, o desenvolvedor deve apenas herdar esta classe e passar os objetos de DTO e Entidade.

```csharp
public class ProductService : BaseService<ProductDto, Product>, IProductService
{
    public ProductService(IProductRepository repository) : base(repository)
    {
    }
}
```

#### BaseController
A classe BaseController deverá ser hedada por todos os Controllers que implementem as operações de CRUD, 
isso está facilitando pois ela já possui as configurações necessárias para manter as rotas no padrão RESTful, 
configurações de segurança e também a implementação de validações de exceções e também a documentação do Swagger. 
Com isso ficou bem simples criar um novo controller com todas as operações. 

```csharp
    public class ProductController : BaseController<ProductDto, Product>   
    {
        public ProductController(IProductService productService) : base(productService)
        {
        }
    }        
```
