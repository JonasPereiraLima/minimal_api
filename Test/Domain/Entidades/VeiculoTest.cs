using Minimal_Api.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 1;
        veiculo.Nome = "i5 M60";
        veiculo.Marca = "BMW";
        veiculo.Ano = 2020;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("i5 M60", veiculo.Nome);
        Assert.AreEqual("BMW", veiculo.Marca);
        Assert.AreEqual(2020, veiculo.Ano);
    }
}
