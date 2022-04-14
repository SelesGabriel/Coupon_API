1 - Banco utilizado: MySQL (Banco gratuito online do herokuapp) obs* Pode impactar na performance devido às limitações.

2 - API está online também no link: https://challangecouponapi.azurewebsites.net/

3 - Adicionei autenticação JWT, e para gerar o token, basta acessar https://challangecouponapi.azurewebsites.net/login (POST) e no body: 
{
  "userName": "admin",
  "password": "admin"
}
o token expira depois de 1 dia após a geração.

4 - Para levantar a aplicação, pode ser utilizando o Visual Studio ou Visual Code. No Visual Studio, basta rodar que já vai direto para a interface do Swagger.
No Visual Code, basta dar o comando dotnet run e acessar a interface do Swagger no link abaixo.
Acredito que em outras IDEs funcionam também. 
Swagger: http://localhost:5245/swagger/index.html

5 - Algumas regras a se considerar: Para poder aplicar o cupom de desconto, só é possível em itens que já estiver nos favoritos. 
http://localhost:5245/doFavorite?idItem=MLB1858695880&idUser=1 (PUT). Caso o item não esteja nos favoritos, não será possível aplicar o cupom de desconto.

6 - No item 5, tem a url para favoritar, para desfavoritar, seria quase da mesma forma doFavorite?idItem=MLB1858695880&idUser=1 (PUT).

7 - Para obter os 5 itens mais favoritado entre os usuários cadastrados, basta acessar o endpoint /coupon/stats (GET)

8 - Para obter um item: https://challangecouponapi.azurewebsites.net/item?idItem=MLB1858695880 (GET)

9 - Para obter os usuários e os itens favoritos de cada um deles, basta acessar o end point /user (GET)

9 - Para obter um único usuário e itens favoritos, acessar /user/{id} (GET)

10 - Para cadastrar um novo usuário, o end point /user (POST)
{
  "name": "string",
  "lastName": "string",
  "registrationDate": "2022-04-14T02:52:14.264Z",
  "email": "string"
}
