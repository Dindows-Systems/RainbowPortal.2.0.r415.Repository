Rainbow 2006
New features list

Setup
Improved setup by Chris Farrell
Now a welcome page guide helps the first install and warn about common setup errors or misconfiguration

by Manu
Now SQL2005 should work (at least until beta2)

Look and feel
Improved Zen theme engine by Jeremy Esland
Added new demo themes (screenshot)
Better Caching handling (no need of Cache Manager anymore)
Now each module may have its own css and current theme may override it

ZEN laytous options:
There are two properties on the ZenNavigation control 
"UsePathTraceInUrl" - true by default
"UsePageNameInUrl" - also true by default
set both to false to use standard friendly pagename instead

ZEN known issue:
Using ZEN menu as in the default skin zenexample on a very large site (more than 1000 pages)
in IE can have a big load on CPU.
It is not recommended to use ZEN menu on large sites.
It is an IE issue, Firefox works correctly.

Security
by Danijel Kecman
Hash+Salt for password. 
Password not plaint text in db anymore. 
Implemented Reset Password instead of Send Password (activate it on web.config)
Existing password in db are upgraded to hash+salt during first logon.

Administration

Recycler module by John Bowen
Now deleted modules are not really deleted and can be recovered.
Use Recycler module to see deleted modules and to restore them to any Page

File Manager by Rob Siera
Access the whole portal dir with one module
Icon available for any file type

Management
Improved error pages and rewritten most code on base settings by Jeremy Esland

Workflow
New merge engine and some bugs fixed by Hongwei Shen

Modules
New module Filemanager by Rob Siera
Improved Articles module (now supports workflow and images in abstract) by Manu

Extra
Included ELB www.EasyListBox.com special edition
Upgraded FCK to FCKEditor V2

Bug Fixes
A lot of old and new bug fixed

Breaking changes!

Big rename from Tab to Page in code.
Old Tab methods still availabe for backward compatibility.
However:

Change in allDesktopLayouts that refers to TabStripDetails class to PageStripDetails

example:
search for (usually used on DesktopPortalBanner.aspx)
<a href="<%#Rainbow.HttpUrlBuilder.BuildUrl(((Rainbow.Configuration.TabStripDetails) Container.DataItem).TabID)%>"><%# ((Rainbow.Configuration.TabStripDetails) Container.DataItem).TabName %></a>
and replace with:
<a href="<%#Rainbow.HttpUrlBuilder.BuildUrl(((Rainbow.Configuration.PageStripDetails) Container.DataItem).PageID)%>"><%# ((Rainbow.Configuration.PageStripDetails) Container.DataItem).PageName %></a>

or you get an error like this:
System.Web.HttpUnhandledException: Generata eccezione di tipo System.Web.HttpUnhandledException. ---> System.InvalidCastException: Cast specificato non valido.
   at ASP.DesktopPortalBanner_ascx.__DataBind__control10(Object sender, EventArgs e) in C:\Dev\Rainbow\SVN\DEV\Design\DesktopLayouts\OldTabsMenuHWLeft\DesktopPortalBanner.ascx:line 79


Module List
Admin - Add Module Control
Admin - Add Tab
Admin - BlackList
Admin - Cache Viewer
Admin - Content Manager
Admin - Database Table Edit
Admin - Database Tool
Admin - EventLogs
Admin - FileManager
Admin - MagicUrls
Admin - Manage Portals (AdminAll)
Admin - Manage Users
Admin - Module Definitions
Admin - Module Types (AdminAll)
Admin - Monitoring
Admin - Newsletter
Admin - Pages
Admin - Recycler
Admin - Role Assignment
Admin - Roles
Admin - Security Check
Admin - Site Settings
Amazon from KonoTree.com v2.0
Announcements
Articles
Articles Inline
Blog
BreadCrumbs
ComponentModule
Contacts
Daily Dilbert
Discussion
Documents
Enhanced Html
Enhanced Links
Events
FAQs
File Directory Tree
Flash Module
FriendlyName
HTML Document
IframeModule
Image
LanguageSwitcher
LDAP User Profile
Links
MapQuest
MileStones
PageKeyPhrase
Pictures
Quiz
Quote
Rainbow Version
Search Site - via DB (beta, slow/unreliable use Google module instead)
Search Site (via Google)
Send Thoughts
ServiceItemList
Shortcut
ShortcutAll
SignIn
SignIn LDAP
SignInCool
SignInLink
Simple Menu
Sitemap
Survey
Tasks
UserDefinedTable
Weather German
Weather US
WebPartModule
Who's Logged On?
XML/XSL
XML/XSL (MultiLanguage)
XmlFeed
