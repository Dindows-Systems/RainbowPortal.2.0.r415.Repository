IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[rb_GetTabsFlat]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetTabsFlat]
GO

CREATE PROCEDURE rb_GetTabsFlat
(
        @PortalID int
)
AS
--Create a Temporary table to hold the tabs for this query
CREATE TABLE #TabTree
(
        [TabID] [int],
        [TabName] [nvarchar] (100),
        [ParentTabID] [int],
        [TabOrder] [int],
        [NestLevel] [int],
        [TreeOrder] [varchar] (1000)
)
SET NOCOUNT ON  -- Turn off echo of "... row(s) affected"
DECLARE @LastLevel smallint
SET @LastLevel = 0
-- First, the parent levels
INSERT INTO     #TabTree
SELECT  TabID,
        TabName,
        ParentTabID,
        TabOrder,
        0,
        cast(100000000 + TabOrder as varchar)
FROM    rb_Tabs
WHERE   ParentTabID IS NULL AND PortalID =@PortalID
ORDER BY TabOrder
-- Next, the children levels
WHILE (@@rowcount > 0)
BEGIN
  SET @LastLevel = @LastLevel + 1
  INSERT        #TabTree (TabID, TabName, ParentTabID, TabOrder, NestLevel, TreeOrder) 
                SELECT  rb_Tabs.TabID,
                        Replicate('-', @LastLevel *2) + rb_Tabs.TabName,
                        rb_Tabs.ParentTabID,
                        rb_Tabs.TabOrder,
                        @LastLevel,
                        cast(#TabTree.TreeOrder as varchar) + '.' + cast(100000000 + rb_Tabs.TabOrder as varchar)
                FROM    rb_Tabs join #TabTree on rb_Tabs.ParentTabID= #TabTree.TabID
                WHERE   EXISTS (SELECT 'X' FROM #TabTree WHERE TabID = rb_Tabs.ParentTabID AND NestLevel = @LastLevel - 1)
                 AND PortalID =@PortalID
                ORDER BY #TabTree.TabOrder
END
--Get the Orphans
  INSERT        #TabTree (TabID, TabName, ParentTabID, TabOrder, NestLevel, TreeOrder) 
                SELECT  rb_Tabs.TabID,
                        '(Orphan)' + rb_Tabs.TabName,
                        rb_Tabs.ParentTabID,
                        rb_Tabs.TabOrder,
                        999999999,
                        '999999999'
                FROM    rb_Tabs 
                WHERE   NOT EXISTS (SELECT 'X' FROM #TabTree WHERE TabID = rb_Tabs.TabID)
                         AND PortalID =@PortalID
-- Reorder the tabs by using a 2nd Temp table AND an identity field to keep them straight.
select IDENTITY(int,1,2) as ord , cast(TabID as varchar) as TabID into #tabs
from #TabTree
order by nestlevel, TreeOrder
-- Change the TabOrder in the sirt temp table so that tabs are ordered in sequence
update #TabTree set TabOrder=(select ord from #tabs WHERE cast(#tabs.TabID as int)=#TabTree.TabID) 
-- Return Temporary Table
SELECT TabID, parenttabID, tabname, TabOrder, NestLevel
FROM #TabTree 
order by TreeOrder
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[rb_UpdateTabParent]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_UpdateTabParent]
GO

CREATE PROCEDURE rb_UpdateTabParent
(
    @PortalID        int,
    @TabID           int,
    @ParentTabID     int
)
AS
IF (@ParentTabID = 0) SET @ParentTabID = NULL

UPDATE
    rb_Tabs
SET
    ParentTabID = @ParentTabID
WHERE
    TabID = @TabID
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[rb_GetPagesParentTabID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetPagesParentTabID]
GO

CREATE  PROCEDURE rb_GetPagesParentTabID
(
	@PortalID int,
	@TabID int
)
AS

Select ParentTabID
From rb_Tabs
Where TabID = @TabID And PortalID = @PortalID
GO


IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[rb_GetPageTree]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetPageTree]
GO

CREATE PROCEDURE rb_GetPageTree
(
	@PortalID int
)
AS
--drop table #tree
-- Get the hierarchy
create table #tree (id int, sequence varchar(1000), levelNo int, TabOrder int)



-- insert top level (to get sub tree just insert relevent id here)
insert #tree 
select TabID, (convert(varchar(10),Len(rb_Tabs.TabOrder)) + '.' + convert(varchar(10),rb_Tabs.TabOrder)), 1, TabOrder
from rb_Tabs 
where (PortalId = @PortalID) And (ParentTabID is null)
Order BY rb_Tabs.TabOrder

declare @i int
select @i = 0
-- keep going until no more rows added
while @@rowcount > 0
begin
     select @i = @i + 1
     insert #tree
     -- Get all children of previous level
	
     select rb_Tabs.TabID, 
	#tree.sequence + '.'+ convert(varchar(10),Len(rb_Tabs.TabOrder)) + '.' + convert(varchar(10),rb_Tabs.TabOrder), 
	@i + 1, 
	rb_Tabs.TabOrder
     from rb_Tabs, #tree 
     where #tree.levelNo = @i
	     and rb_Tabs.ParentTabID = #tree.id
     Order BY rb_Tabs.TabOrder
end

-- output with hierarchy formatted
select rb_Tabs.TabID, 
	rb_Tabs.ParentTabID, 
	rb_Tabs.TabOrder, 
	rb_Tabs.TabName, 
	#tree.levelNo , 
	Replicate('-', (#tree.levelNo) * 2) + rb_Tabs.TabName as PageOrder
	--, #tree.sequence
from #tree, rb_Tabs
where #tree.id = rb_Tabs.TabID
order by #tree.sequence--, rb_Tabs.TabOrder

drop table #tree
--drop table #z
GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[rb_GetTabsParent]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_GetTabsParent]
GO

CREATE PROCEDURE rb_GetTabsParent
(
	@PortalID int,
	@TabID int
)
AS
--Create a Temporary table to hold the tabs for this query
CREATE TABLE #TabTree
(
        [TabID] [int],
        [TabName] [nvarchar] (100),
        [ParentTabID] [int],
        [TabOrder] [int],
        [NestLevel] [int],
        [TreeOrder] [varchar] (1000)
)
SET NOCOUNT ON  -- Turn off echo of "... row(s) affected"
DECLARE @LastLevel smallint
SET @LastLevel = 0
-- First, the parent Levels
INSERT INTO     #TabTree
SELECT  TabID,
        TabName,
        ParentTabID,
        TabOrder,
        0,
        cast(100000000 + TabOrder AS varchar)
FROM    rb_Tabs
WHERE   ParentTabID IS NULL AND PortalID =@PortalID
ORDER BY TabOrder
-- Next, the children Levels
WHILE (@@rowcount > 0)
BEGIN
  SET @LastLevel = @LastLevel + 1
  INSERT        #TabTree (TabID, TabName, ParentTabID, TabOrder, NestLevel, TreeOrder) 
                SELECT  rb_Tabs.TabID,
                        Replicate('-', @LastLevel *2) + rb_Tabs.TabName,
                        rb_Tabs.ParentTabID,
                        rb_Tabs.TabOrder,
                        @LastLevel,
                        cast(#TabTree.TreeOrder AS varchar) + '.' + cast(100000000 + rb_Tabs.TabOrder AS varchar)
                FROM    rb_Tabs join #TabTree on rb_Tabs.ParentTabID= #TabTree.TabID
                WHERE   EXISTS (SELECT 'X' FROM #TabTree WHERE TabID = rb_Tabs.ParentTabID AND NestLevel = @LastLevel - 1)
                 AND PortalID =@PortalID
                ORDER BY #TabTree.TabOrder
END
--Get the Orphans
  INSERT        #TabTree (TabID, TabName, ParentTabID, TabOrder, NestLevel, TreeOrder) 
                SELECT  rb_Tabs.TabID,
                        '(Orphan)' + rb_Tabs.TabName,
                        rb_Tabs.ParentTabID,
                        rb_Tabs.TabOrder,
                        999999999,
                        '999999999'
                FROM    rb_Tabs 
                WHERE   NOT EXISTS (SELECT 'X' FROM #TabTree WHERE TabID = rb_Tabs.TabID)
                         AND PortalID =@PortalID
-- Reorder the tabs by using a 2nd Temp table AND an identity field to keep them straight.
select IDENTITY(int,1,2) AS ord , cast(TabID AS varchar) AS TabID into #tabs
from #TabTree
order by NestLevel, TreeOrder
-- Change the TabOrder in the sirt temp table so that tabs are ordered in sequence
update #TabTree set TabOrder=(select ord from #tabs WHERE cast(#tabs.TabID AS int)=#TabTree.TabID) 
-- Return Temporary Table
SELECT TabID, TabName, TreeOrder
FROM #TabTree 
UNION
SELECT 0 TabID, ' ROOT_LEVEL' TabName, '-1' AS TreeOrder
order by TreeOrder
GO


IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[rb_UpdateTab]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [rb_UpdateTab]
GO

--Update Stored PROCEDURE: rb_UpdateTab
--Prevents orphaning a tab or placing tabs in an infinte recursive loop
CREATE PROCEDURE rb_UpdateTab
(
    @PortalID        int,
    @TabID           int,
    @ParentTabID     int,
    @TabOrder        int,
    @TabName         nvarchar(50),
    @AuthorizedRoles nvarchar(256),
    @MobileTabName   nvarchar(50),
    @ShowMobile      bit
)
AS
IF (@ParentTabID = 0) 
    SET @ParentTabID = NULL
IF NOT EXISTS
(
    SELECT 
        * 
    FROM 
        rb_Tabs
    WHERE 
        TabID = @TabID
)
INSERT INTO rb_Tabs (
    PortalID,
    ParentTabID,
    TabOrder,
    TabName,
    AuthorizedRoles,
    MobileTabName,
    ShowMobile
) 
VALUES (
    @PortalID,
    @TabOrder,
    @ParentTabID,
    @TabName,
    @AuthorizedRoles,
    @MobileTabName,
    @ShowMobile
   
)
ELSE
--Updated 26.Dec.2002 Cory Isakson
--Check the Tab recursion so Tab is not placed into an infinate loop when building Tab Tree
BEGIN TRAN
--If the Update breaks the tab from having a path back to the root then do not Update
UPDATE
    rb_Tabs
SET
    ParentTabID = @ParentTabID,
    TabOrder = @TabOrder,
    TabName = @TabName,
    AuthorizedRoles = @AuthorizedRoles,
    MobileTabName = @MobileTabName,
    ShowMobile = @ShowMobile
WHERE
    TabID = @TabID
--Create a Temporary table to hold the tabs
CREATE TABLE #TabTree
(
	[TabID] [int],
	[TabName] [nvarchar] (100),
	[ParentTabID] [int],
	[TabOrder] [int],
	[NestLevel] [int],
	[TreeOrder] [varchar] (1000)
)
SET NOCOUNT ON	-- Turn off echo of "... row(s) affected"
DECLARE @LastLevel smallint
SET @LastLevel = 0
-- First, the parent Levels
INSERT INTO	#TabTree
SELECT 	TabID,
	TabName,
	ParentTabID,
	TabOrder,
	0,
	cast(100000000 + TabOrder AS varchar)
FROM	rb_Tabs
WHERE	ParentTabID IS NULL AND PortalID =@PortalID
ORDER BY TabOrder
-- Next, the children Levels
WHILE (@@rowcount > 0)
BEGIN
  SET @LastLevel = @LastLevel + 1
  INSERT 	#TabTree (TabID, TabName, ParentTabID, TabOrder, NestLevel, TreeOrder) 
		SELECT 	rb_Tabs.TabID,
			Replicate('-', @LastLevel *2) + rb_Tabs.TabName,
			rb_Tabs.ParentTabID,
			rb_Tabs.TabOrder,
			@LastLevel,
			cast(#TabTree.TreeOrder AS varchar(8000)) + '.' + cast(100000000 + rb_Tabs.TabOrder AS varchar)
		FROM	rb_Tabs join #TabTree on rb_Tabs.ParentTabID= #TabTree.TabID
		WHERE	EXISTS (SELECT 'X' FROM #TabTree WHERE TabID = rb_Tabs.ParentTabID AND NestLevel = @LastLevel - 1)
		 AND PortalID =@PortalID
		ORDER BY #TabTree.TabOrder
END
--Check that the Tab is found in the Tree.  If it is not then we abort the Update
IF NOT EXISTS (SELECT TabID from #TabTree WHERE TabID=@TabID)
BEGIN
	ROLLBACK TRAN
	--If we want to modify the TabLayout code then we can throw an error AND catch it.
	RAISERROR('Not allowed to choose that parent.',11,1)
END
ELSE
COMMIT TRAN
GO
