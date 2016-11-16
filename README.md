# SQLiteMergeTool
A tool to merge 2 SQLite databases

First implementation is the table migration tool.
This purpose is move a table from database A to B or vice versa.

The requirement is same tables has a identical structure.

The process is done in 4 steps.

1 - Turn off pragma of destination table;
2 - Delete all records of destination table;
3 - Read records from origin table;
4 - Move records 1 by 1 to destination table;
5 - Turn on pragma of destination table.

I'm studing another ways to do this faster and how about do a realy a merge, not a transportation of data.
Any contribution are wellcome.


