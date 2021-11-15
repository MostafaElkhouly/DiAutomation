# DiAutomation
Another way for making the developer life easier, I worked on providing a different way for handling dependency injection.
My initial thought is about making each service define its (existing or new) instance lifetime (Singleton, Transient, or Scoped). 
I developed this using the reflection approach.
In the startup class, I used the reflection to load all .dll files then the code searches for the classes that have this annotation (DI),
and based on the annotation value (defined instance lifetime), the code will inject the service instance lifetime during the runtime.
The purpose of this idea is the clean code
