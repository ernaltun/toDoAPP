GÖREV TAKİP PROGRAMI

Projede todo list mantığıyla belli başlı görevlerin takip edilecebileceği bir yapı olarak tasarlanmış ve uygulanmıştır. Kullanıcı giriş sistemi ile birlikte
her kullanıcı kendi takip listesini oluşturabilir,güncelleyebilir ve silebilir. Ek olarak bu işlemlerin hepsinde log tutulmakta ve loglar logs/logDoc.txt dosyasına kaydedilmektedir.
//Eğer logs dosyası bulunamadı ise proje içerisine logs klasörü açılması yeterli olacaktır.

Proje dosyaları içerisindeki sqlScript dosyası sql de çalıştırıldıktan sonra visual studio da projenin başlatılmasıyla kullanıma başlanabilir.

Proje repository design pattern ile olabildiğince sadeleştirilmiş kod yapısı oluşturulmaya çalışılmıştır kullanılan teknolojiler; Entity Framewok, ASP.NET MVC, Serilog, GitHub ve Sql Server.
Temel olarak Interfacelerde tanımladığımız veri işlemlerini Repositorylerde veri erişimi ile sorguları gerçekleştirdikten sonra bu veriler ile ilgili tüm işlemleri gerçekleştirip
modellere ilettik ve arayüz işlemlerini gerçekleştirdik.
