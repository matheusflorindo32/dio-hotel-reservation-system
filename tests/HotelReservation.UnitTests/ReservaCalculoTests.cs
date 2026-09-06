using HotelReservation.Domain;

namespace HotelReservation.UnitTests;

/// <summary>
/// Testes das RN-002, RN-003 e RN-004 (DIO-002, DIO-003, DIO-004):
/// quantidade de hóspedes, cálculo do valor e desconto de 10% para
/// reservas com DiasReservados &gt;= 10.
/// </summary>
public class ReservaCalculoTests
{
    private static Reserva CriarReservaComSuite(int diasReservados, decimal valorDiaria = 100m)
    {
        var reserva = new Reserva(diasReservados);
        reserva.CadastrarSuite(new Suite("Standard", capacidade: 6, valorDiaria));
        return reserva;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void ObterQuantidadeHospedes_DeveRetornarQuantidadeCadastrada(int quantidade)
    {
        var reserva = CriarReservaComSuite(diasReservados: 2);
        var hospedes = Enumerable.Range(1, quantidade)
            .Select(i => new Pessoa($"Hospede{i}", "Teste"));

        reserva.CadastrarHospedes(hospedes);

        Assert.Equal(quantidade, reserva.ObterQuantidadeHospedes());
    }

    [Fact]
    public void ObterQuantidadeHospedes_SemHospedes_DeveRetornarZero()
    {
        var reserva = CriarReservaComSuite(diasReservados: 2);

        Assert.Equal(0, reserva.ObterQuantidadeHospedes());
    }

    // RN-003 / DIO-003: ValorTotal = DiasReservados x ValorDiaria (sem desconto).
    [Theory]
    [InlineData(1, 100.00, 100.00)]
    [InlineData(5, 150.00, 750.00)]
    [InlineData(9, 320.00, 2880.00)]
    [InlineData(9, 199.90, 1799.10)]
    public void CalcularValorDiaria_ComMenosDeDezDias_NaoDeveAplicarDesconto(
        int diasReservados, decimal valorDiaria, decimal valorEsperado)
    {
        var reserva = CriarReservaComSuite(diasReservados, valorDiaria);

        Assert.Equal(valorEsperado, reserva.CalcularValorDiaria());
        Assert.False(reserva.PossuiDescontoLongaEstadia);
    }

    // RN-004 / DIO-004: desconto de 10% quando DiasReservados >= 10.
    // O caso de 10 dias é o TESTE DE FRONTEIRA OBRIGATÓRIO (ADR-001).
    [Theory]
    [InlineData(10, 100.00, 900.00)]
    [InlineData(10, 199.90, 1799.10)]
    [InlineData(11, 150.00, 1485.00)]
    [InlineData(30, 200.00, 5400.00)]
    public void CalcularValorDiaria_ComDezDiasOuMais_DeveAplicarDescontoDeDezPorCento(
        int diasReservados, decimal valorDiaria, decimal valorEsperado)
    {
        var reserva = CriarReservaComSuite(diasReservados, valorDiaria);

        Assert.Equal(valorEsperado, reserva.CalcularValorDiaria());
        Assert.True(reserva.PossuiDescontoLongaEstadia);
    }

    [Fact]
    public void CalcularValorDiaria_ComExatamenteDezDias_DeveAplicarDesconto()
    {
        // Teste de fronteira explícito da decisão normativa >= 10 dias (ADR-001).
        var reserva = CriarReservaComSuite(diasReservados: 10, valorDiaria: 250m);

        Assert.Equal(2250.00m, reserva.CalcularValorDiaria());
    }

    [Fact]
    public void CalcularValorDiaria_ComNoveDias_DeveCobrarValorIntegral()
    {
        // Fronteira imediatamente inferior: garante que o desconto não vaza para 9 dias.
        var reserva = CriarReservaComSuite(diasReservados: 9, valorDiaria: 250m);

        Assert.Equal(2250.00m, reserva.CalcularValorDiaria());
    }
}
