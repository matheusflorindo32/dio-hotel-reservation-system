# Roadmap de evolução

Este roadmap descreve a evolução potencial do projeto. **Apenas a V1 está
implementada** — as demais versões são direção arquitetural, não promessa de
escopo imediato.

## V1 — LAB (atual)

Sistema de hospedagem em console, com domínio puro, testes automatizados e CI.

- [x] Entidades `Pessoa`, `Suite` e `Reserva`
- [x] Regras RN-001 a RN-005 com exceções de domínio
- [x] Testes unitários e de integração (xUnit)
- [x] CI com GitHub Actions
- [x] Documentação, ADRs e matriz de rastreabilidade

## V2 — Backend

ASP.NET Core Web API sobre o mesmo domínio (o Domain já não depende de I/O).

- Recursos: hotéis, quartos, reservas, disponibilidade, clientes, cancelamentos
- Endpoints REST com validação e tratamento de erros padronizado (ProblemDetails)
- Novas camadas `Application` e `Infrastructure` (ver ADR-002)

## V3 — Persistência

- PostgreSQL com EF Core e migrations
- Repositórios e unit of work
- Testes de integração com banco real (Testcontainers)

## V4 — SaaS

- Autenticação e autorização (multiusuário)
- Multi-property (vários hotéis por conta)
- Pagamentos, dashboards e relatórios

## V5 — Produção

- Docker e deploy em cloud
- Observabilidade (logs estruturados, métricas, tracing)
- Caching, rate limiting e security hardening
- Pipeline de deploy (CD)

## Critério de progressão

Cada versão só inicia quando a anterior estiver estável e testada. O domínio da
V1 foi desenhado para não precisar de alterações estruturais nas versões
seguintes — apenas composição com novas camadas.
