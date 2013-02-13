One File Module Example - Simple (Source is VB)
Credits: Jakob Hansen, hansen3000@hotmail.com


Another Rainbow desktop module - more to download on http://www.rainbowportal.net


Note: Before you install the "OneFileModuleKit" must have been installed!

INSTALL
1. Copy file SimpleVB.ascx to folder Rainbow\DesktopModules
   Note: Do not add SimpleVB.ascx to the project - no compiling needed!
2. Run ApplyDBPatch.bat (or execute DBPatch.sql in SQL Query Analyzer)
3. Log on as Admin and add the "Simple VB (OneFileModule example 3)" module to a tabpage 
4. Edit module settings: 
     enter "FirstName=Elvis;LastName=Presly;" in field "Settings string"
5. Use it! ;o) 



HISTORY
Ver. 1.0 - 20. dec 2002 - First realase by Jakob Hansen
Ver. 1.1 -  9. sep 2003 - Updated DBPatch.sql
Ver. 1.2 - 29. jan 2005 - Updated DBPatch.sql for all examples


Issues and Known problems:
- Tested with Rainbow version 1.5.0.1791i


More demo:
1. Open file Simple.ascx and change:
     InitSettings(SettingsType.Str) --->> InitSettings(SettingsType.StrAndXml)
2. Copy file SettingsSimpleVB.xml to folder Rainbow\_Rainbow
3. Edit module settings: 
     enter "SettingsSimpleVB.xml" in field "XML settings file"
4. Edit module settings: 
     enter "FirstName=Elvis;LastName=Costello;" in field "Settings string"
5. Note that "Settings string" overrules the settings in the XML file
