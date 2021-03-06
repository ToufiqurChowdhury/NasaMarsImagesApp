This project demonstrates fetch/load Mars Rover Image of the day using NASA API. 

User experience:
================
![NasaMarsImagesAppSS](https://user-images.githubusercontent.com/60831585/134740696-ff006a40-4545-4f6a-9727-7e9e5ca968da.PNG)

WebSpa Application
==================
1. Angular Single Page Application
2. API Gateway using .Net Core Server

Application Features
====================
1. Component to present Image Dates as button list
2. Image Viewer Card for on button click view operation.  
3. GetDates API to read from "dates.txt" file in "Dates" folder.
4. GetImageContent API to get Image file information. Images are saved in "wwwroot" folder.

Angular application consumes the APIs and load dates and image file from local environment.


Tech Stack
===========
1. Angular
2. .Net Core 5.0
3. Xunit for Unit Test

NASA Api Nuget Package : 
APOD.net 

How to Run
===========
Please Update NASA_API_Key in "Contstants/MarsImageConstants.cs" 

You can build and run this application in following steps

1. Git clone or download the application
2. Open in Visual Studio 2019 and build
3. Run using IIS server
4. WebSpa will be loaded in url, http://localhost:63655/

You can also use Visual Studio Code to compile and run using,

#npm install

#ng build

#ng serve

Note: .Net application needs to be running for API gateway access

**Reference:**
https://github.com/MarcusOtter/APOD.Net

**License:**
MIT License
