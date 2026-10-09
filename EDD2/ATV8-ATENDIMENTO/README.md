# Sistema de Atendimento com Fila

Sistema acadêmico de gerenciamento de senhas e guichês em C# com Windows Forms.

## Como executar

```bash
dotnet run
```

## O que faz

- Gerar senhas sequenciais
- Adicionar guichês
- Chamar senhas para atendimento
- Ver histórico de atendimentos

## Estrutura

```
Models/
├── Senha.cs        # Entidade de senha
├── Senhas.cs       # Fila de senhas
├── Guiche.cs       # Entidade de guichê
└── Guiches.cs      # Gerenciador de guichês
```

## Tecnologias

- C# (.NET 10.0)
- Windows Forms
