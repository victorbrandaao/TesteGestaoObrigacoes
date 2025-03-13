# Teste Técnico - Sistema de Gestão de Obrigações Acessórias

## 💼 Sobre o Projeto
API REST desenvolvida como parte do processo seletivo, focada no gerenciamento de empresas e suas obrigações acessórias junto ao governo.

## 🛠 Tecnologias Utilizadas
- .NET 8
- Entity Framework Core
- PostgreSQL (Supabase)
- Swagger/OpenAPI
- Padrão REST

## 🏗 Arquitetura
O projeto segue uma arquitetura em camadas:
- Controllers: Endpoints da API
- Services: Lógica de negócio
- Models: Entidades do domínio
- Data: Acesso a dados

## 📋 Funcionalidades
### Empresas
- Cadastro de empresas
- Listagem de empresas
- Atualização de dados
- Exclusão de registro
- Busca por CNPJ

### Obrigações Acessórias
- Cadastro de obrigações
- Listagem por empresa
- Atualização de status
- Controle de periodicidade
  - Diária
  - Semanal
  - Quinzenal
  - Mensal
  - Bimestral
  - Trimestral
  - Quadrimestral
  - Semestral
  - Anual

## 🚀 Como Executar

1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/GestaoObrigacoes.git
```

2. Configure as variáveis de ambiente no arquivo `.env`
```properties
SUPABASE_CONNECTION_STRING=sua-string-de-conexao
```

3. Instale as dependências
```bash
dotnet restore
```

4. Execute as migrations
```bash
dotnet ef database update
```

5. Inicie a aplicação
```bash
dotnet run
```

6. Acesse a documentação Swagger
```
https://localhost:5001/swagger
```

## 📝 Documentação
A documentação completa da API está disponível através do Swagger UI após iniciar a aplicação.

## 👤 Autor
Nome: [Seu Nome]
Email: [Seu Email]

## 📄 Licença
Este projeto está sob a licença MIT.