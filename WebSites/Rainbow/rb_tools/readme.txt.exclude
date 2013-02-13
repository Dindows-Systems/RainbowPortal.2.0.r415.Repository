Rainbow 1.5 - Quickstart
Last changed on mar 01, 2005 by Emmanuele De Andreis

Latest online version of this document at:
http://support.rainbowportal.net/confluence/x/-AM

This is a short quickstart.
If you need more informations or you need to troubleshoot your installation have a look at the full document online:
http://support.rainbowportal.net/confluence/x/xQM

Prerequisites
Net framework and a SQL 2000 or compatible database (like MSDE 2000)

SQL 2000 database
SQL Server or Microsoft Desktop Engine (MSDE) version 2000 or later.
MSDE is also included in the .NET Framework full SDK version. With SQL Server, With Office Developer, and on MSDN.
You may also download the Web Matrix version.

NET Framework and ASP NET

ASP.NET Version 1.1
ASP.NET is available as free download and runs on the following platforms:
Microsoft Windows 2000 Professional and Server (latest SP recommended)
Microsoft Windows XP Professional
Microsoft Windows Server 2003

NET Framework Redistributable
This download includes ASP.NET and the .NET Framework, and provides everything necessary to build and deploy ASP.NET applications.
This is the recommended installation package for production Web servers.

NET Framework Software Development Kit
This download includes everything in the Redistributable, plus documentation,
Quickstart samples, MSDE (a lightweight SQL database engine), and command-line tools and compilers.
Note: The SDK is needed for recompiling Rainbow using nant (without visual studio)

Download the NET Framework
http://msdn.microsoft.com/netframework/downloads/framework1_1/

Web setup
Setting up:
Web instance with multiple sites:

    * Copy all source files on a dir called Rainbow on Web Server.
    * Create application for that dir (IIS admin -> Rainbow folder -> Properties)
    * Check the web.config for specific settings about your configuration.


Web instance with Rainbow only (Also when not using multiple portals):

    * Create new web site from IIS Internet Manager
    * Copy all source files on main dir of the web.
    * Check the web.config for specific settings about your configuration.

Database
For new installations:

    * Go to setup/scripts dir and run setup.wsh (recommended) or setup.bat
    * Go to the site for complete the installation
    * admin@rainbowportal.net/admin password is the predefined password, you can change later


or

    * Go on enterprise manger
    * Create a new database named Rainbow
    * Grant access as dbowner to ASPNET account
    * Go to the site for complete the installation
    * admin@rainbowportal.net/admin password is the predefined password, you can change later

Upgrade note

    * Update code and run portal, you will be prompted for confirm db update process.


WARNING: We have made a lot of changes on db and procedures.
We have made the best to ensure any database is upgradable
to latest version with no data loss.
Anyway: BACKUP your data before any upgrade.
Custom database may require manual intervention.

Before upgrading please review db config settings:
<!-- DB SETTINGS -->

If you run in troubles please post on forums:
http://forums.rainbowportal.net