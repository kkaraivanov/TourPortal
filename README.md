## 👓 Project Introduction
***
**Tour Posrtal** is my defense project for **ASP.NET Core** course at [SoftUni](https://softuni.bg/ "SoftUni") (June-August 2021). The website is part of a major project, and it consists of 2 parts (**TourPortal Client** and **TourPortal Server**). Each of the two parts is starts independently, **giving functionality to a Single Page Application (SPA)**.

## &copy; Tour Posrtal 2021
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628846476/TourPortal/slider_breed0.png)
<br />

## ✏️ Overview
***
**Tour Portal** is a site that is a represents platform for gathering people looking for a hotel to visit and hoteliers offering their services. Each customer of the site at his registration can be defined as a ***user*** or ***owner*** to receive the desired functionality from the platform. The main idea is to organize a space in which business get a working environment in which to offer and sell their services, and consumers have easy access to them.
<br />

>Each hotel **owner** can register the services he offers, as well as his employees.
>>*A employee of the hotel is a user who processes the bookings of services. A registered service is confirmed by the employee when a user who has booked a service visits the hotel. The employee cannot register his account in the platform*.

>Each **user** can search for services by criteria such as *free date, number of beds, number of people* and then can make a reservation for a selected service.
<br />

There is only one registered administrator in the platform. It receives information about registered users, services and reservations. The administrator can disable or delete entries in the system.
<br />

## 🔏 Used Roles
***
- Administrator
- Owner
- Employe
- User
<br />

## 🔨 Used technologies
***
- [ASP.NET CORE 5.0](https://dotnet.microsoft.com/download/dotnet/5.0 "CORE 5.0")
- ASP.NET Identity
- ASP.NET Security [JwtBearer](https://jwt.io/ "Jwt")
- ASP.NEt Authorization
- ASP.NET CORE view components
- ASP.NET Core areas
- MSSQL Server
- [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/ "IdentityServer")
- Serilog
- Serilog AspNetCore
- [MyTested AspNetCore Mvc](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc)
- XUnit
- Client-Side [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor "Blazor")
- Comunication between Blazor components based on EventHandlers
- Blazored LocalStorage
- Blazored Modal
- Radzen Blazor
- [BlazorStrap](https://blazorstrap.io/ "BlazorStrap")
- Microsoft Json
- [Newtonsoft Json](https://www.newtonsoft.com/json "Newtonsoft Json")
- [Bootstrap](https://github.com/twbs/bootstrap)
<br/>

## 📷 Screenshots
***
### 1. Unregistered users have these views
- Home page
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855727/TourPortal/homepage_rojqkg.jpg)
- Register page
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855727/TourPortal/register_fqo5le.jpg)
- Login page
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855727/TourPortal/login_eshnfz.jpg)
- Menu options
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855727/TourPortal/menue_ulkmiv.jpg)

### 2. Registered users have these views
- Hotel page
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855727/TourPortal/button-hotel_pl8mwa.jpg)
- Employe page
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855727/TourPortal/button-employe_n2kkod.jpg)
- Profile view
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855727/TourPortal/profile-view_vzmcaz.jpg)
- Profile edit
![](https://res.cloudinary.com/dwxsoifb5/image/upload/v1628855728/TourPortal/profile-edit_hem69w.jpg)
<br />

## 🔧Setup of the layouts
***
The application uses two layouts for registered and unregistered users respectively. The default layout is the layout for unregistered users.  All required CSS and JS libraries are loaded through the layout for registered users using the following code:

>```C#
>private async Task RemoveElementsFromTemplate(IJSObjectReference module)
>{
>    await module.InvokeVoidAsync("removeScripts");
>     await module.InvokeVoidAsync("removeCss");
>}
>
>private async Task AddStyles(IJSObjectReference module)
>{
>    foreach (var cssPath in cssPaths)
>    {
>        await module.InvokeVoidAsync("addCss", cssPath);
>    }
>}
>
>private async Task AddScripts(IJSObjectReference module)
>{
>    foreach (var scriptPath in scriptPaths)
>    {
>        await module.InvokeVoidAsync("addScripts", scriptPath);
>    }
>}
<br />

## 💎 Used templates
- **[Picstudio](https://html.design/download/photography-studio-psd-template/ "Picstudio")** – Photography Studio PSD Template
- **[Pluto](https://html.design/download/pluto-admin-template "Pluto")** - Bootstrap Admin Dashboard 
<br />

## ✍️ Author
***
[Kostadin Karaivanov](https://github.com/kkaraivanov)
<br />

<a href="mailto:kostadin.karaivanov@outlok.com"><img src="https://img.shields.io/badge/-kostadin.karaivanov@outlok.com-EA4335?style=flat&logo=gmail&logoColor=white"/></a>&nbsp;
<a href="https://www.linkedin.com/in/kostadin-karaivanov-8390061a5/"><img src="https://img.shields.io/badge/-Kostadin%20Karaivanov-0A66C2?style=flat&logo=linkedin&logoColor=white"/></a>&nbsp;
<a href="https://www.facebook.com/profile.php?id=100000311415045"><img src="https://img.shields.io/badge/-Kostadin%20Karaivanov-1877F2?style=flat&logo=facebook&logoColor=white"/></a>&nbsp;
![Discord](https://img.shields.io/badge/-kkaraivanov-7289DA?style=flat&logo=discord&logoColor=white)