using minimal_api.Dominio.Entidades;

namespace Teste.Domain.Entidades
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {

            //Arrange
            var veiculo = new Veiculo();

            //Act
            veiculo.Id = 1;
            veiculo.Nome = "Siena";
            veiculo.Marca ="Fiat";
            veiculo.Ano = 2008;

            //Assert
            Assert.AreEqual(1, veiculo.Id);
            Assert.AreEqual("Siena", veiculo.Nome);
            Assert.AreEqual("Fiat", veiculo.Marca);
            Assert.AreEqual(2008, veiculo.Ano);
        }
    }
}