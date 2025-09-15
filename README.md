## Aplica��o Console: Respons�vel por consumir os dados da API externa, process�
los e armazen�-los em um banco de dados PostgreSQL. 

##Falta
 passar pelo DTO (Services)
 Testes unit�rios
 Testes Gerais e corre��es de alguns bugs






 ## Documenta��o
 A documenta��o detalhada do projeto est� dispon�vel no arquivo [README.md](../README.md) na raiz do reposit�rio. Este arquivo inclui informa��es sobre a 
 arquitetura do sistema, instru��es de configura��o, detalhes sobre as funcionalidades implementadas e diretrizes para contribuir com o projeto.

 ## Configura��o do Banco de Dados
 A aplica��o utiliza um banco de dados PostgreSQL para armazenar os dados processados. Certifique-se de ter o PostgreSQL instalado e em execu��o.

 As configura��es de conex�o com o banco de dados podem ser ajustadas no arquivo `appsettings.json` localizado na raiz do projeto.

 Exemplo de configura��o no `appsettings.json`:
 ```json
 {
   "ConnectionStrings": {
	 "DefaultConnection": "Host=localhost;Database=meu_banco;Username=meu_usuario;Password=minha_senha"
   }
 }
 ```	
 Certifique-se de substituir `meu_banco`, `meu_usuario` e `minha_senha` pelas suas credenciais reais do PostgreSQL.

 ## Execu��o da Aplica��o
 Para executar a aplica��o, siga os passos abaixo:
 1. Clone o reposit�rio para sua m�quina local.

 2. Navegue at� o diret�rio do projeto.
	

	3. Restaure as depend�ncias do projeto utilizando o comando:
	```bash
	
