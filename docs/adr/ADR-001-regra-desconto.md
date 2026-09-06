# ADR-001 — Regra de desconto: `DiasReservados >= 10`

- **Status:** Aceito
- **Data:** 2026-09-06
- **Contexto:** Desafio "Sistema de Hospedagem" da trilha .NET da DIO
  ([digitalinnovationone/trilha-net-explorando-desafio](https://github.com/digitalinnovationone/trilha-net-explorando-desafio)).

## Problema

O material oficial da DIO contém uma inconsistência textual:

- Na seção **Contexto**, o enunciado diz que o desconto de 10% se aplica a reservas
  "para caso a reserva seja para um período **maior que 10 dias**" (ou seja, `> 10`).
- Na seção **Regras e validações**, item 3, a regra detalhada diz:
  "Caso seja feita uma reserva **igual ou maior que 10 dias**, deverá ser concedido
  um desconto de 10%" (ou seja, `>= 10`). O código-base do template do desafio
  também segue essa forma.

## Evidência considerada

1. As regras detalhadas do README oficial (item 3) prevalecem sobre o texto
   descritivo de contexto por serem a especificação normativa.
2. O template de código fornecido pela DIO utiliza a condição `>= 10`.
3. Em domínios de hospedagem, descontos por longa estadia normalmente incluem o
   limiar ("a partir de 10 diárias"), o que reforça a interpretação inclusiva.

## Decisão

Adotar como regra normativa:

```csharp
DiasReservados >= 10  // aplica desconto de 10%
```

O limiar e o percentual estão explícitos como constantes públicas em `Reserva`:

- `Reserva.DiasMinimosParaDesconto = 10`
- `Reserva.PercentualDescontoLongaEstadia = 0.10m`

## Consequências

- Uma reserva de **exatamente 10 dias** recebe 10% de desconto.
- Uma reserva de **9 dias** paga valor integral.
- O comportamento é garantido por testes de fronteira obrigatórios:
  - `ReservaCalculoTests.CalcularValorDiaria_ComExatamenteDezDias_DeveAplicarDesconto`
  - `ReservaCalculoTests.CalcularValorDiaria_ComNoveDias_DeveCobrarValorIntegral`
  - `FluxoReservaTests.FluxoCompleto_ReservaNaFronteiraDeDezDias_DeveAplicarDesconto`

## Impacto

Caso a DIO padronize a regra como `> 10` no futuro, a alteração é cirúrgica:
mudar a comparação em `Reserva.PossuiDescontoLongaEstadia` e ajustar os testes de
fronteira. A divergência permanece documentada neste ADR.
