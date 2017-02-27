NLog.MongoDB20 Target 
=============

### You should check out [nlog mongo](https://github.com/loresoft/NLog.Mongo) as it has kept up with the latest MongoDB driver.



=============
### This target has not kept up with recent changes and is not being actively maintained...


The NLog.MongoDB20 target allows you to use a MongoDB instance as the persistence mechanism for NLog.

You can define which database and server to use, but by default a collection will be created for each "Logger" you use.

This version targets use by MongoDB.Driver 2.0 API

[![nlog_mongodb20 MyGet Build Status](https://www.myget.org/BuildSource/Badge/nlog_mongodb20?identifier=7a7ba553-1963-424c-810f-db9c8bd092cf)](https://www.myget.org/)

Installation
-------------

To install, place the binaries in your application bin and add the following configuration entries to your NLog configuration.

### Using an existing connection string

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
			xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
		<extensions>
			<add assembly="NLog.MongoDB20"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB20" connectionName="ConnectionName" />
		</targets>
		<rules>
			<logger name="*" minLevel="Info" appendTo="Mongo"/>
		</rules>
	</nlog>
	
### Using a new connection string

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
			xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
		<extensions>
			<add assembly="NLog.MongoDB20"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB20" connectionString="mongodb://mongo:db@server:12345/nlog" />
		</targets>
		<rules>
			<logger name="*" minLevel="Info" appendTo="Mongo"/>
		</rules>
	</nlog>	

### Using integrated settings

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
			xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
		<extensions>
			<add assembly="NLog.MongoDB20"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB20" 
				database="nlog"
				host="server" port="12348"            
				username="mongo" password="password"/>
		</targets>
		<rules>
			<logger name="*" minLevel="Info" appendTo="Mongo"/>
		</rules>
	</nlog>
	
### Using custom formatting	

Targets now support custom formatting. If you specify field nodes within the target node, the target will use your configured settings to store the log event.

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
			xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
		<extensions>
			<add assembly="NLog.MongoDB20"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB20" 
				database="nlog"
				host="server" port="12348"            
				username="mongo" password="password">
			  <field name="timestamp" layout="${date}" />
			  <field name="level" layout="${level}" />
			  <field name="message" layout="${message}" />
			  <field name="exception" layout="${exception}" />				
			</target>
		</targets>
		<rules>
			<logger name="*" minLevel="Info" appendTo="Mongo"/>
		</rules>
	</nlog>

### Target Settings:

* Host (Defaults to 'localhost')
* Port (Defaults to 27017)
* Database(Defaults to 'NLog')
* Username
* Password



OR
* ConnectionString (a complete Mongo Url)

OR
* ConnectionName (the name configured in the configuration/connectionString node)

*Also (optional)*
* collectionName - the name of the Mongo collection. If you don't specify this, it uses the logger name.
