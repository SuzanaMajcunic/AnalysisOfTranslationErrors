# TREAT (TRanslation Error Annotation Tool)

<img src="https://github.com/SuzanaMajcunic/AnalysisOfTranslationErrors/blob/master/Print-screens/Logo_TREAT.png" title="TREAT logo" width="250">

TREAT is a graphical Windows application that is developed as a product of my graduate thesis. It is a user-friendly Windows 10 application, for the analysis of translation errors. It is developed with the aim to simplify the process of error analysis. The app has a built-in MQM (Multidimensional Quality Metrics) support for annotation of translation quality. TREAT is an intuitive tool for machine translation evaluation. In the folder 'Print-screen' are appended print-screens of the application in use.

TREAT supports:
- loading text files
- bilingual sentences review and tagging of parts of sentences according to the MQM
- modification of system sentences and calculation of process time
- modification of MQM hierarchy by adding new nodes, removing new nodes and excluding built-in nodes
- search of sentences by annotated issue type
- displaying basic numerical statistics of annotated issue types
- generating and displaying visualizations of annotated issue types (Line chart and Pie chart)
- local storage of statistics file(.txt, .doc, .rtf) and visualizations (.jpg, .png)
- creating new projects and storing it in .txt format
- loading existing projects and browsing in local storage
- quick load of recently added projects

Requirements and limitations:
- The app is supported only on Windows 10 system (32bit or 64bit), version 1803 or higher.
- All text files that contain translation sentences must be in UTF8 format.
- All text files that contain translation sentences must respect the rule of parallel corpus (equal number of sentences).

The application is implemented in a C# language by using Universal Windows Platform (UWP) which is an implementation of .NET that is used for building modern, touch-enabled Windows applications and software for the Internet of Things (IoT). The graphics are implemented in declarative language XAML (Extensible Application Markup Language).
Charts are implemented by WinRT XAML Toolkit, which support charting controls for UWP (winrtxamltoolkit.controls.datavisualization.uwp).
A very helpful concept used in this development is LINQ (Language Integrated Query), a Microsoft .NET Framework component that adds query capabilities to C# and another .NET languages.

The project was built by Model–view–viewmodel (MVVM) software architectural pattern. MVVM is an architecture that focuses on the separation of development of user interface from development of the business logic.

This is the first version of TREAT and the development of additional features is in the plan.
