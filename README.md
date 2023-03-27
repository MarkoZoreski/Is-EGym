# E-Gym

E-Gym is a simple ASP.NetCore school project for "Integrated Systems". It showcases the layered architecture style that aims to provide a separation of concerns between different components of the web application. In this architecture style, the application is divided into layers, with each layer having a specific responsibility and interacting with the layers above and below it in a defined way.

---
# App components
E-Gym consists of four main layers: the presentation layer, the business logic layer, the services layer, and the data storage layer:

-Eshop.Domain: responsible for representing the data and business entities within the application. It defines the structure of the data and the business logic that operates on it. 

-Eshop.Repository: provides an abstraction layer over the data storage layer. It is responsible for retrieving, persisting, and managing data.

-Eshop.Service: contains the business logic of the application. It provides a high-level API that can be called by other parts of the application, such as the presentation layer or other services.

-Eshop.Web: responsible for handling incoming requests and generating responses to be sent back to the user. The web layer communicates with the services layer to perform business logic and retrieve data, and then generates a response to be sent back to the user.

---
# Main page with some products added

![image](https://user-images.githubusercontent.com/83420035/228055201-9409ca27-62c6-43fc-8e5b-b839984c43c1.png)
