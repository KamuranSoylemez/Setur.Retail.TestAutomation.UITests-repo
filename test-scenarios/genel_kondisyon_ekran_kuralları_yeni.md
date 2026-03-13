# GENEL KONDISYON TESTLERI - YENİ KURALLAR TABLOSU (24 Test Case)

## KOMBINASYONLAR VE TEST DURUMLARI

### T1: Rebate Fixed Margin - Hesaplamasız - Tekli
**Actions:**
- Kondisyon Tipi: Rebate Fixed Margin
- Hedef Tipi: Hesaplamasız
- Marj Tipi: Tekli

**Verifications:**
- Marj: Required
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Disabled
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T2: Rebate Fixed Margin - Hesaplamasız - Çoklu
**Actions:**
- Kondisyon Tipi: Rebate Fixed Margin
- Hedef Tipi: Hesaplamasız
- Marj Tipi: Çoklu

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Disabled
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T3: Rebate Target Purchase Bonus - Alım Adeti
**Actions:**
- Kondisyon Tipi: Rebate Target Purchase Bonus
- Hedef Tipi: Alım Adeti

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Required
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Enabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Required
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T4: Rebate Target Purchase Bonus - Alım Tutarı
**Actions:**
- Kondisyon Tipi: Rebate Target Purchase Bonus
- Hedef Tipi: Alım Tutarı

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Required
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T5: Rebate Target Purchase Bonus - Hesaplamasız
**Actions:**
- Kondisyon Tipi: Rebate Target Purchase Bonus
- Hedef Tipi: Hesaplamasız

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T6: Rebate Purchase Bonus - Alım Adeti - Kademeli:Hayır
**Actions:**
- Kondisyon Tipi: Rebate Purchase Bonus
- Hedef Tipi: Alım Adeti
- Kademeli: Hayır

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Hayır selected)
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Enabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Required
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T7: Rebate Purchase Bonus - Alım Adeti - Kademeli:Evet
**Actions:**
- Kondisyon Tipi: Rebate Purchase Bonus
- Hedef Tipi: Alım Adeti
- Kademeli: Evet

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Evet selected)
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T8: Rebate Purchase Bonus - Alım Tutarı - Kademeli:Hayır
**Actions:**
- Kondisyon Tipi: Rebate Purchase Bonus
- Hedef Tipi: Alım Tutarı
- Kademeli: Hayır

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Hayır selected)
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T9: Rebate Purchase Bonus - Alım Tutarı - Kademeli:Evet
**Actions:**
- Kondisyon Tipi: Rebate Purchase Bonus
- Hedef Tipi: Alım Tutarı
- Kademeli: Evet

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Evet selected)
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T10: Rebate Purchase Bonus - Hesaplamasız
**Actions:**
- Kondisyon Tipi: Rebate Purchase Bonus
- Hedef Tipi: Hesaplamasız

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Optional
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T11: Rebate Target Sales Bonus - Satış Adedi
**Actions:**
- Kondisyon Tipi: Rebate Target Sales Bonus
- Hedef Tipi: Satış Adedi

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Required
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Enabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Required
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T12: Rebate Target Sales Bonus - Satış Cirosu
**Actions:**
- Kondisyon Tipi: Rebate Target Sales Bonus
- Hedef Tipi: Satış Cirosu

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Required
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T13: Rebate Target Sales Bonus - Hesaplamasız
**Actions:**
- Kondisyon Tipi: Rebate Target Sales Bonus
- Hedef Tipi: Hesaplamasız

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T14: Rebate Sales Bonus - Satış Adedi - Kademeli:Hayır - Çoklu Ödül:Hayır
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Adedi
- Kademeli: Hayır
- Çoklu Ödül: Hayır

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Hayır selected)
- Çoklu Ödül: Enabled (Hayır selected)
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Enabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Required
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T15: Rebate Sales Bonus - Satış Adedi - Kademeli:Hayır - Çoklu Ödül:Evet
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Adedi
- Kademeli: Hayır
- Çoklu Ödül: Evet

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Hayır selected)
- Çoklu Ödül: Enabled (Evet selected)
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Enabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Required
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T16: Rebate Sales Bonus - Satış Adedi - Kademeli:Evet
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Adedi
- Kademeli: Evet
- Çoklu Ödül: Enabled

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Evet selected)
- Çoklu Ödül: Enabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T17: Rebate Sales Bonus - Satış Cirosu - Kademeli:Hayır - Çoklu Ödül:Hayır
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Cirosu
- Kademeli: Hayır
- Çoklu Ödül: Hayır

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Hayır selected)
- Çoklu Ödül: Enabled (Hayır selected)
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required (Conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (Conditional: Tutar Girilmediyse)
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

**Ek Test Notları:**
- Tutar girilirse Oran disabled, Oran girilirse Tutar disabled

---

### T18: Rebate Sales Bonus - Satış Cirosu - Kademeli:Hayır - Çoklu Ödül:Evet
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Cirosu
- Kademeli: Hayır
- Çoklu Ödül: Evet

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Hayır selected)
- Çoklu Ödül: Enabled (Evet selected)
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T19: Rebate Sales Bonus - Satış Cirosu - Kademeli:Evet
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Cirosu
- Kademeli: Evet
- Çoklu Ödül: Enabled

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Enabled (Evet selected)
- Çoklu Ödül: Enabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T20: Rebate Sales Bonus - Hesaplamasız - Çoklu Ödül:Hayır
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Hesaplamasız
- Çoklu Ödül: Hayır

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Enabled (Hayır selected)
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T21: Rebate Sales Bonus - Hesaplamasız - Çoklu Ödül:Evet
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Hesaplamasız
- Çoklu Ödül: Evet

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Enabled (Evet selected)
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T22: Rental Fee - Hesaplamasız
**Actions:**
- Kondisyon Tipi: Rental Fee
- Hedef Tipi: Hesaplamasız

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T23: Marketing Activity - Hesaplamasız
**Actions:**
- Kondisyon Tipi: Marketing Activity
- Hedef Tipi: Hesaplamasız

**Verifications:**
- Marj: Disabled
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Disabled
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Required
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

### T24: Contract Margin - Hesaplamasız - Tekli
**Actions:**
- Kondisyon Tipi: Contract Margin
- Hedef Tipi: Hesaplamasız
- Marj Tipi: Tekli

**Verifications:**
- Marj: Required
- Hesaplama Periyodu: Required
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- İşlem Para Birimi: Required
- Faturalama Para Birimi: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Tutar Kdv Dahil: Enabled
- Brüt Alım Kalem Tipi: Not Shown
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Marka: Optional
- Açıklama: Optional
- Birim Çarpanı: Disabled
- Toptan Kâr Merkezi: Enabled
- Gölge Rebate Hesaplansın mı?: Enabled

---

## ÖZET TABLO

| Test | Kondisyon Tipi | Hedef Tipi | Kademeli | Çoklu Ödül | Test Odağı |
|------|---|---|---|---|---|
| T1 | Rebate Fixed Margin | Hesaplamasız | Tekli | - | Fixed Margin Tekli |
| T2 | Rebate Fixed Margin | Hesaplamasız | Çoklu | - | Fixed Margin Çoklu |
| T3 | Rebate Target Purchase Bonus | Alım Adeti | - | - | Target Purchase Adeti |
| T4 | Rebate Target Purchase Bonus | Alım Tutarı | - | - | Target Purchase Tutarı |
| T5 | Rebate Target Purchase Bonus | Hesaplamasız | - | - | Target Purchase Simple |
| T6 | Rebate Purchase Bonus | Alım Adeti | Hayır | - | Purchase Adeti Conditional |
| T7 | Rebate Purchase Bonus | Alım Adeti | Evet | - | Purchase Adeti Kademeli |
| T8 | Rebate Purchase Bonus | Alım Tutarı | Hayır | - | Purchase Tutarı Conditional |
| T9 | Rebate Purchase Bonus | Alım Tutarı | Evet | - | Purchase Tutarı Kademeli |
| T10 | Rebate Purchase Bonus | Hesaplamasız | - | - | Purchase Simple |
| T11 | Rebate Target Sales Bonus | Satış Adedi | - | - | Target Sales Adedi |
| T12 | Rebate Target Sales Bonus | Satış Cirosu | - | - | Target Sales Cirosu |
| T13 | Rebate Target Sales Bonus | Hesaplamasız | - | - | Target Sales Simple |
| T14 | Rebate Sales Bonus | Satış Adedi | Hayır | Hayır | Sales Adedi No Multi |
| T15 | Rebate Sales Bonus | Satış Adedi | Hayır | Evet | Sales Adedi Multi |
| T16 | Rebate Sales Bonus | Satış Adedi | Evet | - | Sales Adeti Kademeli |
| T17 | Rebate Sales Bonus | Satış Cirosu | Hayır | Hayır | Sales Ciro No Multi |
| T18 | Rebate Sales Bonus | Satış Cirosu | Hayır | Evet | Sales Ciro Multi |
| T19 | Rebate Sales Bonus | Satış Cirosu | Evet | - | Sales Ciro Kademeli |
| T20 | Rebate Sales Bonus | Hesaplamasız | - | Hayır | Sales Simple No Multi |
| T21 | Rebate Sales Bonus | Hesaplamasız | - | Evet | Sales Simple Multi |
| T22 | Rental Fee | Hesaplamasız | - | - | Rental Fee |
| T23 | Marketing Activity | Hesaplamasız | - | - | Marketing Activity |
| T24 | Contract Margin | Hesaplamasız | Tekli | - | Contract Margin |

