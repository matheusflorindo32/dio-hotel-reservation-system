using HotelReservation.Domain;

namespace HotelReservation.UnitTests;

public class PessoaTests
{
    [Fact]
    public void CriarPessoa_ComDadosValidos_DeveArmazenarNomeESobrenome()
    {
        var pessoa = new Pessoa("Ana", "Silva");

        Assert.Equal("Ana", pessoa.Nome);
        Assert.Equal("Silva", pessoa.Sobrenome);
        Assert.Equal("Ana Silva", pessoa.NomeCompleto);
    }

    [Fact]
    public void CriarPessoa_ComEspacosExtras_DeveNormalizarOsDados()
    {
        var pessoa = new Pessoa("  Ana  ", "  Silva  ");

        Assert.Equal("Ana Silva", pessoa.NomeCompleto);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CriarPessoa_ComNomeInvalido_DeveLancarArgumentException(string? nome)
    {
        Assert.Throws<ArgumentException>(() => new Pessoa(nome!, "Silva"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CriarPessoa_ComSobrenomeInvalido_DeveLancarArgumentException(string? sobrenome)
    {
        Assert.Throws<ArgumentException>(() => new Pessoa("Ana", sobrenome!));
    }
}
