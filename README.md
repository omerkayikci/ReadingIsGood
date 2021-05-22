# ReadingIsGood
This project was created in accordance with its layered architecture. This structure, created with a mediator and repository pattern, meets e-commerce stock management in certain dimensions.
- [x] **The "Ideas" part is important!**

## Installation & Run ##

- Clone the repository
```console
https://github.com/omerkayikci/ReadingIsGood
```

- Go to solution directory
```console
cd ReadingIsGood
```

- Build a docker container
```console
docker-compose up -d
```
<br/>

When the project first starts, the products to be tested, a customer and a related user are created. After the token is received with the user information, order transactions can be executed.
The project will be created in 3 different structures as mongoDB, mongo express and api projects. These;
- Api Project
```console
http://localhost:5000/ 
```
- MongoDB(it is accessed from within the project as "mongo:27017".)
```console
http://localhost:27017/ 
```
- MongoExpress 
```console
http://localhost:8081/ 
```

## Design ##
The project consists of api, application, common, core and MongoDB layers.
-  API Layer contains structures to which endpoints are routed. Importantly, it includes an intermediate layer that allows errors to be handled with ExeptionHandleMiddleware.
-  Application layer is where "Mediator Pattern" is implemented. A structure that separates and manages Command and Query operations in accordance with CQRS has been planned and implemented.
-  Common Layer contains enum, helper method etc. used throughout the project. 
-  Core Layer contains the structures in which the process of the project takes place. These are Validation, Repository, Abstract Service etc.
-  MongoDB Layer contains a generic structure built on mongodb builder. This structure allows you to create a collection for the specified entity, as well as to perform operations on it.

## Ideas ##
1. Since replicaSet is required for MongoDB Trancastion, it can be used by making the necessary definitions in the dockercompose ymal file. (suggested action could not be taken.)
2. With the transaction structure, stock reduction in parallel with the order process can be done at the same time and in case of error, the entire transaction can be roolbacked and inventory tracking can be monitored instantly.
3. Additionally, you can process orders sequentially using outbox pattern and RabbitMQ. During the order processing phase, you can update the stock status in parallel with the transaction structure and take necessary actions in case of no acknowledge.
4. As a test, I think it is more beneficial to write a Behavior Driven test with "SpecFlow". There was no situation related to "testing" in the requested project text.
