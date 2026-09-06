using HotelReservation.Domain;

namespace HotelReservation.UnitTests;

public class SuiteTests
{
    [Fact]
    public void CriarSuite_ComDadosValidos_DeveArmazenarPropriedades()
    {
        var suite = new Suite("Premium", 4, 320.50m);

        Assert.Equal("Premium", suite.TipoSuite);
        Assert.Equal(4, suite.Capacidade);
        Assert.Equal(320.50m, suite.ValorDiaria);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void CriarSuite_ComCapacidadeInvalida_DeveLancarArgumentException(int capacidade)
    {
        Assert.Throws<ArgumentException>(() => new Suite("Standard", capacidade, 100m));
    }

    [Fact]
    public void CriarSuite_ComValorDiariaNegativo_DeveLancarArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Suite("Standard", 2, -0.01m));
    }

    [Fact]
    public void CriarSuite_ComValorDiariaZero_DeveSerAceito()
    {
        var suite = new Suite("Cortesia", 2, 0m);

        Assert.Equal(0m, suite.ValorDiaria);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CriarSuite_ComTipoInvalido_DeveLancarArgumentException(string? tipoSuite)
    {
        Assert.Throws<ArgumentException>(() => new Suite(tipoSuite!, 2, 100m));
    }
}
