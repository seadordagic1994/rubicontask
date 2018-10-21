# rubicontask

Hello "Rubicon d.o.o". Here is the finished .NET Developer Assignment.

Before you start testing the application, you will have to set up the connection string in "Web.config" file, so the database can be created on your SQL Server. (SQL Server Express will be ok)

- In the /<connectionStrings/> tag, under "connectionString=" change the "Data Source=" and write your SQL Server instance name. I have used "Integrated Security=true" so I am not using username and password. If you need to enter username and password then at the end of "connectionString=" add "User id=;Password=;" with your autorization data. 
  
- I have used "Postman" to test the API. After calling any request that was in the Assigment (lets say: /api/posts) the database will be created automatically, and will be filled with some inital data. For nonGET requests use JSON(application/json) body format. 

- I was not sure what happens when I add a new post with tags that do not exist in the database so I done the following: 
Tags that don't exist in database but are part of the new blog post I added in the database, and then in a "many to many relation" table I joined the tags to the new blog post. I hope that will be ok. 

- The letter "Z" in at the end of the string (DateTime json format), represents UTC (Coordinated Universal Time). I didn't know should I fill the database with that format or just read the data in that format. I filled the database with local time and read the data in UTC format (so I can have a track of both). 

With respect, 
Sead Ordagić 


