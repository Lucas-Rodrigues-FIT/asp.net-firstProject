

# PIZZA API - Learning ASP.NET
A RESTful API example developed using <a href="https://asp.net">asp.net</a>.

### - get All
localhost:8080/api/pizzas

### - get by id
localhost:8080/api/pizzas/{id}

### - post
localhost:8080/api/pizzas <br>
<strong>JSON(body):</strong> <br>
<code>{
	"name" : "The Pizza name",
	"isGlutenFree" : false
}</code>

### - put
localhost:8080/api/pizzas/{id} <br>
<strong>JSON(body):</strong> <br>
<code>{
	"name" : "New Pizza name",
	"isGlutenFree" : false
}</code>

### - delete
localhost:8080/api/pizzas/{id}
