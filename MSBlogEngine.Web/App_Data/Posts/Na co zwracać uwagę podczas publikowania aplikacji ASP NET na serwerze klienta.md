Title: Na co zwracać uwagę podczas publikowania aplikacji ASP.NET na serwerze klienta
Autor: Mateusz Świetlicki
CreateDate: 2012-07-14 19:15
Tags: 	Blog
		ASP.NET
		EntityFramework
		SQL


Na co zwracać uwagę podczas publikowania aplikacji ASP.NET na serwerze klienta
==================

Wczoraj wdrażam system mojej firmy na serwerze przygotowanym dla nas przez klienta (Urzędu Miasta).

W skrócie 15min roboty rozrosło się do 3 godzin.

O to dlaczego:

- Problem 1: Jeśli powiesz klientowi “System wymaga co najmniej Windows 2008 / SQL 2005 / IIS7 itd.” klient prawie na pewno dostarczy Ci właśnie to Minimum.
- Problem 2: ASP.NET 4.0 nie działa na Windows 2008 / IIS 7.0 bez  odpowiedniej konfiguracji.
	- Pamiętaj o instalacji .NET Framework 4.0 Full
	- Pamiętaj o odpaleniu “aspnet_regiis.exe –i” aby skonfigurować IIS7
	- Pamiętaj o wybraniu poprawnej Puli Aplikacji w managerze IIS7
	- Pule dzielą się na Classic i Integrated. Prawie na pewno potrzebujesz Classic.
	- Upewnij się, że włączyłeś ASP.NET 4.0 w “ISAPI and CGI Restrictions”
- Problem 3: MS SQL Express nie instaluje się domyślnie z SQL Managemend Studio, i już na pewno nie z Reporting Services. Upewnij się, że ściągnąłeś odpowiednią instalkę.
- Dobrym pomysłem jest też zabrać ze sobą własny Internet. Klient w cale nie musi mieć WiFi i nie użyczy ci internetu.

Pozdrawiam,