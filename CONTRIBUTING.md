# Contribuindo

Obrigado pelo interesse em contribuir!

## Como contribuir

1. Faça um fork do repositório e crie uma branch a partir de `main`:
   `git checkout -b feat/minha-melhoria`
2. Garanta que o build e os testes passam localmente:
   ```bash
   dotnet restore
   dotnet build --configuration Release
   dotnet test --configuration Release
   ```
3. Siga os padrões do projeto:
   - [Conventional Commits](https://www.conventionalcommits.org/pt-br/) nas
     mensagens de commit (`feat:`, `fix:`, `test:`, `docs:`, `ci:`, `chore:`);
   - estilo definido em `.editorconfig` (o build trata warnings como erros);
   - novas regras de negócio exigem testes correspondentes.
4. Abra um Pull Request preenchendo o template e associando o requisito ou issue
   relacionado.

## Princípio de escopo

Este repositório é a evolução de um desafio educacional. Melhorias são bem-vindas,
mas a solução original do desafio (Camada A) deve permanecer claramente
identificável — evite abstrações que a obscureçam.

## Reportar problemas

Use o template de **Bug Report** ao abrir uma issue. Para vulnerabilidades de
segurança, consulte [SECURITY.md](SECURITY.md).
