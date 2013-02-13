-- see issue #79
-- http://code.google.com/p/rainbow/issues/detail?id=79

ALTER PROCEDURE [dbo].[rb_ContentMgr_GetModuleInstances]
(
    @ItemID     int,
    @PortalID   int
)
AS
    SELECT
        ModuleID,(PageName + '\' + ModuleTitle) AS TabModule
    FROM
        rb_ContentManager,rb_ModuleDefinitions,rb_Modules,rb_Pages
    WHERE
        rb_ContentManager.ItemID = @ItemID
            AND
        rb_ContentManager.SourceGeneralModDefID  = rb_ModuleDefinitions.GeneralModDefID
            AND
        rb_ModuleDefinitions.ModuleDefID = rb_Modules.ModuleDefID
            AND
        rb_ModuleDefinitions.PortalID = @PortalID
            AND
        rb_Modules.TabID = rb_Pages.PageID

    ORDER BY
        rb_Pages.PageName,rb_Modules.ModuleTitle
GO


ALTER PROCEDURE [dbo].[rb_ContentMgr_GetModuleInstancesExc]
(
    @ItemID     int,
    @ExcludeItem  int,
    @PortalID   int
)
AS
    SELECT
        ModuleID,
        (PageName + '\' + ModuleTitle) AS PageModule
    FROM
        rb_ContentManager,rb_ModuleDefinitions,rb_Modules,rb_Pages
    WHERE
        rb_ContentManager.ItemID = @ItemID
            AND
        rb_Modules.ModuleID != @ExcludeItem
            AND
        rb_ContentManager.DestGeneralModDefID  = rb_ModuleDefinitions.GeneralModDefID
            AND
        rb_ModuleDefinitions.ModuleDefID = rb_Modules.ModuleDefID
            AND
        rb_ModuleDefinitions.PortalID = @PortalID
            AND
        rb_Modules.TabID = rb_Pages.PageID
    ORDER BY
        rb_Pages.PageName,rb_Modules.ModuleTitle
GO

 