## Introduction

<br>

- This document is C# coding conventions for effective and maintainable software code.

<br>

### Terminology

<br>

- In this section, define `Naming rules` for following section.

|Naming rule|Example|Description|
|---|---|---|
|PascalCase|int PascalCase|First letter of every word is in capital|
|CamelCase|int camelCase|First letter of word is in small and after that each word starts with capital|

<br>

## Naming rules

<br>

### Directory, file

<br>

- Directory and file names are `PascalCase`
    - ex
        - Directory name : `FolderName`
        - File name : `CodeFile.cs`
- Directory name should be general than file name for well-organizing code files.
- If files are defined as `partial`, using underscore is allowed for concrete presentation.
    - ex
        - File name 1 : `Network_Connection.cs`
        - File name 2 : `Network_Messaging.cs`
    - In this case, recommended structure is organized files by folder.
        - ex
            ```
            Network (Directory name)
            |- Connection.cs
            |- Messaging.cs
            ```

<br>

### Code

<br>

- Types and naming rules are following table.

|Type|Naming rules|Description|
|---|---|---|
|Assembly|PascalCase|Consider judiciously name of assembly|
|Namespace|PascalCase|1. Do not use name as single character<br>2. Do not use the same name for namespace and type|
|Class|PascalCase||
|Struct|PascalCase||
|Interface|`I`PascalCase|Start prefix with `I`<br>`interface IMyInterface`|
|Record|PascalCase||
|Enum|PascalCase||
|Tuple|PascalCase|`(bool IsSuccess, int Value)`|
|Method|PascalCase||
|Property|Same naming conventions as following `Access modifiers` table||
|Constant|Same naming conventions as following `Access modifiers` table||
|Field|Same naming conventions as following `Access modifiers` table||
|Parameter|camelCase||
|Local variable|camelCase||
|Collection class|PascalCase`Collection`|Add `Collection` suffix|
|Delegate class|PascalCase`Delegate`|Add `Delegate` suffix|
|Event argument class|PascalCase`EventArgs`|Add `EventArgs` suffix|
|Event handler|PascalCase`EventHandler`|Add `EventHandler` suffix|
|Exception class|PascalCase`Exception`|Add `Exception` suffix|
|Attribute class|PascalCase`Attribute`|Add `Attribute` suffix|

<br>

- Naming rules for `Access modifiers` are following table.

|Modifier|Naming rules|Description|
|---|---|---|
|Public property|PascalCase||
|Internal property|_camelCase||
|Protected property|_camelCase||
|Private property|_camelCase||
|Public constant|PascalCase||
|Internal constant|camelCase||
|Protected constant|camelCase||
|Private constant|camelCase||
|Public field|PascalCase||
|Internal field|_camelCase||
|Protected field|_camelCase||
|Private field|_camelCase||

<br>

- Additional rules are following descriptions.
    - Try to avoid `Hungarian notation` : This is old-fashioned strategy. Modern IDEs could display types immediately
    - Try to avoid abbreviation : It will be allowed if it is well-known word. Consider to present it as PascalCase.
        - ex : `Tcp`, `Http`, `Udp`...
        - ex : Use `MultiThreadExtension` than `MTE`
    - _Don't use_ redundant meanings in names : Using them is meaningless.
        - ex
            ```cs
            enum MyTypesEnum { }
            class UtilityClass { }
            struct DataStruct { }
            ```

<br>

## Coding style

<br>

### Organization

<br>

1. Namespace `using` declarations go at the top of every file.
    - `using` import order is alphabetical.
2. Class member ordering : 
    - Group class members in the following order (Top to bottom, left to right) : 
        - Enum, interface, nested class, delegate and event
        - Const, static and readonly field and static property
        - Field, property (Backing field is on upper line of property)
        - Constructor, finalizer
        - Method
    - For each group, members are located in following access modifier order : 
        - Public
        - Internal
        - Protected internal
        - Protected
        - Private protected
        - Private
        - File
    - And also, members are located in following modifier order : 
        - new
        - abstract
        - virtual
        - override
        - sealed
        - static
        - readonly
        - extern
        - unsafe
        - volatile
        - async

<br>

### Code writing

<br>

1. Align code consistently to improve readability.
2. Limit line length to enhance code readability on docs.
    - Consider environment of code reader.
3. Write only one statement / declaration per line.
4. Open and close brace with new line.
5. Break long statements into multiple lines to improve clarity.
    - ex
        ```cs
        // From

        var LongStatements = new LongStatements("string value", 100, true, false, MyEnum.LongStatementsType, GetSomeValues(), ...);
        ```
        ```cs
        // To

        var BreakStatements = new BreakStatements
        (
            "string value",
            100,
            true,
            false,
            MyEnum.LongStatementsType,
            GetSomeValues(),
            ...
        );
        ```
6. Always input access modifiers to improve clarity.
    - ex : Use `private int` instead of `int`
7. Use C# keywords instead of BCL types
    - ex : Use `int`, `string`, `float` instead of `Int32`, `String`, `Single`, etc
8. Multiple attributes should be separated by new lines.
    - ex
        ```cs
        [WriteLog]
        [Benchmark]
        [RelayCommand]
        private void TestMethod()
        ```
9. Try to avoid `magic` string, number ... : Use `const`, `nameof()` for reusable code, IntelliSense information, and so on.
    - ex
        ```cs
        // NG
        
        switch{myString}
        {
            case "Magic string A":
            break;
        }
        switch{myTypeName}
        {
            case "TypeA":
            break;
        }
        ```
        ```cs
        // Recommended

        switch{myString}
        {
            case Constants.StringA:
            break;
        }
        switch{myTypeName}
        {
            case nameof(TypeA):
            break;
        }
        ```
10. Try to organize variables into group types : Some variables have common purpose
    - ex
        ```cs
        // From

        class MySerialPort
        {
            public string COMPort { get; set; }
            public int BaudRate { get; set; }
            public Parity Parity { get; set; }
            public int DataBits { get; set; }
            public StopBits StopBits { get; set; }
            public Handshake Handshake { get; set; }

            public SerialPort Port;
        }
        ```
        ```cs
        // To

        struct SerialPortInformation
        {
            public string COMPort { get; set; }
            public int BaudRate { get; set; }
            public Parity Parity { get; set; }
            public int DataBits { get; set; }
            public StopBits StopBits { get; set; }
            public Handshake Handshake { get; set; }
        }

        class MySerialPort
        {
            public SerialPortInformation SerialPortInformation;
            public SerialPort Port;
        }
        ```
11. Don't be afraid to using dot operator to namespace : Dot operator `.` is useful for dividing and configuring SW section.
    - ex
        ```cs
        // NG

        namespace CommunicationTransport { }
        namespace CommunicationMessaging { }
        ```
        ```cs
        // Recommended

        namespace Communication.Transport { }
        namespace Communication.Messaging { }
        ```
    - But in general, namespaces should be no more than 3 levels deep.
12. Judiciously use expression body syntax in lambdas and properties.
    - Especially, don't use on method definition.
    - Try to avoid using it except simple single logic.
13. When calling a delegate, use Invoke() and use the null conditional operator.
    - ex : SomeDelegate?.Invoke(). 
        This clearly marks the call at the callsite as ‘a delegate that is being called’. The null check is concise and robust against threading race conditions.

<br>

### Commenting

<br>

1. Place the comment on a separate line, not at the end of a line of code.
2. Begin comment text with an uppercase letter.
3. End comment text with a period.
4. Insert one space between the comment delimiter `//` and the comment text.
5. Avoid long comment in the code file : Load comment after write comment document.
6. Encourage to use `Task list` comments of Visual studio for visibility.

<br>

## References

<br>

- [C# Coding Style](https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md)
- [C# at Google Style Guide](https://google.github.io/styleguide/csharp-style.html)