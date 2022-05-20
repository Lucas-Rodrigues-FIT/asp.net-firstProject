

# Learning ASP.NET
A WEB API example developed using <a href="https://asp.net">asp.net</a>. It's use the concept of Routing, Model Binding, Model Validation, Filter Pipeline and Entity Framework.

## :pizza: Pizza API 

### - get All
localhost:8080/api/pizzas

### - get by id
localhost:8080/api/pizzas/{id}

### - post
localhost:8080/api/pizzas <br>
<strong>JSON(body):</strong> <br>
<code>{
	"name" : "The Pizza name",
	"isGlutenFree" : false,
	"price" : 29.99
}</code>

### - put
localhost:8080/api/pizzas/{id} <br>
<strong>JSON(body):</strong> <br>
<code>{
	"name" : "New Pizza name",
	"isGlutenFree" : false,
	"price" : 29.99
}</code>

### - delete
localhost:8080/api/pizzas/{id}
