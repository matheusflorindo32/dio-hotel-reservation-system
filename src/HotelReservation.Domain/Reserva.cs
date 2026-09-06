using HotelReservation.Domain.Exceptions;

namespace HotelReservation.Domain;

/// <summary>
/// Representa uma reserva de hospedagem, relacionando hóspedes e suíte.
/// Centraliza as regras de negócio do desafio DIO:
/// RN-001 (capacidade), RN-002 (quantidade de hóspedes),
/// RN-003 (valor da diária) e RN-004 (desconto para longa estadia).
/// </summary>
public class Reserva
{
    private readonly List<Pessoa> _hospedes = new();

    /// <summary>Quantidade mínima de dias reservados que concede desconto (RN-004 / ADR-001).</summary>
    public const int DiasMinimosParaDesconto = 10;

    /// <summary>Percentual de desconto aplicado em reservas de longa estadia (RN-004).</summary>
    public const decimal PercentualDescontoLongaEstadia = 0.10m;

    public Reserva(int diasReservados)
    {
        if (diasReservados <= 0)
        {
            throw new ArgumentException(
                "A quantidade de dias reservados deve ser maior que zero.", nameof(diasReservados));
        }

        DiasReservados = diasReservados;
    }

    /// <summary>Visão somente leitura dos hóspedes, preservando o encapsulamento da coleção interna.</summary>
    public IReadOnlyCollection<Pessoa> Hospedes => _hospedes.AsReadOnly();

    public Suite? Suite { get; private set; }

    public int DiasReservados { get; }

    /// <summary>
    /// Cadastra a suíte da reserva (RN-005: suíte ausente é rejeitada).
    /// </summary>
    public void CadastrarSuite(Suite suite)
    {
        ArgumentNullException.ThrowIfNull(suite);
        Suite = suite;
    }

    /// <summary>
    /// Cadastra os hóspedes da reserva, validando a capacidade da suíte
    /// (RN-001 / DIO-001 e RN-005: lista nula ou vazia é rejeitada).
    /// </summary>
    /// <exception cref="ArgumentNullException">Lista de hóspedes nula.</exception>
    /// <exception cref="DomainException">Lista de hóspedes vazia.</exception>
    /// <exception cref="SuiteNaoInformadaException">Suíte ainda não cadastrada.</exception>
    /// <exception cref="CapacidadeExcedidaException">Hóspedes acima da capacidade da suíte.</exception>
    public void CadastrarHospedes(IEnumerable<Pessoa> hospedes)
    {
        ArgumentNullException.ThrowIfNull(hospedes);

        if (Suite is null)
        {
            throw new SuiteNaoInformadaException();
        }

        var listaHospedes = hospedes.ToList();

        if (listaHospedes.Count == 0)
        {
            throw new DomainException("A reserva deve possuir ao menos um hóspede.");
        }

        // A capacidade é validada contra o total acumulado, não apenas contra a
        // nova lista: chamadas sucessivas não podem furar a lotação da suíte.
        int totalAposCadastro = _hospedes.Count + listaHospedes.Count;

        if (totalAposCadastro > Suite.Capacidade)
        {
            throw new CapacidadeExcedidaException(totalAposCadastro, Suite.Capacidade);
        }

        _hospedes.AddRange(listaHospedes);
    }

    /// <summary>
    /// Retorna a quantidade total de hóspedes cadastrados (RN-002 / DIO-002).
    /// </summary>
    public int ObterQuantidadeHospedes() => _hospedes.Count;

    /// <summary>
    /// Calcula o valor total da reserva: DiasReservados × ValorDiaria (RN-003 / DIO-003),
    /// com desconto de 10% quando DiasReservados &gt;= 10 (RN-004 / DIO-004).
    /// </summary>
    /// <exception cref="SuiteNaoInformadaException">Suíte ainda não cadastrada.</exception>
    public decimal CalcularValorDiaria()
    {
        if (Suite is null)
        {
            throw new SuiteNaoInformadaException();
        }

        decimal valorTotal = DiasReservados * Suite.ValorDiaria;

        if (PossuiDescontoLongaEstadia)
        {
            valorTotal *= 1 - PercentualDescontoLongaEstadia;
        }

        return valorTotal;
    }

    /// <summary>Indica se a reserva atinge a regra de desconto por longa estadia (RN-004).</summary>
    public bool PossuiDescontoLongaEstadia => DiasReservados >= DiasMinimosParaDesconto;
}
