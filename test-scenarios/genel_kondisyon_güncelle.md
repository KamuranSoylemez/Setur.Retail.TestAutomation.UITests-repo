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
Güncelle butonuna bas.
Expected: Kondisyon güncelleme pop açıldığını gör.
Kondisyon Güncelleme ekranında “Kondisyon İyileşmesi” seç
Açıklama alanına metin gir: "test otomasyon"
“Kaydet” butonuna tıkla.
Expected: Genel Kondisyon Tanımlama ekranı açılır.

Test Case: T6 - Kondisyon İyileşmesi Yukarı Yönlü Değişiklik
Steps:

Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Güncelle butonuna bas.
Expected: Kondisyon güncelleme pop açıldığını gör.
Kondisyon Güncelleme ekranında “Kondisyon İyileşmesi” seç
Açıklama alanına metin gir: "test otomasyon"
“Kaydet” butonuna tıkla.
Expected: Genel Kondisyon Tanımlama ekranı açılır.
Sayısal değer alanlarında yukarı yönlü değişiklik yap
Expected: Değerler yukarı yönlü olarak güncellenir.
Aşağı yönlü değişiklik yapmaya çalış
Expected: Aşağı yönlü değişiklik yapılamaz, sistem engeller.

Test Case: T7 - Kondisyon İyileşmesi Yeni Açılan Kondisyonun Kaydedilmesi
Steps:
 
Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Güncelle butonuna bas.
Expected: Kondisyon güncelleme pop açıldığını gör.
Kondisyon Güncelleme ekranında “Kondisyon İyileşmesi” seç
Açıklama alanına metin gir: "test otomasyon"
“Kaydet” butonuna tıkla.
Expected: Genel Kondisyon Tanımlama ekranı açılır.
Marj, Hedef Ciro, Hedef Miktar, Hesaplama Tutar, Hesaplama Oran (%), Birim Çarpanı alanlarından editable olanları bir birim arttır.
Expected: Editable olan değerler birer birim artar.
Kondisyon Tanımalama ekranında "Kaydet" butonuna tıkla.
Expected: Kaydetme işlemi başarılı Sözleşme Güncelleme ekranı açıldığını gör.
Yeni oluşan kondisyonun Genel Kondsiyon Durumunu kontrol et.
Expected: Yeni kondisyon durumu "Onay Bekleniyor" olduğunu gör.


Test Case: T8 - Kondisyon İyileşmesi Yeni Açılan Kondisyonun Onaylanması
Steps:
 
Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
Onay Bekleniyor durumundaki ilgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Onayla butonuna bas.
Expected: Onay pop-up açılır.
Açılan pop-up Onay butonuna tıkla.
Expected: Genel Kondisyon Güncelleme ekranı açılır.
Açılan ekranda Genel Kondisyon Durumu kontrol et.
Expected: Genel Kondisyon Durumu Onaylandı olmalıdır.