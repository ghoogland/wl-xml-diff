# wl-xml-diff
Tool that creates a Bricklink Wanted list, based on a wanted list minus another wanted list. Purpose : to reuse parts you already have, and thus only order parts you don't have yet.

Usage:

```sh
Welcome to WL.XML Diff.
You will be asked to enter/paste two WL.XML filenames.
One with the parts you WANT, One with the parts you already HAVE.
The result will be a new WL.XML of the parts you are missing.
For instance, if you want to build Brickvaults MF, reusing parts of the UCS MF, you enter Brickvaults XML first, followed by the UCS MF XML

Enter Filename 1 (Parts you want):"D:\Test\WLXMLDiff\01. MF (without Docking Rings).xml"
Enter Filename 2 (Parts you have):"D:\Test\WLXMLDiff\UCS 75192 Partslist.xml"
11796 parts wanted in total, in file 1
3723 parts can be reused from file 2
8073 parts wanted after reuse
```
