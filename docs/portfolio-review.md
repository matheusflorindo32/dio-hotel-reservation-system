# Avaliação de portfólio (autoavaliação crítica)

Avaliação honesta do projeto sob a ótica de recrutadores e avaliadores técnicos.
Nenhuma nota é inflada; pontuações abaixo de 9 são justificadas.

| Critério | Nota (0–10) | Justificativa |
|---|---|---|
| Clareza | 9 | Camada A (requisitos DIO) claramente separada da Camada B (evolução). Matriz de rastreabilidade liga cada requisito a código e teste. |
| Código | 9 | C# idiomático, guard clauses, exceções de domínio, encapsulamento real, `decimal` para dinheiro, zero warnings com `TreatWarningsAsErrors`. |
| Arquitetura | 9 | Deliberadamente simples e justificada em ADR. Domínio puro sem dependências permite evolução (V2+) sem retrabalho. |
| Testes | 9 | 49 testes reais, fronteira de desconto testada nos dois lados, valores monetários exatos, fluxo de integração completo. |
| Documentação | 9 | README completo, regras de negócio, estratégia de testes, ADRs e diagramas Mermaid renderizáveis no GitHub. |
| GitHub | 8 | Templates de issue/PR, CONTRIBUTING, SECURITY e CHANGELOG presentes. Falta histórico de colaboração real (projeto individual). |
| CI | 7 | Workflows de build+test e CodeQL prontos e revisados, mas a ativação em `.github/workflows/` ficou pendente de um passo manual (limitação de escopo do token de publicação — ver `docs/ci/README.md`). Sem CD (fora de escopo na V1) e sem matriz multi-OS. |
| Segurança | 9 | Sem segredos, sem dependências além de xUnit/coverlet, `.gitignore` cobre artefatos locais, SECURITY.md com política de reporte, workflow CodeQL pronto. |
| Manutenibilidade | 9 | Métodos pequenos, nomes expressivos, invariantes no construtor, decisões registradas em ADR. |
| Apresentação | 8 | README objetivo com demonstração real de saída. Não há GIF/screenshot animado — optou-se por saída textual verificável. |
| Potencial de evolução | 9 | Roadmap V2–V5 definido; domínio desacoplado de I/O comporta API, persistência e SaaS sem alteração estrutural. |

**Média: 8,7 / 10**

## Pontos fortes

1. Rastreabilidade completa requisito → código → teste, verificável por
   qualquer avaliador.
2. Decisão normativa sobre a divergência textual da DIO documentada e protegida
   por teste de fronteira.
3. Disciplina de escopo: nenhuma abstração prematura.

## Pontos de melhoria conhecidos

1. Ativar os workflows de CI (passo manual único documentado em `docs/ci/`).
2. Cobertura de linhas pode subir com testes para `ToString()` (baixo valor —
   não priorizado).
3. CI poderia incluir matriz Windows/Linux e cache de pacotes NuGet.
4. Ausência de demonstração visual (GIF) no README.
