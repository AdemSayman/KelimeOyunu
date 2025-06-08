6 Sefer ile Kelime Ezberleme Sistemi
Proje Hakkında
Bu proje, yazılım mühendisliği döneminde Scrum metodu kullanılarak geliştirilmiş, kelime ezberleme odaklı bir eğitim uygulamasıdır. Temel amacı, kullanıcının belirlenen algoritma ile 6 kez üst üste doğru cevap vererek kelimeleri kalıcı olarak öğrenmesini sağlamaktır.

Projede kullanıcı kayıt/giriş, kelime ekleme, sınav modülü, ayarlar, analiz raporu ve bulmaca modülleri bulunmaktadır. Ayrıca, proje sürecinde Scrum tablosu oluşturularak görevler planlanmış ve takip edilmiştir.


Kullanılan Teknolojiler
Programlama Dili: C# 

Veritabanı: SQL Server 

Scrum Takip: Trello

Versiyon Kontrol: Git & GitHub

Opsiyonel: SonarQube 

Proje Özellikleri
1	Kullanıcı Kayıt & Giriş	5	
2	Kelime Ekleme Modülü	5
3	6 Sefer Algoritmalı Sınav Modülü	10	
4	Kullanıcı Ayarları (Kelime sayısı)	5	
5	Analiz Raporu & Yazdırma	5	
6	Bulmaca (Wordle) Modülü	15	
7	Word Chain & LLM Hikaye/Görsel	5

Kullanıcı ve Veri Modelleri
Users
UserID (PK, int)
UserName (string)
Password (string)
Words
WordID (PK, int)
EngWordName (string)
TurWordName (string)
Picture (string - dosya yolu)
WordSamples
WordSamplesID (PK, int)
WordID (int)
Samples (string)

6 Sefer Ezberleme Algoritması
Kullanıcının aynı kelimeyi 6 farklı zamanda doğru olarak cevaplaması gerekmektedir:
1 gün, 1 hafta, 1 ay, 3 ay, 6 ay, 1 yıl aralıklarla.
6 defa üst üste doğru yapamayan kelimeler teste tekrar eklenir. Bu sayede öğrenme kalıcılığı artırılır.

Proje Çalışma Akışı
Kullanıcı kayıt olup giriş yapar.
Kelime ekleme ekranından kelimeler eklenir (resim ve opsiyonel ses içerir).
Sınav modülünde 6 sefer algoritmasına göre testler yapılır.
Kullanıcı istediğinde yeni kelime sayısını ayarlardan değiştirebilir.
Analiz raporunda başarı yüzdeleri ve öğrenme durumu görüntülenir, rapor çıktı alınabilir.
Bulmaca modülüyle kelimelerle oyun oynanabilir.

Proje Yönetimi ve Süreç
Scrum Takibi: Proje görevleri Trello’da scrum table ile takip edilmiştir.
Versiyon Kontrol: GitHub üzerinden yönetilmiş, her sprint sonunda commit yapılmıştır.
Kod Kalitesi: KISS prensiplerine uygun, kod smells minimize edilmiştir.
Dokümantasyon: Bu README dosyası ve proje video anlatımı mevcuttur.


Proje Notları ve Eksikler
Word Chain ve LLM modülü henüz tamamlanmadı, sonraki güncellemelerde eklenecek.
Resim ekleme modülü ve ses modülü tamamlandı.
Proje mobil APK yapılmadı, ekstra puan opsiyonunu kaçırdık.

İletişim
Proje hakkında soru ve görüşler için:
https://github.com/AdemSayman
https://github.com/emirhaneroll
https://github.com/tuywin (Mac kullandığı için winforms uygulamasına commit edemedi)
https://github.com/alperenartuc (Mac kullandığı için winforms uygulamasına commit edemedi)

E-posta: ademsayman5454@gmail.com
