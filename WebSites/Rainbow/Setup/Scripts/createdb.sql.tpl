USE [Master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'%%DBNAME%%')
BEGIN
	DECLARE @spid smallint
	DECLARE @sql varchar(4000)

	DECLARE crsr CURSOR FAST_FORWARD FOR
		SELECT spid FROM sysprocesses p INNER JOIN sysdatabases d ON d.[name] = '%%DBNAME%%' AND p.dbid = d.dbid

	OPEN crsr
	FETCH NEXT FROM crsr INTO @spid

	WHILE @@FETCH_STATUS != -1
	BEGIN
		SET @sql = 'KILL ' + CAST(@spid AS varchar)
		EXEC(@sql) 
		FETCH NEXT FROM crsr INTO @spid
	END

	CLOSE crsr
	DEALLOCATE crsr

	DROP DATABASE [%%DBNAME%%]
END
GO

CREATE DATABASE [%%DBNAME%%]
GO

ALTER DATABASE [%%DBNAME%%]
COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'%%DBNAME%%', N'autoclose', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'bulkcopy', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'trunc. log', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'torn page detection', N'true'
GO

exec sp_dboption N'%%DBNAME%%', N'read only', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'dbo use', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'single', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'autoshrink', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'ANSI null default', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'recursive triggers', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'ANSI nulls', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'concat null yields null', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'cursor close on commit', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'default to local cursor', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'quoted identifier', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'ANSI warnings', N'false'
GO

exec sp_dboption N'%%DBNAME%%', N'auto create statistics', N'true'
GO

exec sp_dboption N'%%DBNAME%%', N'auto update statistics', N'true'
GO

