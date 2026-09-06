# ADR-003 — .NET 8 LTS como versão alvo

- **Status:** Aceito
- **Data:** 2026-09-06

## Contexto

O projeto precisa de uma versão do .NET moderna, estável e amplamente suportada,
coerente com o ecossistema usado na trilha da DIO e com ambientes de CI.

## Decisão

Usar **.NET 8 (LTS)** em todos os projetos da solution.

## Justificativa

1. **Suporte de longo prazo:** .NET 8 é LTS, com suporte oficial até novembro de
   2026 e amplo suporte em ferramentas, IDEs e runners de CI.
2. **Disponibilidade verificada:** o SDK 8.0.424 está instalado e validado no
   ambiente de desenvolvimento deste projeto; o workflow de CI fixa `8.0.x`.
3. **Recursos de linguagem:** C# 12 com Nullable Reference Types, Implicit Usings
   e analisadores ativados (`Directory.Build.props`), sem exigir recursos
   exóticos.

## Consequências

- Build e testes reproduzíveis em qualquer máquina com SDK 8.x.
- A migração futura para um LTS mais recente é um passo isolado de `TargetFramework`,
  sem impacto arquitetural.
