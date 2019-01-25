# Flat-File Converter

Simple Flat File converter to .NET DataTable/Json string/Dynamic Object

## Installing

To install this into your project, use Nuget Package installer

Package Manager
```
PM> Install-Package FlatFileConverter -Version 1.0.0
```

.NET CLI
```
dotnet add package FlatFileConverter --version 1.0.0
```


## Uses

Turn your file content into a string and pass it through the converter to return a desired conversion.
Great for using EDI (X12/EDIFACT/CODECO) and translating into DataTable/Json/object.

### Properties

| Property | Type| Description |
| --- | --- | --- |
| fileContent | string | File content into string format from file |
| EOL | string | End Of Line character |
| delimiter | char | Segment separator |
| firstLineIsHeader | bool | T/F to determine if first row/line is the column names |

## Examples

DataTable
```
using FlatFileConverter;

//create parameter variables
string fileContent = File.ReadAllText("C:\Files\TestFile.txt")
string EOL = "~";
char delimiter = '*';
bool firstLineIsHeader = false;

//convert to DataTable
DataTable dt = FF_Conversion.ConvertFlatFileToDataTable(fileContent, EOL, delimiter, firstLineIsHeader);
```

Json
```
using FlatFileConverter;

//create parameter variables
string fileContent = File.ReadAllText("C:\Files\TestFile.txt")
string EOL = "~";
char delimiter = '*';
bool firstLineIsHeader = false;

//convert to Json string
string jsonStr = FF_Conversion.ConvertFlatFileToJson(fileContent, EOL, delimiter, firstLineIsHeader);
```

Object (dynamic)
```
using FlatFileConverter;

//create parameter variables
string fileContent = File.ReadAllText("C:\Files\TestFile.txt")
string EOL = "~";
char delimiter = '*';
bool firstLineIsHeader = false;

//convert to dynamic object
dynamic dynObj = FF_Conversion.convertToObject(fileContent, EOL, delimiter, firstLineIsHeader);
```
