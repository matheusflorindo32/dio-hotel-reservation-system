# Avaliação de portfólio — revisão crítica

Avaliação do projeto sob a ótica de recrutadores e avaliadores técnicos, atualizada após a ativação real de CI e CodeQL no PR #1.

| Critério | Nota (0–10) | Justificativa |
|---|---:|---|
| Clareza | 9,5 | Camada educacional DIO permanece rastreável e separada das melhorias de engenharia. |
| Código | 9,5 | C# idiomático, guard clauses, exceções de domínio, encapsulamento e `decimal` para dinheiro. |
| Arquitetura | 9,2 | Simples por decisão, suficiente para o escopo e pronta para evolução sem padrões artificiais. |
| Testes | 9,7 | 49/49 no CI, fronteiras críticas, valores monetários exatos e integração ponta a ponta. |
| Documentação | 9,6 | README, regras, estratégia de testes, ADRs, Mermaid, roadmap e rastreabilidade. |
| GitHub | 9,3 | PR, templates, commits semânticos, workflows ativos e documentação de governança. |
| CI/CD | 9,4 | Restore, build Release, testes, cobertura e artefato em PR/push. CD não é necessário na V1 console. |
| Segurança | 9,4 | CodeQL ativo, permissões mínimas, sem segredos e SECURITY.md. |
| Manutenibilidade | 9,5 | Domínio pequeno, coeso, invariantes explícitas e baixo acoplamento. |
| Apresentação | 9,5 | Primeira dobra do README prioriza tecnologia, evidência e diferenciais verificáveis. |
| Potencial de evolução | 9,3 | Roadmap V2–V5 documentado sem fingir que a V1 já é um SaaS. |

**Média indicativa: 9,4 / 10.**

## Evidências verificadas no PR #1

- build Release: **PASS**;
- warnings: **0**;
- errors: **0**;
- testes unitários: **45/45**;
- testes de integração: **4/4**;
- total: **49/49**;
- cobertura unitária do Domain: **94,52% linhas / 100% branches**;
- artefato de coverage: **publicado pelo workflow**;
- CodeQL: **PASS**.

## Pontos fortes

1. Rastreabilidade requisito → código → teste.
2. Divergência textual da regra de desconto tratada por ADR e teste de fronteira.
3. Automação real no GitHub, em vez de badges ou claims decorativos.
4. Disciplina de escopo: sem abstrações prematuras.

## Melhorias futuras não bloqueantes

1. Fixar explicitamente `LangVersion` para máxima reprodutibilidade futura.
2. Avaliar Dependabot e branch protection conforme o repositório ganhar colaboração.
3. Considerar uma demonstração visual apenas se adicionar valor real ao portfólio.
4. Evoluir para API/persistência somente em uma fase separada do desafio DIO.

A revisão independente está em [final-review.md](final-review.md).
