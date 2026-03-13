# GENEL KONDISYON TESTLERI - FINAL KURALLAR TABLOSU (24 Test Case)

## KOMBINASYONLAR VE TEST DURUMLARI

### T1: Rebate Fixed Margin - Hesaplamasız - Tekli
**Actions:**
- Kondisyon Tipi: Rebate Fixed Margin
- Hedef Tipi: Hesaplamasız
- Marj Tipi: Tekli

**Verifications:**
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj: Required
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Hayır (select)
- Çoklu Ödül: (no action - disabled)

**Verifications:**
- Kademeli mi?: Enabled - Hayır selected
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Evet (select)
- Çoklu Ödül: (no action - disabled)

**Verifications:**
- Kademeli mi?: Enabled - Evet selected
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Hayır (select)
- Çoklu Ödül: (no action - disabled)

**Verifications:**
- Kademeli mi?: Enabled - Hayır selected
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Evet (select)
- Çoklu Ödül: (no action - disabled)

**Verifications:**
- Kademeli mi?: Enabled - Evet selected
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Hayır (select)
- Çoklu Ödül: Hayır (select)

**Verifications:**
- Kademeli mi?: Enabled - Hayır selected
- Çoklu Ödül: Enabled - Hayır selected
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Hayır (select)
- Çoklu Ödül: Evet (select)

**Verifications:**
- Kademeli mi?: Enabled - Hayır selected
- Çoklu Ödül: Enabled - Evet selected
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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

### T16: Rebate Sales Bonus - Satış Adedi - Kademeli:Evet - Çoklu Ödül:Enabled
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Adedi
- Kademeli mi?: Evet (select)
- Çoklu Ödül: (select)

**Verifications:**
- Kademeli mi?: Enabled - Evet selected
- Çoklu Ödül: Enabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Hayır (select)
- Çoklu Ödül: Hayır (select)

**Verifications:**
- Kademeli mi?: Enabled - Hayır selected
- Çoklu Ödül: Enabled - Hayır selected
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Hayır (select)
- Çoklu Ödül: Evet (select)

**Verifications:**
- Kademeli mi?: Enabled - Hayır selected
- Çoklu Ödül: Enabled - Evet selected
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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

### T19: Rebate Sales Bonus - Satış Cirosu - Kademeli:Evet - Çoklu Ödül:Enabled
**Actions:**
- Kondisyon Tipi: Rebate Sales Bonus
- Hedef Tipi: Satış Cirosu
- Kademeli mi?: Evet (select)
- Çoklu Ödül: (select)

**Verifications:**
- Kademeli mi?: Enabled - Evet selected
- Çoklu Ödül: Enabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Çoklu Ödül: Hayır (select)

**Verifications:**
- Kademeli mi?: Disabled
- Çoklu Ödül: Enabled - Hayır selected
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Çoklu Ödül: Evet (select)

**Verifications:**
- Kademeli mi?: Disabled
- Çoklu Ödül: Enabled - Evet selected
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj Tipi: Disabled
- Marj: Disabled
- Hesaplama Periyodu: Required
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
- Kademeli mi?: Disabled
- Çoklu Ödül: Disabled
- Marj: Required
- Hesaplama Periyodu: Required
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
| T1 | Rebate Fixed Margin | Hesaplamasız | Disabled | Disabled | Fixed Margin Tekli |
| T2 | Rebate Fixed Margin | Hesaplamasız | Disabled | Disabled | Fixed Margin Çoklu |
| T3 | Rebate Target Purchase Bonus | Alım Adeti | Disabled | Disabled | Target Purchase Adeti |
| T4 | Rebate Target Purchase Bonus | Alım Tutarı | Disabled | Disabled | Target Purchase Tutarı |
| T5 | Rebate Target Purchase Bonus | Hesaplamasız | Disabled | Disabled | Target Purchase Simple |
| T6 | Rebate Purchase Bonus | Alım Adeti | Enabled:Hayır | Disabled | Purchase Adeti Hayır |
| T7 | Rebate Purchase Bonus | Alım Adeti | Enabled:Evet | Disabled | Purchase Adeti Evet |
| T8 | Rebate Purchase Bonus | Alım Tutarı | Enabled:Hayır | Disabled | Purchase Tutarı Hayır |
| T9 | Rebate Purchase Bonus | Alım Tutarı | Enabled:Evet | Disabled | Purchase Tutarı Evet |
| T10 | Rebate Purchase Bonus | Hesaplamasız | Disabled | Disabled | Purchase Simple |
| T11 | Rebate Target Sales Bonus | Satış Adedi | Disabled | Disabled | Target Sales Adedi |
| T12 | Rebate Target Sales Bonus | Satış Cirosu | Disabled | Disabled | Target Sales Cirosu |
| T13 | Rebate Target Sales Bonus | Hesaplamasız | Disabled | Disabled | Target Sales Simple |
| T14 | Rebate Sales Bonus | Satış Adedi | Enabled:Hayır | Enabled:Hayır | Sales Adedi Hayır/Hayır |
| T15 | Rebate Sales Bonus | Satış Adedi | Enabled:Hayır | Enabled:Evet | Sales Adedi Hayır/Evet |
| T16 | Rebate Sales Bonus | Satış Adedi | Enabled:Evet | Enabled | Sales Adedi Evet |
| T17 | Rebate Sales Bonus | Satış Cirosu | Enabled:Hayır | Enabled:Hayır | Sales Ciro Hayır/Hayır |
| T18 | Rebate Sales Bonus | Satış Cirosu | Enabled:Hayır | Enabled:Evet | Sales Ciro Hayır/Evet |
| T19 | Rebate Sales Bonus | Satış Cirosu | Enabled:Evet | Enabled | Sales Ciro Evet |
| T20 | Rebate Sales Bonus | Hesaplamasız | Disabled | Enabled:Hayır | Sales Simple Hayır |
| T21 | Rebate Sales Bonus | Hesaplamasız | Disabled | Enabled:Evet | Sales Simple Evet |
| T22 | Rental Fee | Hesaplamasız | Disabled | Disabled | Rental Fee |
| T23 | Marketing Activity | Hesaplamasız | Disabled | Disabled | Marketing Activity |
| T24 | Contract Margin | Hesaplamasız | Disabled | Disabled | Contract Margin |

