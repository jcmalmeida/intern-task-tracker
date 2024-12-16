# Intern Task Tracker

## Sobre
Intern Task Tracker é uma aplicação para gerenciamento de tarefas e cuja interface funciona via terminal (CLI). Ela foi desenvolvida com .NET 6 e banco de dados MySQL.

## Requisitos
Certifique-se de ter os seguintes softwares instalados:

- [Git](https://git-scm.com/downloads)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/install/)

Opcionalmente, é possível rodar a aplicação sem utilizar Docker e Docker Compose, mas será necessário instalar manualmente o SDK do .NET 6 e o banco de dados MySQL. Também será preciso ajustar manualmente o código para refletir as portas e credenciais que você utilizar ao realizar a instalação destas ferramentas.

- [.NET 6 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0)
- [MySQL](https://dev.mysql.com/downloads/mysql/)


## Como baixar o projeto

1. Clone este repositório para sua máquina local:

```bash
git clone https://github.com/jcmalmeida/intern-task-tracker.git
```

2. Navegue até o diretório do projeto:

```bash
cd intern-task-tracker
```

3. Certifique-se de que o Docker e o Docker Compose estão em execução.

4. No diretório do projeto, execute o seguinte comando:

```bash
docker-compose up --build -d
```

5. Após o carregamento dos contêineres, para acessar o terminal (CLI) para interação com o programa, execute o seguinte comando:

```bash
docker-compose run todoapicli
```

6. Para encerrar o programa no terminal (CLI), selecione a opção 6 e, em seguida, digite:

```bash
docker-compose down
```

## Estrutura do Projeto

- **/TodoApi**: Contém o código-fonte principal da aplicação.
- **/TodoApiCLI**: Contém o código-fonte da interface via terminal (CLI).
- **/TodoApi.Tests**: Contém os testes unitários da aplicação.
- **docker-compose.yml**: Configuração para executar os serviços usando Docker.
- **Dockerfile**: Arquivo de configuração para construir a imagem do contêiner da aplicação (presente em **/TodoApi** e **/TodoApiCLI**).

## Solução de Problemas

1. **Portas já em uso**:
   - Certifique-se de que as portas `5258` e `3306` não estão em uso por outros serviços.

2. **Erro de conexão com o banco de dados**:
   - Verifique o log do contêiner do banco de dados:
     ```bash
     docker logs mysql_container
     ```

3. **Recriar os contêineres**:
   - Para limpar e reconstruir:
     ```bash
     docker-compose down
     docker-compose up --build -d
     ```
