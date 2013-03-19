log4net.correlationPatternConverter
===================================

A log4net pattern converter that appends System.Diagnostics.Trace.CorrelationManager information to log messages.

## Installation
Download the latest package from the project's [downloads](https://github.com/kcargile/log4net.correlationPatternConverter/downloads) page. 
Extract the zip to a folder on your machine, update your log4net configuration, and you're ready to go.

## Configuration
You can set up the converter by adding it to the <layout> section of your existing log4net configuraiton in App.config or Web.config. 
For example:
```
<layout type="log4net.Layout.PatternLayout">
  <param name="ConversionPattern" value="[%CID] %-5p %d{yyyy-MM-dd hh:mm:ss} :: %m%n" />
  <converter>
    <name value="CID" />
    <type value="log4net.Layout.Pattern.CorrelationPatternConverter, log4net.correlationPatternConverter" />
  </converter>
</layout>
```

The **CID** parameter is what captures the output of the converter and inserts it into the log message (via [%CID]). Incidentally, you 
can change the name of this to whatever you want, so long as the two variable names match.

See App.config in the test project for a complete example.

## Building the Project Yourself
Clone the repository, open log4net.correlationPatternConverter.sln in Visual Studio, select the desired configuration, and click Build -> 
Build Solution (F6). Alternatively, you can build from the command line using csc or MSBuild.

## Dependencies
log4net.correlationPatternConverter is written in C# and requires the MS.NET Framework version 4.0. The only other external dependency
is log4net, which will be installed automatically by Nuget when you build the solution.

## Getting Your Blogger Export File
See Google's documentation on importing and exporting blog data [here]([GitHub](http://support.google.com/blogger/bin/answer.py?hl=en&answer=97416). 

## Issues
If you find an bug please [open an issue](https://github.com/kcargile/log4net.correlationPatternConverter/issues) or alternatively contact 
me via my [blog](http://www.kriscargile.com).

## License
See LICENSE. Copyright (c) 2012, Cargile Technology Group, LLC.
