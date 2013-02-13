use [%%DBNAME%%]
exec sp_grantlogin '%%COMPUTERNAME%%\ASPNET'
exec sp_addrolemember 'db_owner', '%%COMPUTERNAME%%\ASPNET'
exec sp_grantdbaccess '%%COMPUTERNAME%%\ASPNET'
