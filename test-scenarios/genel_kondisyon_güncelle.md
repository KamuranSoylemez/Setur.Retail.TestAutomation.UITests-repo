Tüm testlerden önce ;
Sisteme login ol,
https://dfs-retail-ui-staging.azurewebsites.net/ApplicationManagement/Contract/Index sayfasını aç.


Test Case: T1 - Genel Kondisyon Güncelle Butonu Kontrolü 1
Steps:


Sözleşme ara PMI-2026-DAP
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Genel Kondisyon Durumu:Onaylandı olan kondisyon için detayı aç
Expected: Onaylandı durumundaki genel kondisyon detayı görüntülenir.
Güncelle butonunun görünürlüğünü kontrol et
Expected: “Güncelle” butonu görünür.

Test Case: T2 - Genel Kondisyon Güncelle Butonu Kontrolü 2
Steps:

Sözleşme ara PMI-2026-DAP
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Onaylandı durumundaki genel kondisyonun ayar butonuna tıkla
Expected: Ayar menüsü açılır.
Güncelle butonunun görünürlüğünü kontrol et
Expected: “Güncelle” butonu görünür.

Test Case: T3 - Kondisyon Güncelleme Pop-up Kontrolü ve T4 
Steps:

Sözleşme ara PMI-2026-DAP
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

Sözleşme ara PMI-2026-DAP
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

Sözleşme ara PMI-2026-DAP
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Güncelle butonuna bas.
Expected: Kondisyon güncelleme pop açıldığını gör.
Kondisyon Güncelleme ekranında “Kondisyon İyileşmesi” seç
Açıklama alanına metin gir: "test otomasyon kondisyon iyileşme"
“Kaydet” butonuna tıkla.
Expected: Genel Kondisyon Tanımlama ekranı açılır.
Birim çarpanı değerini 1 azalt.
Expected: Aşağı yönlü değişiklik yapılamaz, sistem engeller.
Birim çarpanı değerini 1 artır.
Kaydet butonuna tıkla.
Expected: İşleminiz başarıyla gerçekleştirildi mesajı gör.



Test Case: T7 - Kondisyon İyileşmesi Yeni Açılan Kondisyonun Kaydedilmesi
Steps:
 
Sözleşme ara PMI-2026-DAP
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
T6 ile oluşturulan kaydın genel kondisyon durumu kontrol edilir.
Expected: Yeni kondisyon durumu "Onay Bekleniyor" olduğunu görülür.


Test Case: T8 - Kondisyon İyileşmesi Yeni Açılan Kondisyonun Onaylanması
Steps:
 
Sözleşme ara PMI-2026-DAP
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
Onay Bekleniyor durumundaki ilgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Onayla butonuna bas.
Expected: Onay pop-up açılır.
Açılan pop-up Onay butonuna tıkla.
Expected: Genel Kondisyon Güncelleme ekranı açılır.
Açılan ekranda Genel Kondisyon Durumu kontrol et.
Expected: Genel Kondisyon Durumu Onaylandı olmalıdır.


Test Case: T9 - Kondisyon İyileşmesi Yeni Açılan Kondisyonun Red Edilmesi
Atla

Test Case: T10 - Kondisyon İyileşmesi Kondisyon Tarihçesi
Steps:
 
Sözleşme ara PMI-2026-FCDAPA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
Onaylandı durumundaki genel kondisyonun ayar butonuna tıkla
İlgili sözleşme tarihçesini aç
Expected: Tarihçe ekranı açılır
Kondisyon tarihçesi ekranında "Açıklama" alanlarını kontrol et.
Expected: Açıklama içinde "Kondisyon iyileştirme" metninin geçtiğini doğrula, Açıklama ve Kaynak kondisyon Id alanlarının doğruuğunu kontrol et.



