This Application is Single Page Appliation project to demonstrate fetching Mars Rovar Image of the day using NASA API. 

WebSpa Appliation:
=================
1. Angular Single Page Appliation Project
2. API Gateway Project using .Net Core Server

Appliation Features:
===================
1. Component to present Image Dates as button list
2. Image Viewer Card for on button click view operation.  
3. GetDates API to read from "dates.txt" file in "Dates" folder.
4. GetImageContent API to get Image file information. Images are saved in "wwwroot" folder only once.

Angular application consumes the APIs and load dates and image file from local environment.


Tech Stack:
===========
1. Angular
2. .Net Core 5.0 

NASA Api Nuget Package : 
APOD.net 

How to Run:

You can build and run this application in following steps

1. Git clone or download the application
2. Open in Visual Studio 2019 and build
3. Run using IIS server
4. WebSpa will be loaded in url, http://localhost:63655/

You can also use Visual Studio Code to compile and run using,

#npm install

#ng build

#ng serve

Please note: .Net applicaiton needs to be running for API gateway access

Additional features:
1. Dockerfile added

Optimization work will be uploaded continuously. 

Please feel free advice for any posible modification!   