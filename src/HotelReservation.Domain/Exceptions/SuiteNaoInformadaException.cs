namespace HotelReservation.Domain.Exceptions;

/// <summary>
/// Lançada quando uma operação da reserva exige uma suíte que ainda não foi cadastrada (RN-005).
/// </summary>
public class SuiteNaoInformadaException : DomainException
{
    public SuiteNaoInformadaException()
        : base("Nenhuma suíte foi cadastrada na reserva. Cadastre a suíte antes desta operação.")
    {
    }
}
