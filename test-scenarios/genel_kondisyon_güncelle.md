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

Test Case: T3 - Kondisyon Güncelleme Pop-up Kontrolü
Steps:

Sözleşme ara PMI-2026-FCA
Expected: Sözleşme listesinde ilgili sözleşme görüntülenir.
İlgili sözleşme detayını aç
Expected: Detay ekranı açılır.
Genel Kondisyon Güncelleme ekranında “Güncelle” butonuna tıkla
Expected: Kondisyon Güncelleme pop-up açılır.