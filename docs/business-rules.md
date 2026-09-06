# Regras de negócio

Todas as regras são explícitas, testáveis e possuem testes correspondentes.
A rastreabilidade requisito → código → teste está em
[requirements-traceability.md](requirements-traceability.md).

## RN-001 — Capacidade da suíte

A quantidade de hóspedes não pode superar a capacidade da suíte.

- **Origem:** regra 1 do desafio DIO (DIO-001).
- **Implementação:** `Reserva.CadastrarHospedes` valida o total acumulado de
  hóspedes contra `Suite.Capacidade`.
- **Comportamento:** lança `CapacidadeExcedidaException` (exceção de domínio
  específica, com a quantidade informada e a capacidade). A reserva permanece
  inalterada.

## RN-002 — Quantidade de hóspedes

`ObterQuantidadeHospedes()` retorna exatamente a quantidade de hóspedes
cadastrados.

- **Origem:** regra 2 do desafio DIO (DIO-002).
- **Implementação:** `Reserva.ObterQuantidadeHospedes` retorna a contagem da
  coleção interna.

## RN-003 — Valor básico da reserva

```
ValorTotal = DiasReservados × Suite.ValorDiaria
```

- **Origem:** regra 2 do desafio DIO (DIO-003).
- **Implementação:** `Reserva.CalcularValorDiaria` usando `decimal` para
  precisão monetária exata.

## RN-004 — Desconto de longa estadia

Quando `DiasReservados >= 10`, aplica-se desconto de 10%:

```
ValorFinal = ValorTotal × 0.90m
```

- **Origem:** regra 3 do desafio DIO (DIO-004).
- **Decisão normativa:** o enunciado da DIO diverge entre "maior que 10 dias" e
  "igual ou maior que 10 dias". Este projeto adota **`>= 10`** — justificativa
  completa no [ADR-001](adr/ADR-001-regra-desconto.md), garantida por teste de
  fronteira obrigatório.

## RN-005 — Dados inválidos

Tratamento de entradas inválidas (regra adicional de robustez, sem alterar os
requisitos da DIO):

| Caso | Comportamento |
|---|---|
| Dias reservados = 0 ou negativo | `ArgumentException` no construtor de `Reserva` |
| Capacidade = 0 ou negativa | `ArgumentException` no construtor de `Suite` |
| Valor de diária negativo | `ArgumentException` no construtor de `Suite` |
| Valor de diária = 0 | Aceito (suíte cortesia é um cenário válido) |
| Suíte ausente (`null`) | `ArgumentNullException` em `CadastrarSuite` |
| Lista de hóspedes nula | `ArgumentNullException` em `CadastrarHospedes` |
| Lista de hóspedes vazia | `DomainException` (reserva exige ao menos 1 hóspede) |
| Operação sem suíte cadastrada | `SuiteNaoInformadaException` |
| Nome/sobrenome vazios | `ArgumentException` no construtor de `Pessoa` |

Critério de distinção: dados estruturalmente inválidos → exceções de argumento;
violações de regra de negócio → exceções de domínio.
