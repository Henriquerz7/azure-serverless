
# Azure Serverless Project

Este projeto implementa um sistema serverless utilizando Azure Logic Apps, Azure Functions e Azure Storage Queue.

## Estrutura

- **LogicApp**: Contém a definição da Logic App com um HTTP Trigger que envia mensagens para uma fila.
- **Functions**:
  - **FnSalvarSQL**: Salva mensagens no banco SQL.
  - **FnLerFila**: Lê mensagens da fila e chama a `FnSalvarSQL`.
- **SQL**: Script para criar a tabela `Mensagens`.

## Configuração

1. Configure a Logic App utilizando o arquivo `logicapp-definition.json`.
2. Crie a fila `mensagens` no Azure Storage.
3. Configure as Azure Functions:
   - Adicione variáveis de ambiente `SqlConnectionString` e `SalvarSqlEndpoint`.
4. Crie o banco de dados e a tabela utilizando o script em `SQL/create-table.sql`.

## Teste

- Utilize o Postman para enviar requisições ao endpoint da Logic App.
