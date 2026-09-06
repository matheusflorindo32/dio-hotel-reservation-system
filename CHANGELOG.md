# Changelog

Todas as mudanças notáveis deste projeto são documentadas aqui, seguindo
[Keep a Changelog](https://keepachangelog.com/pt-BR/1.1.0/) e
[Versionamento Semântico](https://semver.org/lang/pt-BR/).

## [1.0.0] - 2026-09-06

### Adicionado

- Domínio de reservas com `Pessoa`, `Suite` e `Reserva` (requisitos DIO-001 a
  DIO-004);
- Regra de desconto de 10% para reservas de 10 dias ou mais (decisão normativa
  em ADR-001);
- Exceções de domínio `CapacidadeExcedidaException` e
  `SuiteNaoInformadaException`;
- Validações de dados inválidos (RN-005);
- Aplicação console de demonstração com três cenários;
- 45 testes unitários e 4 testes de integração (xUnit);
- Workflows de CI (build + testes em Release) e CodeQL prontos em `docs/ci/`,
  com ativação pendente em um passo manual (ver `docs/ci/README.md`);
- Documentação: arquitetura, regras de negócio, matriz de rastreabilidade,
  estratégia de testes, roadmap, ADRs e diagramas Mermaid;
- Arquivos de governança: LICENSE (MIT), CONTRIBUTING, SECURITY, templates de
  issue e pull request, `.editorconfig` e `.gitignore`.
