Tüm testlerden önce ;
Sisteme login ol,
https://dfs-retail-ui-staging.azurewebsites.net/ApplicationManagement/Contract/Index sayfasını aç.


Test Case: T1 - Genel Kondisyon Güncelle Butonu Kontrolü 1
Steps:


Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Genel Kondisyon Durumu:Onaylandı olan 632 id li kondsiyon için detayı aç
Expected: Onaylandı durumundaki genel kondisyon detayı görüntülenir.
Güncelle butonunun görünürlüğünü kontrol et
Expected: “Güncelle” butonu görünür.

Test Case: T2 - Genel Kondisyon Güncelle Butonu Kontrolü 2
Steps:

Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Onaylandı durumundaki genel kondisyonun ayar butonuna tıkla
Expected: Ayar menüsü açılır.
Güncelle butonunun görünürlüğünü kontrol et
Expected: “Güncelle” butonu görünür.

Test Case: T3 - Kondisyon Güncelleme Pop-up Kontrolü ve T4 
Steps:

Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Genel Kondisyon Güncelleme ekranında “Güncelle” butonuna tıkla
Expected: Kondisyon Güncelleme pop-up açılır.

Test Case: T4 - Kondisyon Güncelleme Pop-up Olumsuz Durum
Steps:

İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Genel Kondisyon Güncelleme ekranında “Kaydet” butonuna tıkla (hiçbir seçim yapılmadan)
Expected: Güncelleme Türü ve Açıklama alanlarının zorunlu olduğu uyarısı görünür.
Expected: “Açıklama Alanı Boş Bırakılamaz.” uyarısı görüntülenir.

Test Case: T5 - Kondisyon Güncelleme Kondisyon İyileşmesi
Steps:

Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Genel Kondisyon Güncelleme ekranında “Kondisyon İyileşmesi” seç
Expected: Güncelleme türü olarak Kondisyon İyileşmesi seçilir.
Açıklama alanına metin gir
Expected: Açıklama alanı doldurulur.
“Kaydet” butonuna tıkla
Expected: Genel Kondisyon Tanımlama ekranı açılır.