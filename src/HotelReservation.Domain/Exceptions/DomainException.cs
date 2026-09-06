namespace HotelReservation.Domain.Exceptions;

/// <summary>
/// Exceção base para violações de regras de negócio do domínio de reservas.
/// Permite que consumidores distingam erros de domínio de erros técnicos.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
