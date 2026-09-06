namespace HotelReservation.Domain;

/// <summary>
/// Representa um hóspede da reserva.
/// Mantém apenas os dados mínimos necessários (minimização de dados pessoais).
/// </summary>
public class Pessoa
{
    public Pessoa(string nome, string sobrenome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do hóspede é obrigatório.", nameof(nome));
        }

        if (string.IsNullOrWhiteSpace(sobrenome))
        {
            throw new ArgumentException("O sobrenome do hóspede é obrigatório.", nameof(sobrenome));
        }

        Nome = nome.Trim();
        Sobrenome = sobrenome.Trim();
    }

    public string Nome { get; }
    public string Sobrenome { get; }

    public string NomeCompleto => $"{Nome} {Sobrenome}";

    public override string ToString() => NomeCompleto;
}
