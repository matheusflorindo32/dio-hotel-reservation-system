using System.Globalization;
using HotelReservation.Domain;
using HotelReservation.Domain.Exceptions;

var cultura = CultureInfo.GetCultureInfo("pt-BR");

static void ImprimirTitulo(string titulo)
{
    Console.WriteLine();
    Console.WriteLine(new string('=', 60));
    Console.WriteLine(titulo);
    Console.WriteLine(new string('=', 60));
}

static void ImprimirResumo(Reserva reserva, CultureInfo cultura)
{
    Console.WriteLine($"Suíte: {reserva.Suite!.TipoSuite}");
    Console.WriteLine($"Hóspedes ({reserva.ObterQuantidadeHospedes()}): " +
                      string.Join(", ", reserva.Hospedes.Select(h => h.NomeCompleto)));
    Console.WriteLine($"Dias reservados: {reserva.DiasReservados}");
    Console.WriteLine($"Desconto de longa estadia: {(reserva.PossuiDescontoLongaEstadia ? "10% aplicado" : "não aplicável")}");
    Console.WriteLine($"Valor total: {reserva.CalcularValorDiaria().ToString("C", cultura)}");
}

ImprimirTitulo("SISTEMA DE RESERVAS DE HOSPEDAGEM — DESAFIO DIO");

// Cenário 1: reserva curta (sem desconto).
ImprimirTitulo("Cenário 1 — Reserva de 5 dias (sem desconto)");

var suiteStandard = new Suite(tipoSuite: "Standard", capacidade: 2, valorDiaria: 150.00m);
var reservaCurta = new Reserva(diasReservados: 5);
reservaCurta.CadastrarSuite(suiteStandard);
reservaCurta.CadastrarHospedes(new[]
{
    new Pessoa("Ana", "Silva"),
    new Pessoa("Bruno", "Souza")
});
ImprimirResumo(reservaCurta, cultura);

// Cenário 2: reserva longa (com desconto de 10% — RN-004).
ImprimirTitulo("Cenário 2 — Reserva de 12 dias (desconto de 10%)");

var suitePremium = new Suite(tipoSuite: "Premium", capacidade: 3, valorDiaria: 320.00m);
var reservaLonga = new Reserva(diasReservados: 12);
reservaLonga.CadastrarSuite(suitePremium);
reservaLonga.CadastrarHospedes(new[]
{
    new Pessoa("Carla", "Mendes"),
    new Pessoa("Diego", "Pereira"),
    new Pessoa("Elisa", "Rocha")
});
ImprimirResumo(reservaLonga, cultura);

// Cenário 3: tentativa de reserva acima da capacidade (RN-001).
ImprimirTitulo("Cenário 3 — Capacidade excedida (exceção de domínio)");

try
{
    var reservaInvalida = new Reserva(diasReservados: 3);
    reservaInvalida.CadastrarSuite(suiteStandard);
    reservaInvalida.CadastrarHospedes(new[]
    {
        new Pessoa("Fabio", "Lima"),
        new Pessoa("Gisele", "Alves"),
        new Pessoa("Hugo", "Barros")
    });
}
catch (CapacidadeExcedidaException ex)
{
    Console.WriteLine($"Reserva rejeitada: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Demonstração concluída.");
