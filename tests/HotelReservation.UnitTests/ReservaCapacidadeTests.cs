using HotelReservation.Domain;
using HotelReservation.Domain.Exceptions;

namespace HotelReservation.UnitTests;

/// <summary>
/// Testes da RN-001 / DIO-001: a quantidade de hóspedes não pode superar
/// a capacidade da suíte.
/// </summary>
public class ReservaCapacidadeTests
{
    private static List<Pessoa> CriarHospedes(int quantidade) =>
        Enumerable.Range(1, quantidade)
            .Select(i => new Pessoa($"Hospede{i}", "Teste"))
            .ToList();

    [Fact]
    public void CadastrarHospedes_AbaixoDaCapacidade_DeveCadastrarComSucesso()
    {
        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(new Suite("Standard", capacidade: 3, valorDiaria: 100m));

        reserva.CadastrarHospedes(CriarHospedes(2));

        Assert.Equal(2, reserva.ObterQuantidadeHospedes());
    }

    [Fact]
    public void CadastrarHospedes_ExatamenteNaCapacidade_DeveCadastrarComSucesso()
    {
        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(new Suite("Standard", capacidade: 2, valorDiaria: 100m));

        reserva.CadastrarHospedes(CriarHospedes(2));

        Assert.Equal(2, reserva.ObterQuantidadeHospedes());
    }

    [Fact]
    public void CadastrarHospedes_AcimaDaCapacidade_DeveLancarCapacidadeExcedidaException()
    {
        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(new Suite("Standard", capacidade: 2, valorDiaria: 100m));

        var ex = Assert.Throws<CapacidadeExcedidaException>(
            () => reserva.CadastrarHospedes(CriarHospedes(3)));

        Assert.Equal(3, ex.QuantidadeHospedes);
        Assert.Equal(2, ex.CapacidadeSuite);
    }

    [Fact]
    public void CadastrarHospedes_AcimaDaCapacidade_NaoDeveAlterarReserva()
    {
        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(new Suite("Standard", capacidade: 1, valorDiaria: 100m));

        Assert.Throws<CapacidadeExcedidaException>(
            () => reserva.CadastrarHospedes(CriarHospedes(2)));

        Assert.Equal(0, reserva.ObterQuantidadeHospedes());
    }

    [Fact]
    public void CadastrarHospedes_EmChamadasSucessivas_DeveValidarCapacidadeAcumulada()
    {
        // Regressão: duas chamadas parciais não podem ultrapassar a capacidade total.
        var reserva = new Reserva(diasReservados: 3);
        reserva.CadastrarSuite(new Suite("Standard", capacidade: 4, valorDiaria: 100m));
        reserva.CadastrarHospedes(CriarHospedes(3));

        var ex = Assert.Throws<CapacidadeExcedidaException>(
            () => reserva.CadastrarHospedes(CriarHospedes(2)));

        Assert.Equal(5, ex.QuantidadeHospedes);
        Assert.Equal(4, ex.CapacidadeSuite);
        Assert.Equal(3, reserva.ObterQuantidadeHospedes());
    }
}
