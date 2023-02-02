// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using ConsoleAppTest.Domain;
using ConsoleAppTest.Models;
using ConsoleAppTest.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using System.Linq;

using var db = new ConsoleAppTest.Data.AppDbContext();

//Verifica se existe migrações pendentes
var existe = db.Database.GetPendingMigrations().Any();

Console.WriteLine("Hello Wold!");

if (existe)
{
    Console.WriteLine("Migrations pendentes!");

    db.Database.Migrate();
}


var cliente = new Cliente
{
    Nome = "Marcelo",
    Email = "marcelomg07@gmail.com",
    Telefone = "71991433168",
    CEP = "44088360",
    Cidade = "Feira de Santana",
    Estado = "BA"
};

var produto = new Produto
{
    Descricao = "Guarana",
    CodigoBarras = "78965432100000",
    Valor = 3,
    TipoProduto = TipoProdutoEnum.MercadoriaParaRevenda,
    Ativo = true,
};

var produtosList = new[]
{
    new Produto
    {
        Descricao = "Abacaxi",
        CodigoBarras = "78965432100000",
        Valor = 2,
        TipoProduto = TipoProdutoEnum.MercadoriaParaRevenda,
        Ativo = true,
    },
    new Produto
    {
        Descricao = "Cebola",
        CodigoBarras = "78965432100000",
        Valor = 4,
        TipoProduto = TipoProdutoEnum.MercadoriaParaRevenda,
        Ativo = true,
    }
};

var consultaPorSintaxe = (from c in db.Cliente where c.Id > 0 select c).ToList();

var consultaPorMetodo = db.Produto
    .Where(p => p.Id > 0)
    .OrderBy(p => p.Descricao)
    .ToList();

//var consultaPorMetodo = db.Produto.AsNoTracking().Where(p => p.Id > 0).ToList();
var pedidos = db.Pedidos!
    .Include(p => p.Itens)!
    .ThenInclude(p => p.Produto)!
    .ToList()!;

foreach (var item in consultaPorMetodo)
{
    Console.WriteLine($"Produto: {item.Descricao}");
    var result = db.Produto.Find(item.Id);
    //var result = db.Produto.FirstOrDefault(p => p.Id == item.Id);
    Console.WriteLine($"Retorno: {result!.Descricao} / {result!.Valor}");
}


// ATENÇÃO: Adicionando um unico registro em uma unica tabela.
incluirProduto("Nome do produto", "123456789", 1, TipoProdutoEnum.MercadoriaParaRevenda, true);

// ATENÇÃO: Adicionando varios registros em uma unica tabela.
incluirProdutos(produtosList);

// ATENÇÃO: Adicionando registros diferentes em tabelas diferentes.
incluirRegistrosDiversos(cliente, produto);

// ATENÇÃO: Adicionando um registro pai e varios filhos ao mesmo tempo.
var itemPedidoList = new List<ItemPedidoModel>
{
    new ItemPedidoModel { ProdutoId = 1, Quantidade = 5, Desconto = 0.25M },
    new ItemPedidoModel { ProdutoId = 2, Quantidade = 3, Desconto = 1.75M },
    new ItemPedidoModel { ProdutoId = 3, Quantidade = 1, Desconto = 2.25M },
};
incluirPedidoComItens(1, itemPedidoList, "Pedido via lista de produtoId (int)");

AtualizarDados1();
AtualizarDados2();
AtualizarDados3();
AtualizarDados4();

DeletarDados1();
DeletarDados2();


static void incluirProduto(string descricao, string codigoBarras, decimal valor, TipoProdutoEnum tipoProduto, bool ativo)
{
    var produto = new Produto
    {
        Descricao = descricao,
        CodigoBarras = codigoBarras,
        Valor = valor,
        TipoProduto = tipoProduto,
        Ativo = ativo,
    };

    using var db = new ConsoleAppTest.Data.AppDbContext();

    db.Add(produto);
    //db.Set<Produto>().Add(produto);
    //db.Entry(produto).State = EntityState.Added;
    //db.Add(produto);

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void incluirProdutos(IEnumerable<Produto> produtos)
{
    using var db = new ConsoleAppTest.Data.AppDbContext();

    db.AddRange(produtos);
    //db.Produto.AddRange(produtos);

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void incluirRegistrosDiversos(Cliente cliente, Produto produto)
{
    using var db = new ConsoleAppTest.Data.AppDbContext();

    db.AddRange(cliente, produto);

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void incluirPedidoComItens(int ClienteId, List<ItemPedidoModel> itemPedidoList, string observacao = "")
{
    using var db = new ConsoleAppTest.Data.AppDbContext();
    
    var produtoIdList = itemPedidoList.Select(p => p.ProdutoId);

    var pedidoItemList = db.Produto.Where(p => produtoIdList.Contains(p.Id)).ToList();

    var itens = new List<PedidoItem>();

    foreach (var item in produtoIdList)
    {
        itens.Add(new PedidoItem
        {
            ProdutoId = item,
            Desconto = itemPedidoList.FirstOrDefault(p => p.ProdutoId == item)!.Desconto,
            Quantidade = itemPedidoList.FirstOrDefault(p => p.ProdutoId == item)!.Quantidade,
            Valor = pedidoItemList.Where(p => p.Id == item).FirstOrDefault()!.Valor,
        });
    }

    var pedido = new Pedido
    {
        ClienteId = ClienteId,//db.Cliente.FirstOrDefault()!.Id,
        IniciadoEm = DateTime.Now,
        FinalizadoEm = DateTime.Now,
        Observacao = observacao,
        Status = StatusPedidoEnum.Analise,
        TipoFrete = TipoFreteEnum.CIF,
        Itens = itens
    };

    db.Add(pedido);

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void AtualizarDados1()
{
    Console.WriteLine($"1 - ?????????????????????????????????????");
    using var db = new ConsoleAppTest.Data.AppDbContext();

    var produto = db.Produto.Find(1);
    produto!.Descricao += 1;

    db.Update(produto);

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void AtualizarDados2()
{
    Console.WriteLine($"2 - ?????????????????????????????????????");
    using var db = new ConsoleAppTest.Data.AppDbContext();

    var produto = db.Produto.Find(2);
    produto!.Descricao += 2;

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void AtualizarDados3()
{
    Console.WriteLine($"3 - ?????????????????????????????????????");
    using var db = new ConsoleAppTest.Data.AppDbContext();

    var produto = db.Produto.Find(3);
    produto!.Descricao += 3;

    db.Entry(produto).State = EntityState.Modified;

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void AtualizarDados4()
{
    Console.WriteLine($"4 - ?????????????????????????????????????");
    using var db = new ConsoleAppTest.Data.AppDbContext();

    //var produto = db.Produto.Find(3);
    var produto = new Produto
    {
        Id = 4,
    };

    var produtoDesconectado = new
    {
        Descricao = "Produto desconectado",
        Valor = 7.0M
    };

    db.Attach(produto);
    db.Entry(produto).CurrentValues.SetValues(produtoDesconectado);

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void DeletarDados1()
{
    Console.WriteLine($"* - ?????????????????????????????????????");
    using var db = new ConsoleAppTest.Data.AppDbContext();

    var produto = db.Produto.Find(5);

    //db.Produto.Remove(produto);
    //db.Remove(produto);
    db.Entry(produto!).State = EntityState.Detached;

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}

static void DeletarDados2()
{
    Console.WriteLine($"* - ?????????????????????????????????????");
    using var db = new ConsoleAppTest.Data.AppDbContext();

    var produto = new Produto { Id = 6 };

    //db.Produto.Remove(produto);
    //db.Remove(produto);
    db.Entry(produto).State = EntityState.Detached;

    var result = db.SaveChanges();
    Console.WriteLine($"Total de registro(s) salvos: {result}");
}