Settings.txt (next to application executable)
=============================================
- " | " delimited flatfile.
- First value is boolean for SQLite (0) or MySQL (1).
- Second value is database information
    + For SQLite, it's the path to the MyVideos##.db file.
    + For MySQL, it's "server=<ip>; user id=<db_user>; password=<db pwd>; database=<db_name>; port=<db_port>"
- Third value is the path to the PseudoTV's settings2.xml file.
- Fourth value is the path to the Addons##.db file.

SortColumn.vb
=============
Custom ListView Sorter
1. Acknowledge the flag for ascending/descending for order of value comparison.
2. If both items are numeric, do standard numeric comparison.
3. If both items are a date, do a datetime comparison.
4. Otherwise, do basic string comparison.