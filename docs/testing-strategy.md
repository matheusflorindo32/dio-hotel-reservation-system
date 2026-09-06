# Estratégia de testes

## Ferramentas

- **xUnit** — framework de testes amplamente adotado no ecossistema .NET.
- **coverlet.collector** — coleta de cobertura (opcional no CI).
- Nenhum framework de mock: o domínio é puro e não possui dependências externas,
  portanto mocks seriam infraestrutura artificial.

## Pirâmide de testes

| Projeto | Quantidade | Propósito |
|---|---|---|
| `HotelReservation.UnitTests` | 45 | Regras de negócio isoladas: capacidade, cálculo, desconto, validações |
| `HotelReservation.IntegrationTests` | 4 | Fluxo completo da reserva de ponta a ponta |

Total: **49 testes, todos passando** (verificado em 2026-09-06, .NET 8.0.424).

## Cobertura das regras

### Capacidade (RN-001)

- hóspedes < capacidade → sucesso;
- hóspedes = capacidade → sucesso (limite inclusivo);
- hóspedes > capacidade → `CapacidadeExcedidaException`;
- cadastros sucessivos não podem furar a capacidade acumulada (regressão);
- estado da reserva inalterado após a exceção.

### Desconto (RN-004)

- 1, 5 e 9 dias → sem desconto (valor integral);
- **10 dias → desconto aplicado (TESTE DE FRONTEIRA OBRIGATÓRIO)**;
- 11 e 30 dias → desconto aplicado;
- fronteira dupla 9 vs. 10 dias com o mesmo valor de diária (250,00), provando
  que o desconto ativa exatamente no limiar.

### Cálculo (RN-003)

- Valores monetários exatos com `decimal`, incluindo diárias com centavos
  (ex.: 199,90) para detectar erros de arredondamento.

### Quantidade de hóspedes (RN-002)

- Diferentes quantidades (0, 1, 2, 5) verificadas independentemente.

### Validações (RN-005)

- Todas as entradas inválidas da tabela de regras possuem teste dedicado.

## Cobertura de código (medida real)

Medida com coverlet na execução dos testes unitários sobre `HotelReservation.Domain`:

- **Linhas: 94,5%**
- **Branches: 100%**

Os poucos pontos não cobertos são os overrides de `ToString()` (usados apenas na
demonstração console). Não adicionamos testes de baixo valor apenas para inflar
a métrica — cobertura é consequência, não objetivo.

## O que este projeto deliberadamente não testa

- Formatação de saída do console (camada de demonstração, sem regra de negócio);
- Comportamentos triviais de getters.

Evitamos testes que apenas repetem a implementação.
