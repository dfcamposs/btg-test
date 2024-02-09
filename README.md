## API Gestão de Limites de PIX

Para atender aos requisitos de negócio para gerenciamento de limites pelo analista de fraudes, foi disponibilizado 4 endpoints:

`POST /limit-manager` - cria novo limite para uma conta específica.  
`GET /limit-manager` - retorna o limite cadastrado de uma conta específica.  
`PUT /limit-manager` - atualiza informações de limite de uma conta específica.  
`DELETE /limit-manager` - deleta informações de limite cadastrado de uma conta específica.  

Foi disponibilizado também outros endpoints para fazer a validação, compensação e reestabelecimento (reset) do limite disponível de PIX, passando o valor da transação desejada como parâmetro:

`POST /validate-limit` - valida se tem limite disponível para uma transação de PIX de uma conta específica.  
`PATCH /limit-manager/update-limit` - atualiza valor de limite disponível para transações de PIX de uma conta específica.  
`PATCH /limit-manager/reset-limit` - reinicia limite disponível baseado no limite cadastrado de uma conta específica. (não está especificado claramente nas regras de negócio, mas creio que seja importante em algum momento - por exemplo: o limite é reiniciado a cada inicio do dia/semana).  

Todos os endpoints acima também foi incluído camada de autenticação JWT, que deverá ser informado no header de cada requisição.  
As chaves para consulta e alteração dos limites são os dados de `agencia (branch)` e `numero da conta (accountNumber)`, que deverá ser informados ou no `header` da requisição ou no `body`, dependendo do endpoint. (Estes campos foram escolhidos como chave para facilitar o acesso dessas informações de limite em integrações com outros sistemas, módulos, etc).

Para acessar detalhes de como utilizar estas APIs, executar os seguintes passos:

1. Acessar pasta do projeto `/BtgTest/src/BtgTest`  
2. Executar comando no terminal `dotnet run`  
3. Acessar o endpoint local no navegador `http://localhost:5000/swagger/index.html`  

Para realizar o deploy da API:

1. Instalar dependências do template utilizado usando o comando `dotnet tool install -g Amazon.Lambda.Tools`  
   1.1. caso já esteja instalado, verificar se há uma atualização mais recente disponível executando o comando `dotnet tool update -g Amazon.Lambda.Tools`  
2. Executar comando `dotnet lambda deploy-serverless`  

Recursos de AWS utilizados:  

Lambda - executar os endpoints da API  
API Gateway - expor as rotas para os endpoints da API  
Dynamo DB - persistir dados para a gestão de limites  
S3 - usado no deploy da lambda via Cloud Formation  


TODOs:  
Tarefas importantes que ainda precisam ser feitas, mas que devido ao tempo ainda não foi possível:
  
[] Testes Unitários  
[] Testes Integrados  
[] Integração do API Gatewaty com o Cognito  
[] Otimização CI/CD  
[] Melhoria em mensagens de retorno dos endpoints da API  
[] Integração com Frontend
