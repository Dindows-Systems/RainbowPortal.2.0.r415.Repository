Rainbow Portal 2.0 Beta 2 Developer Preview 

Beta 2 Changes:

Contributions by ghalib68:

Issue 79:  	 Error when work with contents / add module to page ....
Issue 78:  	 Error when working with user management after installation
Issue 80:  	 Error when working with user management after installation - Part II


Known issue:

fix for Build error: the namespace 'Resources' already contains a definintion for 'Rainbow'
see http://community.rainbowportal.net/forums/thread/14825.aspx
see http://community.rainbowportal.net/forums/thread/14694.aspx


Beta 1 Changes :

Contributions by the MGF Team (Lead: José Arrarte) :


Added ASP.NET Ajax Support
Added ASPNETDB Support for Membership, Roles, Profiles, Web Parts
XHMTL is now on by default
Fixed many open Community issues
Organized Modules into two sections
Added Geographical Provider



Install Instructions : 

    Extract
    Point IIS ( with 2.0 configured) to your directory
    Change web.config connection string value [ in connectionStrings section]
    Browse to site

      [ We will soon automate this process]


Note : This is a Beta Release and should by no means be
       deployed into production as this is a Preview.  If
       the user of this Rainbow Beta 1 deploys in production,
       the Rainbow portal team nor its community will be 
       held liable for issues or downtime.  This software is for developers
       and CMS website owners who would like to see what
       is in store for Rainbow 2.0.

Known Issues:

   Little bugs with ASPNETDB will occur as we slowly integrate this 
new membership layer into Rainbow.  Zen doesnt work with rewrite currently
as code needs to be migrated + bugs fixed.  Please use the classic themes
for now.


Please report new issues : http://code.google.com/p/rainbow/issues/list

Make sure your issue isnt listed before you report.


Released by : Alexey Moudrick
2010-03-08
