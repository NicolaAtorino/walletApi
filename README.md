# walletApi
Wallet Api basic implementation

This Api is written with asp.net web api 2 technology, not hosted on IIS but self-hosted using OWIN.
In this way, the app will be very lightweight, by giving us several hosting possibilities : we can run it as a service
with TopShelf, or just as a console application (like in this example) during development.

## Design

I tried to stick to the web api design conventions by creating controllers focused on resources and not on actions. For this reason,
you'll find two controllers, users and accounts. The design has still room for a lot of improvements (like implementing OData), 
but i found them not necessary for this test given the short requirement list and the limited time at disposal.

The api answers to the basic requirements by talking with an underlying service, wich in turn talks to a repository built on top
of an entity framework DataModel.

## database

e database itself it's not on gitHub. You can download it from dropbox with [this link](https://www.dropbox.com/s/ui8mqlu2n4wa995/WalletApiDB.zip?dl=0) . 
It should be added at the root level of the walletApi project in order for the connection string to find it.

## repository

I implemented the generic repository pattern in order to kepp the app ready to be extended easily by quickly adding other repositories. When a new entity is created,
the only steps needed for implementing a basic repositories will be to create a new interface that implements the generic one , create a new class that will implement the abstract one, 
and bind them through D.I. If specific code has to be written, the new derived class and interface can extend the basic one.

## D.I.

The components are held toghether trough Dependency Injection (constructor injection pattern).
I used Ninject as a container, because it contains a quick out-of-the box solution for owin-hosted webApi.

## Interface

I added a basic client interface to the api using swagger. If you navigate to localhost:5000/swagger , you will be able to check all the controllers' actions 
and try them out directly from there. Keep in mind that right now there is only one account in the database, Id 1 (userId is 1, too.) . I did not add an 'addaccount' 
action because it was not in the requirements, but that could be done easily.

Swagger also reads the xml comment put on the controllers and action to present them in the ui interface; it can be also used (by removing the 'try it out' option) 
as an online documentation because it automatically updates with the system.

## Data Validation

The application handles input validation both at controller and at service level. In this way, it will be easy to eventually use the service in other contexts
without losing these checks, while in the meantime it will be possible for the controllers to return a 400 in case of badRequest toghether with a full response.

## Unit Tests
Given the simplicity of the controllers, i decided to focus on testing the service because that is the main logic point of the application. 
There are four classes, each one of them responsible for testing every method of the service in different contexts. The normal behaviour of the webapp
should be to never answer with a 500 status code, but with a 200 or a 400 in case of bad data.

## ViewModel

The application communicates to the external world through the 'operationResult' class for operation witouth a return, 
and with the 'operationresult<T>' for operation with a return. This way will be easy for the client to understand if an operation succeded or not, and why,
by just implementing this pattern that will be mostly the same for every response. The class is also used for communication between the service and the controller,
for the same reason.

The simplicity of the requirements did not ask for creating complex objects to be serialized for client consuming.

## logging 

I used Nlog for logging, by creating a simple console target that should take every log the application produces. For now, only exceptions are logged.

## Improvements
Several things can still be improved. 
Ninject is not the fastest D.I. container, but it was chosen for simplicity. 

More unit test could be written, for example for testing the controllers' validation system (that is very straightforward, but could change in the future)

Integration tests that will involve a test database could be implemented, maybe by using external tools like postman or soapUI.

A proper client could be implemented in order to use swagger only for in-line documentation of the system.

Logging could be greatly improved by adding logs on several points of the app.

