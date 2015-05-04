NLog.MongoDB Target
=============
The NLog.MongoDB target allows you to use a MongoDB instance as the persistence mechanism for NLog.

You can define which database and server to use, but by default a collection will be created for each "Logger" you use.

Installation
-------------

To install, place the binaries in your application bin and add the following configuration entries to your NLog configuration.

### Using an existing connection string

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
			xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
		<extensions>
			<add assembly="NLog.MongoDB"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB" connectionName="ConnectionName" />
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
			<add assembly="NLog.MongoDB"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB" connectionString="mongodb://mongo:db@server:12345/nlog" />
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
			<add assembly="NLog.MongoDB"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB" 
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
			<add assembly="NLog.MongoDB"/>
		</extensions>

		<targets>
			<target name="Mongo" type="MongoDB" 
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
