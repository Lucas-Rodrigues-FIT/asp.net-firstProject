

##PIZZA API - Learning ASP.NET
A RESTful API example developed using <a href="https://asp.net">asp.net</a>.

#### - get All
localhost:8080/api/pizzas

#### - get by id
localhost:8080/api/pizzas/{id}

#### - post
localhost:8080/api/pizzas
<strong>Body:</strong>
<code>{
	"name" : "The Pizza name",
	"isGlutenFree" : false
}</code>

#### - put
localhost:8080/api/pizzas/{id}
<strong>Body:</strong>
<code>{
	"name" : "New Pizza name",
	"isGlutenFree" : false
}</code>

#### - delete
localhost:8080/api/pizzas/{id}
