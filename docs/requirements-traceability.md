# Matriz de rastreabilidade de requisitos

Requisitos originais do desafio DIO
([fonte oficial](https://github.com/digitalinnovationone/trilha-net-explorando-desafio))
mapeados para implementação e testes. Status verificado por execução real de
`dotnet test` em 2026-09-06 (49/49 testes passando, .NET 8.0.424).

## Requisitos do desafio (Camada A)

| ID | Requisito DIO | Implementação | Teste | Status |
|---|---|---|---|---|
| DIO-001 | Não reservar suíte com capacidade menor que a quantidade de hóspedes (exception) | `Reserva.CadastrarHospedes` → `CapacidadeExcedidaException` (`src/HotelReservation.Domain/Reserva.cs`) | `ReservaCapacidadeTests.CadastrarHospedes_AcimaDaCapacidade_DeveLancarCapacidadeExcedidaException` + 4 testes complementares | PASS |
| DIO-002 | `ObterQuantidadeHospedes()` retorna o total de hóspedes | `Reserva.ObterQuantidadeHospedes` | `ReservaCalculoTests.ObterQuantidadeHospedes_DeveRetornarQuantidadeCadastrada` (1, 2 e 5 hóspedes) | PASS |
| DIO-003 | `CalcularValorDiaria()` = DiasReservados × ValorDiaria | `Reserva.CalcularValorDiaria` | `ReservaCalculoTests.CalcularValorDiaria_ComMenosDeDezDias_NaoDeveAplicarDesconto` (valores monetários exatos) | PASS |
| DIO-004 | Desconto de 10% para reservas de 10 dias ou mais | `Reserva.CalcularValorDiaria` + `Reserva.PossuiDescontoLongaEstadia` (ver [ADR-001](adr/ADR-001-regra-desconto.md)) | `ReservaCalculoTests.CalcularValorDiaria_ComExatamenteDezDias_DeveAplicarDesconto` (fronteira obrigatória) + theory com 10/11/30 dias | PASS |

## Regras de robustez adicionadas (Camada B)

| ID | Regra | Implementação | Teste | Status |
|---|---|---|---|---|
| RN-005a | Dias reservados ≤ 0 rejeitados | Construtor de `Reserva` | `ReservaValidacaoTests.CriarReserva_ComDiasInvalidos_DeveLancarArgumentException` | PASS |
| RN-005b | Capacidade ≤ 0 rejeitada | Construtor de `Suite` | `SuiteTests.CriarSuite_ComCapacidadeInvalida_DeveLancarArgumentException` | PASS |
| RN-005c | Diária negativa rejeitada | Construtor de `Suite` | `SuiteTests.CriarSuite_ComValorDiariaNegativo_DeveLancarArgumentException` | PASS |
| RN-005d | Suíte ausente em operações | `Reserva.CadastrarHospedes` / `CalcularValorDiaria` → `SuiteNaoInformadaException` | `ReservaValidacaoTests.*_SemSuite_DeveLancarSuiteNaoInformadaException` | PASS |
| RN-005e | Lista de hóspedes nula ou vazia | `Reserva.CadastrarHospedes` | `ReservaValidacaoTests.CadastrarHospedes_ComListaNula...` / `..._ComListaVazia...` | PASS |
| RN-005f | Nome/sobrenome obrigatórios | Construtor de `Pessoa` | `PessoaTests.CriarPessoa_ComNomeInvalido...` / `..._ComSobrenomeInvalido...` | PASS |

## Fluxo completo (integração)

| ID | Cenário | Teste | Status |
|---|---|---|---|
| INT-01 | Fluxo completo sem desconto (5 dias) | `FluxoReservaTests.FluxoCompleto_ReservaCurta_DeveCalcularValorSemDesconto` | PASS |
| INT-02 | Fluxo completo com desconto (12 dias) | `FluxoReservaTests.FluxoCompleto_ReservaLonga_DeveCalcularValorComDescontoDeDezPorCento` | PASS |
| INT-03 | Fronteira de 10 dias no fluxo completo | `FluxoReservaTests.FluxoCompleto_ReservaNaFronteiraDeDezDias_DeveAplicarDesconto` | PASS |
| INT-04 | Rejeição por capacidade no fluxo completo | `FluxoReservaTests.FluxoCompleto_HospedesAcimaDaCapacidade_DeveRejeitarReserva` | PASS |
