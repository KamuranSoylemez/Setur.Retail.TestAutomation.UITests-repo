# INCENTIVE KONDISYON TESTLERI - FINAL KURALLAR TABLOSU (11 Test Case)

## KOMBINASYONLAR VE TEST DURUMLARI

### T1: Incentive - Satış Adedi - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Adedi
- Çoklu Ödül: Hayır
- Kademeli: Hayır
- Hedefli: Evet

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Required
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Required
- Hesaplama Tutar: Required (Conditional: Tutar Girilmediyse)
- Hesaplama Oran: Required (Conditional: Oran Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Disabled
- Hedef Miktar: Required
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Required
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T2: Incentive - Satış Adedi - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Adedi
- Çoklu Ödül: Hayır
- Kademeli: Hayır
- Hedefli: Hayır

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Required
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Required
- Hesaplama Tutar: Required (Conditional: Tutar Girilmediyse)
- Hesaplama Oran: Required (Conditional: Oran Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T3: Incentive - Satış Adedi - Çoklu Ödül: Hayır, Kademeli: Evet, Hedefli: Hayır
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Adedi
- Çoklu Ödül: Hayır
- Kademeli: Evet
- Hedefli: Hayır

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Requiredx
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

### T4: Incentive - Satış Adedi - Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Adedi
- Çoklu Ödül: Evet
- Kademeli: Disabled
- Hedefli: Disabled

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Optional
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

### T5: Incentive - Satış Cirosu - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Cirosu
- Çoklu Ödül: Hayır
- Kademeli: Hayır
- Hedefli: Evet

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Required (Conditional: Tutar Girilmediyse)
- Hesaplama Oran: Required (Conditional: Oran Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Required
- Hedef Miktar: Disabled
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T6: Incentive - Satış Cirosu - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Cirosu
- Çoklu Ödül: Hayır
- Kademeli: Hayır
- Hedefli: Hayır

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Required (Conditional: Tutar Girilmediyse)
- Hesaplama Oran: Required (Conditional: Oran Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Not Shown
- Hedef Miktar: Required
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T7: Incentive - Satış Cirosu - Çoklu Ödül: Hayır, Kademeli: Evet, Hedefli: Hayır
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Cirosu
- Çoklu Ödül: Hayır
- Kademeli: Evet
- Hedefli: Hayır

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Not Shown
- Hedef Miktar: Required
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

### T8: Incentive - Satış Cirosu - Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Satış Cirosu
- Çoklu Ödül: Evet
- Kademeli: Disabled
- Hedefli: Disabled

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

### T9: Incentive - Hesaplamasız - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Hesaplamasız
- Çoklu Ödül: Hayır
- Kademeli: Hayır
- Hedefli: Hayır

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Not Shown
- Hedef Miktar: Required
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

### T10: Incentive - Hesaplamasız - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Hesaplamasız
- Çoklu Ödül: Hayır
- Kademeli: Hayır
- Hedefli: Evet

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

### T11: Incentive - Hesaplamasız - Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled
**Actions:**
- Kondisyon Tipi: Incentive
- Hedef Tipi: Hesaplamasız
- Çoklu Ödül: Evet
- Kademeli: Disabled
- Hedefli: Disabled

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Kdv'li mi: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Mandatory
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Mandatory
- Maksimum Kişi Sayısı: Optional
- Sadece Barkodlu Satışlar mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## ÖZET

İncentive kondisyonu testi 11 test case ile tanımlı:
- **Test 1-4**: Satış Adedi hedefi (4 kombinasyon)
- **Test 5-8**: Satış Cirosu hedefi (4 kombinasyon)
- **Test 9-11**: Hesaplamasız hedef (3 kombinasyon)

Her test, farklı kombinasyonları (Kademeli, Hedefli, Çoklu Ödül) test ederek tümüyle kapsamlı coverage sağlar.
