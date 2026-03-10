# Temsilci Kondisyon Ekran Kuralları (14 Test Senaryosu)

## TEST CONFIGURATION GUIDE

### Actions (Seçilecek/Ayarlanacak Alanlar)
- **Kondisyon Tipi**: Test başında seçilir
- **Hedef Tipi**: Test başında seçilir
- **Kademeli mi?**: "Required:Evet" ise Evet, "Required:Hayır" ise Hayır seçilir (Disabled ise seçilmez)
- **Hedefli mi?**: "Required:Evet" ise Evet, "Required:Hayır" ise Hayır seçilir (Disabled ise seçilmez)

### Verification (Kontrol Edilecek Alanlar)
- Tüm diğer alanlar status kontrol edilecek: Required, Disabled, Optional, Not Shown

---

## T1: Salary + Hesaplamasız
**Actions:**
- Kondisyon Tipi: Salary
- Hedef Tipi: Hesaplamasız
- Hedefli: Hayır (select action)

**Verifications:**
- Kademeli: Disabled
- Hedefli: Required
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T2: Bonus + Hesaplamasız
**Actions:**
- Kondisyon Tipi: Bonus
- Hedef Tipi: Hesaplamasız
- Hedefli: Hayır (select action)

**Verifications:**
- Kademeli: Disabled
- Hedefli: Required
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Required
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Required
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T3: Commission + Satış adedi + Kademeli:Hayır + Hedefli:Evet
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Satış adedi
- Kademeli: Hayır (select action)
- Hedefli: Evet (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Required
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Required
- Hesaplama Tutar: Required (conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (conditional: Tutar Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Required
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T4: Commission + Satış adedi + Kademeli:Hayır + Hedefli:Hayır
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Satış adedi
- Kademeli: Hayır (select action)
- Hedefli: Hayır (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Required
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Required
- Hesaplama Tutar: Required (conditional: Oran Girilmediyse)
- Hesaplama Oran: Required (conditional: Tutar Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T5: Commission + Satış adedi + Kademeli:Evet + Hedefli:Disabled
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Satış adedi
- Kademeli: Evet (select action)
- Hedefli: Disabled (no action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Hedefli: Disabled

---

## T6: Commission + Satış Cirosu + Kademeli:Hayır + Hedefli:Evet
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Satış Cirosu
- Kademeli: Hayır (select action)
- Hedefli: Evet (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Optional
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Required (conditional: Oran Girilmediyse)
- Hesaplama Tutar: Required (conditional: Tutar Girilmediyse)
- Hesaplama Oran: Required (conditional: Tutar Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Required
- Hedef Miktar: Disabled
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T7: Commission + Satış Cirosu + Kademeli:Hayır + Hedefli:Hayır
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Satış Cirosu
- Kademeli: Hayır (select action)
- Hedefli: Hayır (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Optional
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Required (conditional: Oran Girilmediyse)
- Hesaplama Tutar: Required (conditional: Tutar Girilmediyse)
- Hesaplama Oran: Required (conditional: Tutar Girilmediyse)
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T8: Commission + Satış Cirosu + Kademeli:Evet + Hedefli:Disabled
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Satış Cirosu
- Kademeli: Evet (select action)
- Hedefli: Disabled (no action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Optional
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Hedefli: Disabled

---

## T9: Commission + Hesaplamasız + Kademeli:Disabled + Hedefli:Hayır
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Hesaplamasız
- Kademeli: Disabled (no action)
- Hedefli: Hayır (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Optional
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Required
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Kademeli: Disabled

---

## T10: Commission + Hesaplamasız + Kademeli:Disabled + Hedefli:Evet
**Actions:**
- Kondisyon Tipi: Commission
- Hedef Tipi: Hesaplamasız
- Kademeli: Disabled (no action)
- Hedefli: Evet (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Optional
- Hesaplama Tutar Para Birimi: Disabled
- Birim Çarpanı: Required
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Required
- Maksimum kişi sayısı: Optional
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Kademeli: Disabled

---

## T11: Promotion Rental Fee + Hesaplamasız + Kademeli:Disabled + Hedefli:Hayır
**Actions:**
- Kondisyon Tipi: Promotion Rental Fee
- Hedef Tipi: Hesaplamasız
- Kademeli: Disabled (no action)
- Hedefli: Hayır (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Kademeli: Disabled

---

## T12: Promotion Rental Fee + Hesaplamasız + Kademeli:Disabled + Hedefli:Evet
**Actions:**
- Kondisyon Tipi: Promotion Rental Fee
- Hedef Tipi: Hesaplamasız
- Kademeli: Disabled (no action)
- Hedefli: Evet (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Kademeli: Disabled

---

## T13: Promotion Marketing Activity + Hesaplamasız + Kademeli:Disabled + Hedefli:Hayır
**Actions:**
- Kondisyon Tipi: Promotion Marketing Activity
- Hedef Tipi: Hesaplamasız
- Kademeli: Disabled (no action)
- Hedefli: Hayır (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Not Shown
- Hedef Miktar: Not Shown
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Kademeli: Disabled

---

## T14: Promotion Marketing Activity + Hesaplamasız + Kademeli:Disabled + Hedefli:Evet
**Actions:**
- Kondisyon Tipi: Promotion Marketing Activity
- Hedef Tipi: Hesaplamasız
- Kademeli: Disabled (no action)
- Hedefli: Evet (select action)

**Verifications:**
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T15: Salary + Hesaplamasız + Kademeli:Disabled + Hedefli:Evet
**Actions:**
- Kondisyon Tipi: Salary
- Hedef Tipi: Hesaplamasız
- Hedefli: Evet (select action)

**Verifications:**
- Kademeli: Disabled
- Hedefli: Required
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Disabled
- Tutar Çarpanlı: Disabled
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Disabled
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Optional
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required

---

## T16: Bonus + Hesaplamasız + Kademeli:Disabled + Hedefli:Evet
**Actions:**
- Kondisyon Tipi: Bonus
- Hedef Tipi: Hesaplamasız
- Hedefli: Evet (select action)

**Verifications:**
- Kademeli: Disabled
- Hedefli: Required
- Başlangıç Tarihi: Required
- Bitiş Tarihi: Required
- Hesaplama Periyodu: Required
- İşlem Para Birimi: Disabled
- Faturalama Para Birimi: Required
- Tutara Kdv Dahil: Required
- Fatura Tutarına Kdv Dahil: Required
- Temel Ölçü Birimi: Optional
- Tutar Çarpanlı: Required
- Hesaplama Tutar Para Birimi: Optional
- Birim Çarpanı: Required
- Hesaplama Tutar: Disabled
- Hesaplama Oran: Disabled
- Marka: Optional
- Açıklama: Optional
- Net/Brüt: Required
- Hedef Ciro: Disabled
- Hedef Miktar: Disabled
- Kişi Başı mı?: Disabled
- Maksimum kişi sayısı: Disabled
- Gölge Rebate Hesaplansın mı?: Required
- Firmaya Fatura Edilsin mi?: Required
- Kademeli: Disabled
