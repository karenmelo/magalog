# magalog

#Execução
- Para realizar a execução do projeto é necessário Visual Studio 2022;
- Realizar o clone do projeto;
- Alterar a Connection String no arquivo appsettings.json;
- Executar o comando "update-database" para que as migrations sejam implementadas;
- Após isso executar o projeto;

- #Endpoint: [POST] /v1/Processamento/processar-arquivo
- Para testar o endpoint "processar-arquivo", expanda-o, clique em "try it out" e selecionar um arquivo de dados e clicar em execute;

![image](https://github.com/user-attachments/assets/7a915ffe-c7f1-4aef-b53a-6ea0b141860b)
![image](https://github.com/user-attachments/assets/8b0f577d-bea7-440f-bb82-23d408e5774d)


- #Endpoint: [GET] /v1/Processamento/consultar-dados
- Pode ser testado apenas clicando em "try it out" e depois em "execute" sem preencher os parâmetros de pesquisa que são order_id e startDate e endDate;
- E também pode ser inserido um order_id e clicar em execute para retornar um resultado caso existe a ordem salva no banco caso contrário retornará um 404.
- e pode buscar também por um intervalo de datas.

![image](https://github.com/user-attachments/assets/d25ad8ed-0374-46e7-be19-9c1171580a8c)
![image](https://github.com/user-attachments/assets/e3621be1-53d7-416e-a014-8135e03027bd)

#Abordagens
  - Clean Code
  - SOLID
  - Teste de Unidade
  - DDD

#Tecnologias
 - .Net 8
 - SqlServer
 - Entity Framework Core
 - xUnit
 - Asp.Net Web API
 - Automapper
 - Moq
 - AutoMock
 - Bogus
 - Faker
   
