# Asp.Net Core Api SignalR ile QR Kodlu Sipariþ Yönetimi

**Proje Hakkýnda**

* Bu proje N-Tier mimarisiyle oluþturulmuþ, veriler Docker üzerinde Portainer ile ayaða kaldýrýlmýþ MSSQL kullanýlarak depolanmýþ,
Duendde IdentityServer ile JWT token'lar oluþturularak güvenliði saðlanmýþ,
anlýk veirlerin Ajax ve SignalR ile alýndýðý bir sipariþ yönetim projesidir.

* Ana sayfada menüleri görüntüleyebilir, rezervasyon yapabilir, Help Desk üzerinden mesajlarýnýzý Admin kullanýcýlarýna iletebilir, kullanýcý kaydý oluþturup kullanýcýnýzla yorum yapabilirsiniz.

* Menüde yer alan ürünleri kategorilerine göre filtreyebilir ve sepetinize ekleyebilirsiniz. Sepet sayfasýnda ana sayfada yer alan indiirmler bölümündeki indiirm kodunu uygulayarak anlýk indirimlerden yararlanabilirsiniz.

* Admin ve kullanýcý panelleri bulunmaktadýr. Admin panelinden ürün, kategori, indirim, masa ekleme, düzenleme ve silme iþlemlerini, 
sisteme kayýt olmuþ kullanýcýlara AspNetRoles ve AspNetUserRoles tablolarýný kullanarak yeni rol tanýmý ve kullanýcýya rol atama iþlemlerini,
Dashboard üzerinden ürünlere ve finansal verilerin istatistiklerini görüntüleme,
Masa durumlarýný anlýk olarak takip edebilir dolu masalara ait sipariþ özetini görüntüleme ve ödeme iþlemlerini,
Rezervasyonlarý görüntüleme, düzenleme ve yeni rezervasyon oluþturma iþlemlerini yapabilirsiniz. Rezervasyona ait herhangi bir deðiþlik yapýldýðýnda veya yeni bir rezervasyon oluþturulduðunda mail adresine rezervasyon bilglerini mail olarak iletilmektedir.

* Kullanýcý panelinden rezervasyon oluþturabilir, rezervasyonlarýnýzý görüntüleyebilir, yorum ve help desk mesajlarýný listeleyebilir, yeni mesaj gönderebilirsiniz. Kullanýcý bilgilerinizi veya þifre deðiþikliði yapabilirsiniz.

**Kullanýlan Teknolojiler**

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

**Projeye Ait Ekran Görüntüleri**
- Daha fazlasý için **SignalRWebUI -> wwwroot -> asset -> screenshots** klasörü altýndan ulaþabilirsiniz.

**Videolar**

- Genel Tanýtým, Sipariþ ve Sepet Ýþlemleri
![Main Page Video](SignalRWebUI/wwwroot/asset/screenshots/genel_tanitim_siparis_sepet.mp4)

- Yeni Kullanýcý Kayýt Ýþlemi ve Þifre Deðiþikliði
![Register Video](SignalRWebUI/wwwroot/asset/screenshots/kullanici_kaydi_sifre_degisikligi.mp4)

- Rezervasyon Ýþlemleri
![Booking Video](SignalRWebUI/wwwroot/asset/screenshots/rezervasyon.mp4)

- Sistem Rolleri ve Kullanýcý Rol Tanýmlamalarý
![Administration Processes Video](SignalRWebUI/wwwroot/asset/screenshots/sistem_rolleri_ve_kullanici_rolleri.mp4)

- Kullanýcý Mesajlarý
![Admin Panel](SignalRWebUI/wwwroot/asset/screenshots/kullanici_mesajlari.mp4)

**Ekran Görüntüleri**

![Screenshot_1](SignalRWebUI/wwwroot/asset/screenshots/main.jpg)
![Screenshot_2](SignalRWebUI/wwwroot/asset/screenshots/menu_1.jpg)
![Screenshot_3](SignalRWebUI/wwwroot/asset/screenshots/menu_2.jpg)
![Screenshot_4](SignalRWebUI/wwwroot/asset/screenshots/login.jpg)
![Screenshot_5](SignalRWebUI/wwwroot/asset/screenshots/register.jpg)
![Screenshot_6](SignalRWebUI/wwwroot/asset/screenshots/docker.jpg)
![Screenshot_7](SignalRWebUI/wwwroot/asset/screenshots/portainer.jpg
**QR Kod okunmasý sýrasýnda bir sorun oluþup masa seçimi yapýlamazsa manuel olarak masa seçimi yapýlýr.**
![Screenshot_8](SignalRWebUI/wwwroot/asset/screenshots/default_restaurant_tables.jpg)
![Screenshot_9](SignalRWebUI/wwwroot/asset/screenshots/admin_dashboard_.jpg)
![Screenshot_10](SignalRWebUI/wwwroot/asset/screenshots/money_case_.jpg)
![Screenshot_11](SignalRWebUI/wwwroot/asset/screenshots/money_case_history.jpg)
![Screenshot_12](SignalRWebUI/wwwroot/asset/screenshots/tables.jpg)
![Screenshot_13](SignalRWebUI/wwwroot/asset/screenshots/table_status.jpg)
![Screenshot_14](SignalRWebUI/wwwroot/asset/screenshots/system_roles.jpg)
![Screenshot_15](SignalRWebUI/wwwroot/asset/screenshots/users_list.jpg)
![Screenshot_16](SignalRWebUI/wwwroot/asset/screenshots/user_roles.jpg)