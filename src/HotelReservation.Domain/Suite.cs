namespace HotelReservation.Domain;

/// <summary>
/// Representa uma suíte do hotel, com tipo, capacidade máxima de hóspedes
/// e valor da diária (RN-005: capacidade e diária são validadas na criação).
/// </summary>
public class Suite
{
    public Suite(string tipoSuite, int capacidade, decimal valorDiaria)
    {
        if (string.IsNullOrWhiteSpace(tipoSuite))
        {
            throw new ArgumentException("O tipo da suíte é obrigatório.", nameof(tipoSuite));
        }

        if (capacidade <= 0)
        {
            throw new ArgumentException(
                "A capacidade da suíte deve ser maior que zero.", nameof(capacidade));
        }

        if (valorDiaria < 0)
        {
            throw new ArgumentException(
                "O valor da diária não pode ser negativo.", nameof(valorDiaria));
        }

        TipoSuite = tipoSuite.Trim();
        Capacidade = capacidade;
        ValorDiaria = valorDiaria;
    }

    public string TipoSuite { get; }
    public int Capacidade { get; }

    /// <summary>Valor monetário em <see cref="decimal"/>, adequado para cálculos financeiros exatos.</summary>
    public decimal ValorDiaria { get; }

    public override string ToString() =>
        $"Suíte {TipoSuite} (capacidade: {Capacidade}, diária: {ValorDiaria:C})";
}
