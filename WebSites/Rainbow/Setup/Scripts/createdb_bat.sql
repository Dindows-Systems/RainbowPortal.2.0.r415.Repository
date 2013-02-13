USE [Master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Rainbow')
BEGIN
	DECLARE @spid smallint
	DECLARE @sql varchar(4000)

	DECLARE crsr CURSOR FAST_FORWARD FOR
		SELECT spid FROM sysprocesses p INNER JOIN sysdatabases d ON d.[name] = 'Rainbow' AND p.dbid = d.dbid

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

	DROP DATABASE [Rainbow]
END
GO

CREATE DATABASE [Rainbow]
GO

ALTER DATABASE [Rainbow]
COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'Rainbow', N'autoclose', N'false'
GO

exec sp_dboption N'Rainbow', N'bulkcopy', N'false'
GO

exec sp_dboption N'Rainbow', N'trunc. log', N'false'
GO

exec sp_dboption N'Rainbow', N'torn page detection', N'true'
GO

exec sp_dboption N'Rainbow', N'read only', N'false'
GO

exec sp_dboption N'Rainbow', N'dbo use', N'false'
GO

exec sp_dboption N'Rainbow', N'single', N'false'
GO

exec sp_dboption N'Rainbow', N'autoshrink', N'false'
GO

exec sp_dboption N'Rainbow', N'ANSI null default', N'false'
GO

exec sp_dboption N'Rainbow', N'recursive triggers', N'false'
GO

exec sp_dboption N'Rainbow', N'ANSI nulls', N'false'
GO

exec sp_dboption N'Rainbow', N'concat null yields null', N'false'
GO

exec sp_dboption N'Rainbow', N'cursor close on commit', N'false'
GO

exec sp_dboption N'Rainbow', N'default to local cursor', N'false'
GO

exec sp_dboption N'Rainbow', N'quoted identifier', N'false'
GO

exec sp_dboption N'Rainbow', N'ANSI warnings', N'false'
GO

exec sp_dboption N'Rainbow', N'auto create statistics', N'true'
GO

exec sp_dboption N'Rainbow', N'auto update statistics', N'true'
GO

