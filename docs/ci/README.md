# Workflows de CI — ativação

Este diretório contém os dois workflows prontos do projeto:

- `ci.yml` — restore → build (Release, warnings como erros) → testes, em todo
  push na `main` e em pull requests;
- `codeql.yml` — análise estática de segurança (C#) em push, PR e agendamento
  semanal.

## Por que eles estão aqui e não em `.github/workflows/`?

A publicação automatizada deste repositório foi feita via API com um token sem o
escopo `workflow`, e o GitHub recusa (403) a criação remota de arquivos em
`.github/workflows/` sem esse escopo. Em vez de omitir o CI, os arquivos ficam
aqui, prontos e revisados.

## Como ativar (uma única vez, ~1 minuto)

Com git local:

```bash
git clone https://github.com/matheusflorindo32/dio-hotel-reservation-system.git
cd dio-hotel-reservation-system
mkdir -p .github/workflows
cp docs/ci/ci.yml docs/ci/codeql.yml .github/workflows/
git add .github/workflows
git commit -m "ci: activate github actions workflows"
git push
```

Ou pela interface web: **Add file → Upload files**, enviando os dois arquivos
para o caminho `.github/workflows/`.

Após o push, os badges do README passam a exibir o status real das execuções.
