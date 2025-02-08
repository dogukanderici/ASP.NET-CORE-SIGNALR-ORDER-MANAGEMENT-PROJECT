# Asp.Net Core Api SignalR ile QR Kodlu Sipari� Y�netimi

**Proje Hakk�nda**

* Bu proje N-Tier mimarisiyle olu�turulmu�, veriler Docker �zerinde Portainer ile aya�a kald�r�lm�� MSSQL kullan�larak depolanm��,
Duendde IdentityServer ile JWT token'lar olu�turularak g�venli�i sa�lanm��,
anl�k veirlerin Ajax ve SignalR ile al�nd��� bir sipari� y�netim projesidir.

* Ana sayfada men�leri g�r�nt�leyebilir, rezervasyon yapabilir, Help Desk �zerinden mesajlar�n�z� Admin kullan�c�lar�na iletebilir, kullan�c� kayd� olu�turup kullan�c�n�zla yorum yapabilirsiniz.

* Men�de yer alan �r�nleri kategorilerine g�re filtreyebilir ve sepetinize ekleyebilirsiniz. Sepet sayfas�nda ana sayfada yer alan indiirmler b�l�m�ndeki indiirm kodunu uygulayarak anl�k indirimlerden yararlanabilirsiniz.

* Admin ve kullan�c� panelleri bulunmaktad�r. Admin panelinden �r�n, kategori, indirim, masa ekleme, d�zenleme ve silme i�lemlerini, 
sisteme kay�t olmu� kullan�c�lara AspNetRoles ve AspNetUserRoles tablolar�n� kullanarak yeni rol tan�m� ve kullan�c�ya rol atama i�lemlerini,
Dashboard �zerinden �r�nlere ve finansal verilerin istatistiklerini g�r�nt�leme,
Masa durumlar�n� anl�k olarak takip edebilir dolu masalara ait sipari� �zetini g�r�nt�leme ve �deme i�lemlerini,
Rezervasyonlar� g�r�nt�leme, d�zenleme ve yeni rezervasyon olu�turma i�lemlerini yapabilirsiniz. Rezervasyona ait herhangi bir de�i�lik yap�ld���nda veya yeni bir rezervasyon olu�turuldu�unda mail adresine rezervasyon bilglerini mail olarak iletilmektedir.

* Kullan�c� panelinden rezervasyon olu�turabilir, rezervasyonlar�n�z� g�r�nt�leyebilir, yorum ve help desk mesajlar�n� listeleyebilir, yeni mesaj g�nderebilirsiniz. Kullan�c� bilgilerinizi veya �ifre de�i�ikli�i yapabilirsiniz.

**Kullan�lan Teknolojiler**

- .Net 8
- SignalR
- Docker
- Portainer
- MSSQL
- Duende IdentityServer
- Entity Framework
- Ajax
- N-Tier Architecture
- Dependency Injection Design Pattern
- AutoMapper
- FluentValidation
- Mimekit

**Projeye Ait Ekran G�r�nt�leri**
- Daha fazlas� i�in **SignalRWebUI -> wwwroot -> asset -> screenshots** klas�r� alt�ndan ula�abilirsiniz.

**Videolar**

- Genel Tan�t�m, Sipari� ve Sepet ��lemleri
![Main Page Video](SignalRWebUI/wwwroot/asset/screenshots/genel_tanitim_siparis_sepet.mp4)

- Yeni Kullan�c� Kay�t ��lemi ve �ifre De�i�ikli�i
![Register Video](SignalRWebUI/wwwroot/asset/screenshots/kullanici_kaydi_sifre_degisikligi.mp4)

- Rezervasyon ��lemleri
![Booking Video](SignalRWebUI/wwwroot/asset/screenshots/rezervasyon.mp4)

- Sistem Rolleri ve Kullan�c� Rol Tan�mlamalar�
![Administration Processes Video](SignalRWebUI/wwwroot/asset/screenshots/sistem_rolleri_ve_kullanici_rolleri.mp4)

- Kullan�c� Mesajlar�
![Admin Panel](SignalRWebUI/wwwroot/asset/screenshots/kullanici_mesajlari.mp4)

**Ekran G�r�nt�leri**

![Screenshot_1](SignalRWebUI/wwwroot/asset/screenshots/main.jpg)
![Screenshot_2](SignalRWebUI/wwwroot/asset/screenshots/menu_1.jpg)
![Screenshot_3](SignalRWebUI/wwwroot/asset/screenshots/menu_2.jpg)
![Screenshot_4](SignalRWebUI/wwwroot/asset/screenshots/login.jpg)
![Screenshot_5](SignalRWebUI/wwwroot/asset/screenshots/register.jpg)
![Screenshot_6](SignalRWebUI/wwwroot/asset/screenshots/docker.jpg)
![Screenshot_7](SignalRWebUI/wwwroot/asset/screenshots/portainer.jpg
**QR Kod okunmas� s�ras�nda bir sorun olu�up masa se�imi yap�lamazsa manuel olarak masa se�imi yap�l�r.**
![Screenshot_8](SignalRWebUI/wwwroot/asset/screenshots/default_restaurant_tables.jpg)
![Screenshot_9](SignalRWebUI/wwwroot/asset/screenshots/admin_dashboard_.jpg)
![Screenshot_10](SignalRWebUI/wwwroot/asset/screenshots/money_case_.jpg)
![Screenshot_11](SignalRWebUI/wwwroot/asset/screenshots/money_case_history.jpg)
![Screenshot_12](SignalRWebUI/wwwroot/asset/screenshots/tables.jpg)
![Screenshot_13](SignalRWebUI/wwwroot/asset/screenshots/table_status_.jpg)
![Screenshot_14](SignalRWebUI/wwwroot/asset/screenshots/system_roles_.jpg)
![Screenshot_15](SignalRWebUI/wwwroot/asset/screenshots/users_list_.jpg)
![Screenshot_16](SignalRWebUI/wwwroot/asset/screenshots/user_roles_.jpg)