- identityserver4 artýk desteklenmemektedir. Bunun yerine Duende IdentityServer kullanýlmaktadýr. Duende.IdentityServer maunel olarak kurulmuþtur. Kurulum kodlarý aþaðýdadýr.

- Projenin bulunduðu dizinde yeni bir klasör oluþtulur. (Örn. IdentityServer)
- cmd üzerinden bu oluþturulan dizine gidilir.
- dotnet new webapi -n SignalR.IdentityServer ile yeni bir web api projesi oluþturulur. Bu proje altýna gidilir. ( api için webapi normal proje için web )
- Aþaðýdaki iki kod sýrayla çalýþtýrýlýr.
- dotnet add package Duende.IdentityServer ( lastest )
- dotnet add package Duende.IdentityServer.AspNetIdentity ( lastest )

Bu þekilde bir proje oluþturulacak ve proje Duende.IdentityServer yüklenmiþ olacaktýr.

- Program.cs dosyasýna konfigürasyonlar eklenir.
- Config.cs dosyasý oluþturulur.