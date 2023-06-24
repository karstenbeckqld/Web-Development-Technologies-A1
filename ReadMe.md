Project Github Repository: https://github.com/rmit-wdt-sp2-2023/s3893749-s3912792-a1

<h3>Implemented Design Patters:</h3>
Dependency Injection

Dependency Injection (DI) is a design pattern to achieve loosely coupled types. It can get implemented in three ways:

1. Constructor Injection
2. Property Injection
3. Method Injection

As the names suggest, in Constructor Injection, the dependency gets injected through the constructor, in Property Injection, through a public property and in Method Injection, through a method.

In essence, what DI does is uncouple the class used. In our case, for example CustomerManager, gets called by the CustomerController class. At this stage of the program, the CustomerManager is implementing IManager<T>,  and therefore a specific implementation. If we had to change the implementation of IManager<T>, the CustomerController would still be able to call the CustomerManager as it is uncoupled from its implementation.

Unknown 2023, *Dependency Injection*, *TutorialsTeacher*, accessed 18 June 2023, [https://www.tutorialsteacher.com/ioc/dependency-injection](https://www.tutorialsteacher.com/ioc/dependency-injection).


