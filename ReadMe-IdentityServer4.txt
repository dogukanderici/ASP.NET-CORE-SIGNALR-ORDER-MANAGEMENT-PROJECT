- identityserver4 art�k desteklenmemektedir. Bunun yerine Duende IdentityServer kullan�lmaktad�r. Duende.IdentityServer maunel olarak kurulmu�tur. Kurulum kodlar� a�a��dad�r.

- Projenin bulundu�u dizinde yeni bir klas�r olu�tulur. (�rn. IdentityServer)
- cmd �zerinden bu olu�turulan dizine gidilir.
- dotnet new webapi -n SignalR.IdentityServer ile yeni bir web api projesi olu�turulur. Bu proje alt�na gidilir. ( api i�in webapi normal proje i�in web )
- A�a��daki iki kod s�rayla �al��t�r�l�r.
- dotnet add package Duende.IdentityServer ( lastest )
- dotnet add package Duende.IdentityServer.AspNetIdentity ( lastest )

Bu �ekilde bir proje olu�turulacak ve proje Duende.IdentityServer y�klenmi� olacakt�r.

- Program.cs dosyas�na konfig�rasyonlar eklenir.
- Config.cs dosyas� olu�turulur.