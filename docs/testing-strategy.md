# Estratégia de testes

## Ferramentas

- **xUnit** — framework de testes;
- **coverlet.collector** — coleta de cobertura no CI;
- nenhum framework de mock: o domínio é puro e não possui dependências externas, portanto mocks seriam infraestrutura artificial.

## Pirâmide de testes

| Projeto | Quantidade | Propósito |
|---|---:|---|
| `HotelReservation.UnitTests` | 45 | Regras de negócio isoladas: capacidade, cálculo, desconto e validações |
| `HotelReservation.IntegrationTests` | 4 | Fluxo completo da reserva de ponta a ponta |

**Total: 49 testes.** A execução do GitHub Actions no PR #1 confirmou **45/45 unitários + 4/4 integração**, com build Release concluído com **0 warnings e 0 errors** usando .NET SDK 8.0.424.

## Cobertura das regras

### Capacidade (RN-001)

- hóspedes < capacidade → sucesso;
- hóspedes = capacidade → sucesso;
- hóspedes > capacidade → `CapacidadeExcedidaException`;
- cadastros sucessivos validam capacidade acumulada;
- estado da reserva permanece consistente após exceção.

### Desconto (RN-004)

- 1, 5 e 9 dias → sem desconto;
- **10 dias → desconto aplicado (fronteira obrigatória)**;
- 11 e 30 dias → desconto aplicado;
- comparação 9 vs. 10 dias com a mesma diária para provar o limiar exato.

### Cálculo (RN-003)

Valores monetários são testados com `decimal`, inclusive diárias com centavos.

### Quantidade de hóspedes (RN-002)

Quantidades 0, 1, 2 e 5 são verificadas independentemente.

### Validações (RN-005)

Entradas inválidas relevantes possuem testes dedicados.

## Cobertura de código — evidência do CI

O workflow `.github/workflows/ci.yml` executa `dotnet test` com `XPlat Code Coverage` e publica os relatórios Cobertura como artefato.

No PR #1, os relatórios produzidos sobre `HotelReservation.Domain` foram:

| Suite | Line coverage | Branch coverage |
|---|---:|---:|
| Unit tests | **94,52%** | **100%** |
| Integration tests | **67,12%** | **59,09%** |

Esses relatórios são deliberadamente apresentados separadamente: cada assembly de teste mede a cobertura do domínio a partir de um conjunto diferente de cenários. Somar ou tirar média simples entre eles produziria uma métrica enganosa.

A suíte unitária deixa de fora principalmente caminhos de apresentação como `ToString()`. Não são adicionados testes sem valor apenas para elevar percentuais.

## O que este projeto deliberadamente não testa

- formatação textual da aplicação console;
- getters triviais sem lógica de negócio.

O objetivo é maximizar confiança nas regras, não a quantidade artificial de testes.
