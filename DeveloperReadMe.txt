Rainbow Portal 2.0 DeveloperReadMe.txt

The Rainbow Portal 2.0 team welcomes new developers!

To make it as simple as possible to get up and running with Rainbow, we've
provideded the following quick start instructions.

JOIN THE RAINBOW DEVELOPER TEAM

1. Contact Eric Ramseur at ramseur@gmail.com to request to join the team.
2. Eric will give you access to the http://community.rainbowportal.net/
   Rainbow Community. Eric will give you instructions on creating your 
   statement of work.  You will primarily use the Developer Forum at
   http://community.rainbowportal.net/forums/31/ShowForum.aspx to collaborate
   with the other developers.  You will also be able to maintain your own
   blog about the work you are doing on this site.
3. Join the Rainbow Portal Google Group at
   http://groups.google.com/group/rainbowportal to receive other 
   developer communications.
4. Eric will ask Alexey Moudrick at moudrick@gmail.com to grant you access to 
   the Rainbow Portal project at Novell Forge SVN site at
   http://code.google.com/p/rainbow/.  You will be able
   to browse the repository at 
   http://rainbow.googlecode.com/svn/ and use Subversion
   to access the repository at
   http://rainbow.googlecode.com/svn/trunk/.  Alexey will create a sandbox for
   you and provide additional instructions on best practices.
5. Sign up for a google account to use Google Code.
6. Download and install the Tortoise Subversion GUI at 
   http://tortoisesvn.tigris.org/.
7. Subversion documentation is available at http://svnbook.red-bean.com/.



Note that you can use the Tortoise GUI to do the above also. Please contact
Alexey Moudrick at moudrick@gmail.com if you have questions about using 
Google Code, Subversion, or Tortoise to branch from devint and create your
local copy.  Alexey can will also help merge changes back into devint when
they are ready to be tested with other developer's code.


INSTALLING RAINBOW 2.0 FOR DEVELOPMENT

1. Create a MS SQLSERVER 2000 or 2005 database for your Rainbow portal. You
   might name it RainbowDevint for example.
2. The WebSites\Rainbow\web.config.standard can be renamed to web.config.
3. Update the web.config file and ensure that you have valid connection strings
   to the database you just created.  Remember to specify connection strings for
   your rainbow DB and ASPNETDB.  Please note ASPNETDB can be inside/outside the
   rainbow DB
4. Make sure you have VS 2005 installed with the Web Development Projects
   update from MSDN at http://msdn2.microsoft.com/en-us/asp.net/aa336619.aspx
5. Double-click on the Rainbow.sln file to open the solution in Visual Studio
   2005.
6. Click on OK if prompted about ASP.NET 2.0.
7. If necessary, delete the Projects\Rainbow.Tests\App.config file and replace
   it by renaming the App.config.standard to App.config.
8. Build the projects, the website, then the entire solution.
9. Be sure to set the privileges for ASP.NET user to access the 
   WebSites\Rainbow folder.  Certain folders require write access, 
   such as the WebSites\Rainbow\rb_logs folder.
10. Open Default.aspx and press ctrl-f5 to run the project.
11. To debug, you can click Debug > Attach to process... and select the
    aspnet_wp.exe process.
    

COORDINATING WITH OTHER DEVELOPERS

Please contact Eric Ramseur at ramseur@gmail.com to find out what the
priorities are and coordinate with other developers.  You can tell from forum
and blog activity at http://community.rainbowportal.net who is currently 
active.

Please contact Alexey Moudrick at moudrick@gmail.com to coordinate moving
code into devint.

Thanks again for joining the Rainbow Portal 2.0 team!






   



   
