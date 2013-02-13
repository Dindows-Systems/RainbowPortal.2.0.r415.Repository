Recompiling without Visual Studio.

In early Rainbow version I provided a batch file

Well mk.bat is a great idea for compiling Rainbow without Visual studio. 
Anyway it is time consuming sync the project each time.

A better solution is to use NANT for rebuild.
Get NANT here: http://nant.sourceforge.net/
and follow installation instruction.

The good news about this NANT build is ALWAYS in sync with visual studio solution.

Starting with 8.3 Nant can build VS project directly.
It has still some problems translation resources so a manual hack is done.

Necessary files are in Rainbow DIR
- Rainbow.build        The Visual studio to Nant build project (using 1.1 framework)
- vs2nant.bat          Simple batch file that calls nant and Rainbow.build
- vs2nantdbg.bat       Simple batch file that calls nant and Rainbow.build with debug flag

- Rainbow10.build      The Visual studio to Nant build project (using 1.0 framework)
- vs2nant-1.0.bat      Simple batch file that calls nant and Rainbow10.build
 
Simply run vs2nat.bat to rebuild project (you need NANT installed)

NOTE: resgen is required for rebuilding project.
Resgen is NOT included in runtime distribution of Net framework.
You need SDK installed or at least resgen in your path to build succesfully.
Be sure you have the tools that match net framework 
(you cannot target 1.1 with SDK 1.0 and only 1.1 runtime)

NOTE: Rebuild is skipped if solution is not changed.
To force a recompile remove the Rainbow.dll file.

Detailed log is saved in vs2nant.log or vs2nant_debug.log
(only last attempt)
