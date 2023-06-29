Project Github Repository: https://github.com/rmit-wdt-sp2-2023/s3893749-s3912792-a1

<h3>Implemented Design Patters:</h3>
Dependency Injection

Dependency Injection (DI) is a design pattern to achieve loosely coupled types. It can get implemented in three ways:

1. Constructor Injection
2. Property Injection
3. Method Injection



Unknown 2023, *Dependency Injection*, *TutorialsTeacher*, accessed 18 June 2023, [https://www.tutorialsteacher.com/ioc/dependency-injection](https://www.tutorialsteacher.com/ioc/dependency-injection).


<h2>Used Design Patterns:</h2>

<h3>Dependency Injection:</h3>
Dependency Injection (DI) is a design pattern that facilitates code reuse by decoupling the usage of an object from its 
creation. It enables the replacement of dependencies without changing the class that uses them (Thorben 2023). DI can 
get applied in three forms: Constructor Injection, Setter or Property Injection, or Interface Injection. These are 
sometimes called type 3 IoC, type 2 IoC and type 1 IoC, respectively, where IoC stands for Inversion of Control 
(Fowler 2004). As the names suggest, in Constructor Injection, the dependency gets injected through the constructor, in 
Property Injection, through a public property and in Method Injection, through a method.

In essence, what DI does is uncouple the class used. In our case, for example CustomerManager, gets called by the 
CustomerController class. At this stage of the program, the CustomerManager is implementing IManager<T>,  and therefore 
a specific implementation. If we had to change the implementation of IManager<T>, the CustomerController would still be 
able to call the CustomerManager as it is uncoupled from its implementation.


We use DI in our code in the following way:

<h3>Façade Design Pattern:</h3>
The App.cs file uses a façade design pattern. The Façade Pattern is a structural design pattern often used to create 
more unified interfaces to complex systems. This way, access to the underlying methods is simplified (bin Uzayr 2022). 
The façade pattern is one of the Gang of Four (GoF) design patterns published in 1994 by authors Erich Gamma, Ralph 
Johnson, Richard Helm, and John Vlissides (Unknown 2021).<br> It gets called a façade because, like the façade of a building, 
it provides access but hides the underlying functionalities. Following this approach, the App.cs class is a universal 
façade class that forms a single entry point. It acts as an interface for the underlying systems by calling methods like 
Start(), GetCurrentUser() and SwitchView().<br> With a moderately complex system like ours, it is appropriate to use a façade 
pattern as it hides the complexity (Kumar 2023).
<p align="center"> <img src="images/facade-pattern.png" alt="The facade design pattern schematics" width="300"></p>

<h3>MVC Design Pattern:</h3>
One of the best-known design patterns is the Model-View-Controller (MVC) pattern. Its purpose is to separate internal 
representations of information from how it gets presented to the user (Reenskaug & Coplien 2009). Initially used for 
graphical user interfaces, this pattern became standard for web applications. Trygve Reenskaug created the MVC design 
pattern in the late 1970s. He wanted a design pattern to structure any program where users interact with a large data 
set. Initially, his design had four parts: Model, View, Thing, and Editor. However, he and the rest of his group 
eventually settled on model, view, and controller (Reenskaug 1979).<br>
The MVC pattern consists of three components. The model represents the central component of an application's dynamic 
data structure. It is independent of the user interface. The model manages the application's data, rules and logic and 
often gets represented by a database in modern web applications.<br>
The view is a representation of this information. While in the past, it usually consisted only of a chart, modern web 
applications use HTML templates. The view can receive input from the user but is general-purpose and composable (Fowler 2006).<br>
The controller handles user events. At any given time, it has one associated view and model. While a model object can 
hear from many controllers, only one controller (the "active" controller) receives input at any given time 
(LaLonde & Pugh 2009). The active controller usually gets set by a global window manager.<br>
In our application, we implemented the MVC pattern using the façade (window manager) to implement the required 
controller (framework). The model and its connected controllers got placed in the class library (MyBankDbAccess), whose 
sole purpose is to handle data to and from the database.

<h3>The 'required' modifier:</h3>
The required modifier indicates that the property it gets applied to must be initialised by an object initialiser and 
got introduced with C# 11. Any expression that initialises a new instance of this type must initialise all required 
members why we use the required modifier in our model types for essential fields. This restriction allows code to create 
classes that require proper initialisation of properties with the flexibility of still using object initialisers. The 
required modifier can get applied to fields and properties declared in classes and structs, including record and record 
structs and interfaces.<br>
Required properties or fields must be, at a minimum, as visible as their containing type and have setters (set or init 
accessors) that are at least as visible as their containing type.
Some types use a primary constructor to initialise positional properties. The primary constructor adds the 
[SetsRequiredMembers] attribute if those properties include the required modifier. This setting indicates that the 
primary constructor initialises all the necessary members.<br>
However, the compiler doesn't verify that these constructors do initialise all required members. Instead, the attribute 
asserts to the compiler that the constructor does initialise all required members. In principle, the [SetsRequiredMembers] 
disables the compiler's checks that all required members get initialised upon the creation of an object (Wagner & Bryant 2023).
We use the required modifier within our model classes for the customer ID property in Customer and Account, the login ID 
in Login, and the account number and transaction type properties in Transaction. We've chosen these fields to be required 
because, in the case of the Account, Customer and Login classes, they resemble the primary key columns in the database 
tables. But also they define the sole property that references them.<br>
In the Transaction class, we focus on the account number and transaction type, as the user never sets the primary key. 
The account number, however, is the crucial reference for each transaction and hence essential. In addition, the 
transaction type determines if a fee applies and should also be required.

<h2>References</h2>
Fowler, M 2004, Inversion of Control Containers and the Dependency Injection pattern, MartinFowler.com, accessed 27 June 2023, <https://martinfowler.com/articles/injection.html>.<br>
Fowler, M 2006, GUI Architectures, MartinFowler.com, accessed 29 June 2023, <https://martinfowler.com/eaaDev/uiArchs.html#ModelViewController>.<br>
Kumar, S 2023, Facade Design Pattern | Introduction, GeeksforGeeks, accessed 28 June 2023, <https://www.geeksforgeeks.org/facade-design-pattern-introduction/>.<br>
LaLonde, WR & Pugh, JR 2009, Inside Smalltalk, Volume 3,.<br>
Reenskaug, T 1979, Trygve/MVC, MVC, accessed 29 June 2023, <https://folk.universitetetioslo.no/trygver/themes/mvc/mvc-index.html>.<br>
Reenskaug, T & Coplien, J 2009, The DCI Architecture: A New Vision of Object-Oriented Programming, Artima, accessed 28 June 2023, <https://web.archive.org/web/20090323032904/https://www.artima.com/articles/dci_vision.html>.<br>
Thorben 2023, Design Patterns Explained – Dependency Injection with Code Examples, Stackify, accessed 27 June 2023, <https://stackify.com/dependency-injection/>.<br>
Unknown 2021, Facade pattern: unified interface for software projects , IONOS Products, accessed 28 June 2023, <https://www.ionos.com/digitalguide/websites/web-development/whats-the-facade-pattern/>.<br>
bin Uzayr, S 2022, Software Design Patterns, First Edit, CRC Press, Boca Raton.<br>
Wagner, B & Bryant, B 2023, required modifier - C# Reference, Microsoft Learn, accessed 29 June 2023, <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required>.<br>
 
