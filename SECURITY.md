# Política de Segurança

## Versões suportadas

| Versão | Suportada |
|---|---|
| V1 (branch `main`) | ✅ |

## Reportar uma vulnerabilidade

Se você encontrar uma vulnerabilidade de segurança, **não abra uma issue
pública**. Entre em contato pelo
[LinkedIn](https://www.linkedin.com/in/matheus-florindo-de-deus-b953b017a/) ou
pelo e-mail disponível no perfil do GitHub, descrevendo o problema e os passos
para reprodução.

Você receberá uma resposta em até 7 dias, com a avaliação e, se confirmada, o
plano de correção.

## Higiene de segurança deste repositório

- Nenhum segredo, credencial, chave ou token no código ou no histórico;
- Nenhum dado pessoal real (os exemplos usam dados fictícios);
- Dependências mínimas: apenas xUnit e coverlet (desenvolvimento/testes);
- `.gitignore` exclui artefatos locais, builds e arquivos de IDE;
- Análise estática com GitHub CodeQL (workflow documentado no repositório);
- Build com warnings tratados como erros.
