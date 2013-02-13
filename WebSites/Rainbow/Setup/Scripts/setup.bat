@echo off
@echo ------------------------------------------------------------
@echo Welcome to Rainbow DB Installation.
@echo For support please visit http://www.rainbowportal.net
@echo Please report any error on rainbowportal forum.
@echo http://www.rainbowportal.net/ASPNetForums
@echo ------------------------------------------------------------
@echo '
@echo ------------------------------------------------------------
@echo If you use MSDE you need to open the batch with notepad 
@echo and do the change commented out.
@echo '
@echo Proceeding with this script will drop any existing Rainbow database.
@echo Be sure the DB is not in use. If DB is in use try restart SQL.
@echo '
@echo In addition, this script will give the local ASPNET account 
@echo rights for the Rainbow database.
@echo ------------------------------------------------------------
@echo '
@echo Press Ctrl-C to abort or Enter to continue
@pause

@set DBNAME=(local)
@rem Uncomment the following line for MSDE
@rem set DBNAME=(local)\NETSDK

@osql -S %DBNAME% -E -n -i createdb_bat.sql

@echo '
@echo ------------------------------------------------------------
@echo Please check if database created correctly.
@echo Press Ctrl-C to abort or Enter to continue
@echo ------------------------------------------------------------
@echo '
@pause

@echo '
@echo ------------------------------------------------------------
@echo Grant ASPNET account db access
@setlocal
@set ASPNET_ACCOUNT=%COMPUTERNAME%\ASPNET
@set OUTPUT_FILE=aspnetusr_bat.sql

@echo use [Rainbow]>%OUTPUT_FILE%
@echo exec sp_grantlogin '%ASPNET_ACCOUNT%'>>%OUTPUT_FILE%
@echo exec sp_addrolemember 'db_owner', '%ASPNET_ACCOUNT%'>>%OUTPUT_FILE%
@echo exec sp_grantdbaccess '%ASPNET_ACCOUNT%'>>%OUTPUT_FILE%

@osql -S %DBNAME% -d Rainbow -E -n -i %OUTPUT_FILE%

@endlocal
@echo ------------------------------------------------------------
@echo '

@echo '
@echo *******************************************************
@echo                 Windows Server 2003 ONLY
@echo     If you have version 1.0 you will get some errors
@echo                   Please ignore IT
@echo     You can close this window now if you have v 1.0
@echo        Press Ctrl-C to abort or Enter to continue
@echo *******************************************************
@echo '
@pause
@setlocal
@set ASPNET_ACCOUNT=%COMPUTERNAME%\ASPNET
@set OUTPUT_FILE=webserverusr.sql
@echo use [Rainbow]>%OUTPUT_FILE%
@echo exec sp_grantlogin 'NT AUTHORITY\NETWORK SERVICE'>>%OUTPUT_FILE%
@echo exec sp_addrolemember 'db_owner', 'NT AUTHORITY\NETWORK SERVICE'>>%OUTPUT_FILE%
@echo exec sp_grantdbaccess 'NT AUTHORITY\NETWORK SERVICE'>>%OUTPUT_FILE%

@osql -S %DBNAME% -d Rainbow -E -n -i %OUTPUT_FILE%

@endlocal

@echo '
@echo ------------------------------------------------------------
@echo Setup Complete!
@echo ------------------------------------------------------------
@echo '
@pause