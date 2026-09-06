# ADR-002 — Estrutura simplificada: Domain + ConsoleApp

- **Status:** Aceito
- **Data:** 2026-09-06

## Contexto

A estrutura de referência sugeria três projetos em `src/` (Domain, Application,
Console). O desafio da DIO, porém, é deliberadamente pequeno: três classes de
domínio e quatro regras de negócio.

## Decisão

Adotar dois projetos em `src/`:

- `HotelReservation.Domain` — entidades e regras de negócio (Pessoa, Suite,
  Reserva e exceções de domínio), sem nenhuma dependência externa.
- `HotelReservation.ConsoleApp` — demonstração executável do domínio.

O nome `ConsoleApp` (em vez de `Console`) evita colisão de namespace com
`System.Console`, o que geraria ambiguidade e usings confusos.

## Justificativa

1. **Elegância > quantidade de padrões.** Uma camada Application sem casos de uso
   além da própria orquestração trivial seria uma casca vazia — complexidade
   artificial que obscureceria a solução do desafio.
2. **YAGNI.** Não há serviços de aplicação, repositórios ou integrações na V1.
   Interfaces, factories e camadas extras seriam abstrações prematuras.
3. **Caminho de evolução preservado.** O Domain não referencia nenhum projeto e
   não conhece I/O: quando a V2 (API ASP.NET Core) chegar, as camadas Application
   e Infrastructure podem ser adicionadas sem alterar o domínio
   (ver `docs/roadmap.md`).

## Consequências

- Leitura direta do código do desafio, sem indireções.
- Testes de integração exercitam o fluxo completo contra o domínio real, sem
  infraestrutura artificial.
- A separação em camadas adicionais fica registrada como evolução futura, não
  como dívida.
