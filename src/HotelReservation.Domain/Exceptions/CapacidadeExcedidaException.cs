namespace HotelReservation.Domain.Exceptions;

/// <summary>
/// Lançada quando a quantidade de hóspedes excede a capacidade da suíte (RN-001 / DIO-001).
/// </summary>
public class CapacidadeExcedidaException : DomainException
{
    public CapacidadeExcedidaException(int quantidadeHospedes, int capacidadeSuite)
        : base($"A suíte comporta no máximo {capacidadeSuite} hóspede(s), " +
               $"mas foram informados {quantidadeHospedes}.")
    {
        QuantidadeHospedes = quantidadeHospedes;
        CapacidadeSuite = capacidadeSuite;
    }

    public int QuantidadeHospedes { get; }
    public int CapacidadeSuite { get; }
}
