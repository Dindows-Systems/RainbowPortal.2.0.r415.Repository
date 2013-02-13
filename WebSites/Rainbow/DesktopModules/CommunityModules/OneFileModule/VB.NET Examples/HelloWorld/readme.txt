One File Module Example - HelloWorld (Source is VB)
Credits: Jakob Hansen, hansen3000@hotmail.com


Another Rainbow desktop module - more to download on http://www.rainbowportal.net


Note: Before you install the "OneFileModuleKit" must have been installed!

INSTALL
1. Copy file HelloWorld.ascx to folder Rainbow\DesktopModules
   Note: Do not add HelloWorld.ascx to the project - no compiling needed!
2. Run ApplyDBPatch.bat (or execute DBPatch.sql in SQL Query Analyzer)
   If you are on a production site use module "Admin - Database Tool"
   and exeute all sql in DBPatch.sql starting after the GO command.
3. Log on as Admin and add the "HelloWorld (OneFileModule example)" module to a page 
4. Edit module settings: 
     enter "FirstName=Elvis;LastName=Presly;" in field "Settings string"
5. Use it! ;o) 



HISTORY
Ver. 1.0 - 9. sep 2005 - First realase by Jakob Hansen for the Rainbow Gathering


Issues and Known problems:
- Tested with Rainbow version 1.5.0.1876a - 07/09/2005


More demo:
1. Open file HelloWorld.ascx and change:
     InitSettings(SettingsType.Str) --->> InitSettings(SettingsType.StrAndXml)
2. Copy file SettingsHelloWorld.xml to folder Rainbow\_Rainbow
3. Edit module settings: 
     enter "SettingsHelloWorld.xml" in field "XML settings file" 
     and delete all text in field "Settings string"
Note: "Settings string" overrules the settings in the XML file
