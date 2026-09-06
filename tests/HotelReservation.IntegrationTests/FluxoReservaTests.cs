using HotelReservation.Domain;
using HotelReservation.Domain.Exceptions;

namespace HotelReservation.IntegrationTests;

/// <summary>
/// Testes de integração do fluxo completo de uma reserva:
/// criar hóspedes, criar e cadastrar a suíte, cadastrar hóspedes,
/// obter a quantidade e calcular o valor (com e sem desconto).
/// </summary>
public class FluxoReservaTests
{
    [Fact]
    public void FluxoCompleto_ReservaCurta_DeveCalcularValorSemDesconto()
    {
        // Arrange — criar hóspedes e suíte.
        var hospedes = new[]
        {
            new Pessoa("Ana", "Silva"),
            new Pessoa("Bruno", "Souza")
        };
        var suite = new Suite("Standard", capacidade: 2, valorDiaria: 150.00m);

        // Act — cadastrar suíte, hóspedes e executar a reserva.
        var reserva = new Reserva(diasReservados: 5);
        reserva.CadastrarSuite(suite);
        reserva.CadastrarHospedes(hospedes);

        // Assert — quantidade e valor sem desconto.
        Assert.Equal(2, reserva.ObterQuantidadeHospedes());
        Assert.False(reserva.PossuiDescontoLongaEstadia);
        Assert.Equal(750.00m, reserva.CalcularValorDiaria());
    }

    [Fact]
    public void FluxoCompleto_ReservaLonga_DeveCalcularValorComDescontoDeDezPorCento()
    {
        var hospedes = new[]
        {
            new Pessoa("Carla", "Mendes"),
            new Pessoa("Diego", "Pereira"),
            new Pessoa("Elisa", "Rocha")
        };
        var suite = new Suite("Premium", capacidade: 3, valorDiaria: 320.00m);

        var reserva = new Reserva(diasReservados: 12);
        reserva.CadastrarSuite(suite);
        reserva.CadastrarHospedes(hospedes);

        Assert.Equal(3, reserva.ObterQuantidadeHospedes());
        Assert.True(reserva.PossuiDescontoLongaEstadia);
        Assert.Equal(3456.00m, reserva.CalcularValorDiaria());
    }

    [Fact]
    public void FluxoCompleto_ReservaNaFronteiraDeDezDias_DeveAplicarDesconto()
    {
        var hospedes = new[] { new Pessoa("Fabio", "Lima") };
        var suite = new Suite("Standard", capacidade: 2, valorDiaria: 200.00m);

        var reserva = new Reserva(diasReservados: 10);
        reserva.CadastrarSuite(suite);
        reserva.CadastrarHospedes(hospedes);

        Assert.Equal(1, reserva.ObterQuantidadeHospedes());
        Assert.Equal(1800.00m, reserva.CalcularValorDiaria());
    }

    [Fact]
    public void FluxoCompleto_HospedesAcimaDaCapacidade_DeveRejeitarReserva()
    {
        var hospedes = new[]
        {
            new Pessoa("Gisele", "Alves"),
            new Pessoa("Hugo", "Barros"),
            new Pessoa("Iris", "Costa")
        };
        var suite = new Suite("Standard", capacidade: 2, valorDiaria: 150.00m);

        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(suite);

        Assert.Throws<CapacidadeExcedidaException>(() => reserva.CadastrarHospedes(hospedes));
        Assert.Equal(0, reserva.ObterQuantidadeHospedes());
    }
}
