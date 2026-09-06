# CI e CodeQL

Os workflows canônicos do projeto ficam em:

- `.github/workflows/ci.yml` — restore, build Release, testes e coleta de cobertura em push e pull request para `main`;
- `.github/workflows/codeql.yml` — análise estática de segurança para C# em push, pull request e agendamento semanal.

Este diretório mantém apenas a documentação de CI. As antigas cópias YAML foram removidas para evitar divergência entre documentação e configuração executável.

## Verificação

Após abrir ou atualizar um pull request, confira em **Actions** se os workflows foram reconhecidos e executados. O projeto só deve declarar CI/CodeQL como aprovados depois de uma execução real bem-sucedida.

Badges e links no `README.md` apontam diretamente para os workflows ativos.
