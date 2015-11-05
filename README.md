## Express Email

###### Simple way to send email in c# using configurations and fluent sintax.

The goal of this package is send email with fluent sintax code.

#### Basic use :

First configure your app.config or web.config file with simple configuration

##### Add this section to configSections
```cs
<configSections>
    <section name="emailConfiguration"
               type="ExpressEmail.Configuration.ExpressEmailConfiguration, ExpressEmail"
               allowLocation="true"
               allowDefinition="Everywhere"
               requirePermission="false"/>
    ...
  </configSections>
```

##### then this configuration in any location on config file
```cs
<emailConfiguration>   
      <mainEmailConfiguration>
        <smtp host="smtp.example.com" port="8080" useDefaultCredentials="false" enableSsl="true">
          <credentials userName="user@example.com" password="123"/>
        </smtp>
      </mainEmailConfiguration>
</emailConfiguration>
```

##### and just a few lines of code :-)

```cs
ExpressEmail.Factory.Create()
	.From("email@example", "Jonh Doe")
	.To("email@example", "Someone")
	.WithBody("TEST EMAIL")
	.Send();
```

Remember when push your local branches not include sensitive information about your email configuration ;).

### Enjoy!!

made with love to everything :P.
