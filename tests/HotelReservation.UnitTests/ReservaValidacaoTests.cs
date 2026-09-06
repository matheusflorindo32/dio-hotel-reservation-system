using HotelReservation.Domain;
using HotelReservation.Domain.Exceptions;

namespace HotelReservation.UnitTests;

/// <summary>
/// Testes da RN-005: tratamento de dados inválidos e operações fora de ordem.
/// </summary>
public class ReservaValidacaoTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-30)]
    public void CriarReserva_ComDiasInvalidos_DeveLancarArgumentException(int diasReservados)
    {
        Assert.Throws<ArgumentException>(() => new Reserva(diasReservados));
    }

    [Fact]
    public void CadastrarSuite_ComSuiteNula_DeveLancarArgumentNullException()
    {
        var reserva = new Reserva(diasReservados: 3);

        Assert.Throws<ArgumentNullException>(() => reserva.CadastrarSuite(null!));
    }

    [Fact]
    public void CadastrarHospedes_ComListaNula_DeveLancarArgumentNullException()
    {
        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(new Suite("Standard", 2, 100m));

        Assert.Throws<ArgumentNullException>(() => reserva.CadastrarHospedes(null!));
    }

    [Fact]
    public void CadastrarHospedes_ComListaVazia_DeveLancarDomainException()
    {
        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(new Suite("Standard", 2, 100m));

        Assert.Throws<DomainException>(() => reserva.CadastrarHospedes(new List<Pessoa>()));
    }

    [Fact]
    public void CadastrarHospedes_SemSuite_DeveLancarSuiteNaoInformadaException()
    {
        var reserva = new Reserva(diasReservados: 3);
        var hospedes = new[] { new Pessoa("Ana", "Silva") };

        Assert.Throws<SuiteNaoInformadaException>(() => reserva.CadastrarHospedes(hospedes));
    }

    [Fact]
    public void CalcularValorDiaria_SemSuite_DeveLancarSuiteNaoInformadaException()
    {
        var reserva = new Reserva(diasReservados: 3);

        Assert.Throws<SuiteNaoInformadaException>(() => reserva.CalcularValorDiaria());
    }

    [Fact]
    public void ExcecoesDeDominio_DevemHerdarDeDomainException()
    {
        Assert.True(typeof(DomainException).IsAssignableFrom(typeof(CapacidadeExcedidaException)));
        Assert.True(typeof(DomainException).IsAssignableFrom(typeof(SuiteNaoInformadaException)));
    }
}
