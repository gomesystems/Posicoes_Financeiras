## Aplicação Console: Responsável por consumir os dados da API externa, processá
los e armazená-los em um banco de dados PostgreSQL. 

## Falta
 passar pelo DTO (Services)
 Testes unitários
 Testes Gerais e correções de alguns bugs






 ## Documentação
 A documentação detalhada do projeto está disponível no arquivo [README.md](../README.md) na raiz do repositório. Este arquivo inclui informações sobre a 
 arquitetura do sistema, instruções de configuração, detalhes sobre as funcionalidades implementadas e diretrizes para contribuir com o projeto.

 ## Configuração do Banco de Dados
 A aplicação utiliza um banco de dados PostgreSQL para armazenar os dados processados. Certifique-se de ter o PostgreSQL instalado e em execução.

 As configurações de conexão com o banco de dados podem ser ajustadas no arquivo `appsettings.json` localizado na raiz do projeto.

 Exemplo de configuração no `appsettings.json`:
 ```json
 {
   "ConnectionStrings": {
	 "DefaultConnection": "Host=localhost;Database=meu_banco;Username=meu_usuario;Password=minha_senha"
   }
 }
 ```	
 Certifique-se de substituir `meu_banco`, `meu_usuario` e `minha_senha` pelas suas credenciais reais do PostgreSQL.

 ## Execução da Aplicação
 Para executar a aplicação, siga os passos abaixo:
 1. Clone o repositório para sua máquina local.

 2. Navegue até o diretório do projeto.
	

	3. Restaure as dependências do projeto utilizando o comando:
	```bash
	
