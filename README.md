# SQLiteMergeTool
A tool to merge 2 SQLite databases

First implementation is the table migration tool.
This purpose is move a table from database A to B or vice versa.

The requirement is same tables has a identical structure.

The process is done in 5 steps.

1 - Turn off pragma of destination table; </br> 
2 - Delete all records of destination table; </br>
3 - Read records from origin table; </br>
4 - Move records 1 by 1 to destination table; </br>
5 - Turn on pragma of destination table. </br>

I'm studing another ways to do this faster and how about do a realy a merge, not a transportation of data.
Any contribution are wellcome.
</br>
Using package to visual WPF
Install-Package MahApps.Metro
