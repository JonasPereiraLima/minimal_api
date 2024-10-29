using System.Net;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minimal_Api.Dominio.Entidades;
using Minimal_Api.Dominio.Servicos;
using Minimal_Api.Infraestrutura.Db;

namespace Test.Domain.Servicos;

[TestClass]
public class VeiculoServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));
        // Configurar o ConfigurationBuilder
        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }

    [TestMethod]
    public void TestandoSalvarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Id = 1;
        veiculo.Nome = "i5 M60";
        veiculo.Marca = "BMW";
        veiculo.Ano = 2020;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);

        // Assert
        Assert.AreEqual(1, veiculoServico.Todos(1, null, null).Count);
    }

    [TestMethod]
    public void TestandoBuscarVeiculoPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoTest = new Veiculo();
        veiculoTest.Id = 1;
        veiculoTest.Nome = "i5 M60";
        veiculoTest.Marca = "BMW";
        veiculoTest.Ano = 2020;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculoTest);
        var veiculo = veiculoServico.BuscaPorId(veiculoTest.Id);

        // Assert
        Assert.AreEqual(1, veiculo?.Id);
    }

    [TestMethod]
    public void TestandoApagarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoTest = new Veiculo();
        veiculoTest.Id = 1;
        veiculoTest.Nome = "i5 M60";
        veiculoTest.Marca = "BMW";
        veiculoTest.Ano = 2020;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculoTest);

        var veiculo = veiculoServico.BuscaPorId(veiculoTest.Id);
        if (veiculo != null)
            veiculoServico.Apagar(veiculo);

        // Assert
        Assert.AreEqual(0, veiculoServico.Todos(1, null, null).Count);
    }

    [TestMethod]
    public void TestandoAtualizarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoTest = new Veiculo();
        veiculoTest.Id = 1;
        veiculoTest.Nome = "i5 M60";
        veiculoTest.Marca = "BMW";
        veiculoTest.Ano = 2020;

        var newVeiculoTest = new Veiculo();
        newVeiculoTest.Nome = "DOLPHIN";
        newVeiculoTest.Marca = "BYD";
        newVeiculoTest.Ano = 2022;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculoTest);

        var veiculo = veiculoServico.BuscaPorId(veiculoTest.Id);
        if (veiculo != null)
        {
            veiculo.Nome = newVeiculoTest.Nome;
            veiculo.Marca = newVeiculoTest.Marca;
            veiculo.Ano = newVeiculoTest.Ano;
            veiculoServico.Atualizar(veiculo);
        }
        var newVeiculo = veiculoServico.BuscaPorId(veiculoTest.Id);


        // Assert
        Assert.AreEqual(newVeiculoTest.Nome, newVeiculo?.Nome);
        Assert.AreEqual(newVeiculoTest.Marca, newVeiculo?.Marca);
    }

}