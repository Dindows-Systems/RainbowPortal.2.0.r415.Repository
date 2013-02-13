use [%%DBNAME%%]
exec sp_grantlogin 'NT AUTHORITY\NETWORK SERVICE'
exec sp_addrolemember 'db_owner', 'NT AUTHORITY\NETWORK SERVICE'
exec sp_grantdbaccess 'NT AUTHORITY\NETWORK SERVICE'
