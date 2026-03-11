# Brand Ambassador Field Status Matrix - All 16 Tests

**Created:** 2026-03-10  
**Legend:** R = Required | D = Disabled | O = Optional | N = Not Shown

---

## Field Status Comparison Table (Test × Alan)

| Test | Kademeli | Hedefli | Başlangıç Tarihi | Bitiş Tarihi | Hesaplama Periyodu | İşlem Para Birimi | Faturalama Para Birimi |
|---|---|---|---|---|---|---|---|
| **T1** | Disabled | Required | Required | Required | Required | Disabled | Required |
| **T2** | Disabled | Required | Required | Required | Required | Disabled | Required |
| **T3** | — | — | Required | Required | Required | Disabled | Required |
| **T4** | — | — | Required | Required | Required | Disabled | Required |
| **T5** | — | — | Required | Required | Required | Disabled | Required |
| **T6** | — | — | Required | Required | Required | Disabled | Required |
| **T7** | — | — | Required | Required | Required | Disabled | Required |
| **T8** | — | — | Required | Required | Required | Disabled | Required |
| **T9** | Disabled | — | Required | Required | Required | Disabled | Required |
| **T10** | Disabled | — | Required | Required | Required | Disabled | Required |
| **T11** | Disabled | — | Required | Required | Required | Disabled | Required |
| **T12** | Disabled | — | Required | Required | Required | Disabled | Required |
| **T13** | Disabled | — | Required | Required | Required | Disabled | Required |
| **T14** | Disabled | — | Required | Required | Required | Disabled | Required |
| **T15** | Disabled | Required | Required | Required | Required | Disabled | Required |
| **T16** | Disabled | Required | Required | Required | Required | Disabled | Required |

| Test | Tutara KDV Dahil | Fatura Tutarına KDV Dahil | Temel Ölçü Birimi | Tutar Çarpanlı | Hesaplama Tutar Para Birimi | Birim Çarpanı | Hesaplama Tutar |
|---|---|---|---|---|---|---|---|
| **T1** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T2** | Required | Required | Optional | Required | Optional | Required | Disabled |
| **T3** | Required | Required | Optional | Required | Optional | Required | Required |
| **T4** | Required | Required | Optional | Required | Optional | Required | Required |
| **T5** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T6** | Required | Required | Disabled | Disabled | Optional | Disabled | Required |
| **T7** | Required | Required | Disabled | Disabled | Optional | Disabled | Required |
| **T8** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T9** | Required | Required | Disabled | Disabled | Optional | Disabled | Required |
| **T10** | Required | Required | Disabled | Disabled | Optional | Disabled | Required |
| **T11** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T12** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T13** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T14** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T15** | Required | Required | Disabled | Disabled | Optional | Disabled | Disabled |
| **T16** | Required | Required | Optional | Required | Optional | Required | Disabled |

| Test | Hesaplama Oran | Marka | Açıklama | Net/Brüt | Hedef Ciro | Hedef Miktar | Kişi Başı mı? | Maksimum kişi sayısı |
|---|---|---|---|---|---|---|---|---|
| **T1** | Disabled | Optional | Optional | Required | Not Shown | Not Shown | Disabled | Disabled |
| **T2** | Disabled | Optional | Optional | Required | Not Shown | Not Shown | Disabled | Disabled |
| **T3** | Required | Optional | Optional | Required | Disabled | Required | Required | Optional |
| **T4** | Required | Optional | Optional | Required | Not Shown | Not Shown | Required | Optional |
| **T5** | Disabled | Optional | Optional | Required | Not Shown | Not Shown | Required | Optional |
| **T6** | Required | Optional | Optional | Required | Required | Disabled | Optional | Optional |
| **T7** | Required | Optional | Optional | Required | Not Shown | Not Shown | Required | Optional |
| **T8** | Disabled | Optional | Optional | Required | Not Shown | Not Shown | Required | Optional |
| **T9** | Disabled | Optional | Optional | Required | Not Shown | Not Shown | Required | Optional |
| **T10** | Disabled | Optional | Optional | Required | Disabled | Disabled | Required | Optional |
| **T11** | Disabled | Optional | Optional | Required | Not Shown | Not Shown | Disabled | Disabled |
| **T12** | Disabled | Optional | Optional | Required | Disabled | Disabled | Disabled | Disabled |
| **T13** | Disabled | Optional | Optional | Required | Not Shown | Not Shown | Disabled | Disabled |
| **T14** | Disabled | Optional | Optional | Required | Disabled | Disabled | Disabled | Disabled |
| **T15** | Disabled | Optional | Optional | Required | Disabled | Disabled | Disabled | Disabled |
| **T16** | Disabled | Optional | Optional | Required | Disabled | Disabled | Disabled | Disabled |

| Test | Gölge Rebate Hesaplansın mı? | Firmaya Fatura Edilsin mi? |
|---|---|---|
| **T1** | Required | Required |
| **T2** | Required | Required |
| **T3** | Required | Required |
| **T4** | Required | Required |
| **T5** | Required | Required |
| **T6** | Required | Required |
| **T7** | Required | Required |
| **T8** | Required | Required |
| **T9** | Required | Required |
| **T10** | Required | Required |
| **T11** | Required | Required |
| **T12** | Required | Required |
| **T13** | Required | Required |
| **T14** | Required | Required |
| **T15** | Required | Required |
| **T16** | Required | Required |

---

## Test Scenario Descriptions

| Test | Scenario | Kondisyon Tipi | Hedef Tipi | Seçimler |
|---|---|---|---|---|
| **T1** | Salary + Hesaplamasız | Salary | Hesaplamasız | Hedefli: Hayır |
| **T2** | Bonus + Hesaplamasız | Bonus | Hesaplamasız | Hedefli: Hayır |
| **T3** | Commission + Satış Adedi + No Gradient + With Target | Commission | Satış Adedi | Kademeli: Hayır, Hedefli: Evet |
| **T4** | Commission + Satış Adedi + No Gradient + No Target | Commission | Satış Adedi | Kademeli: Hayır, Hedefli: Hayır |
| **T5** | Commission + Satış Adedi + Gradient + Disabled | Commission | Satış Adedi | Kademeli: Evet, Hedefli: Disabled |
| **T6** | Commission + Satış Cirosu + No Gradient + With Target | Commission | Satış Cirosu | Kademeli: Hayır, Hedefli: Evet |
| **T7** | Commission + Satış Cirosu + No Gradient + No Target | Commission | Satış Cirosu | Kademeli: Hayır, Hedefli: Hayır |
| **T8** | Commission + Satış Cirosu + Gradient + Disabled | Commission | Satış Cirosu | Kademeli: Evet, Hedefli: Disabled |
| **T9** | Commission + Hesaplamasız + No Target | Commission | Hesaplamasız | Kademeli: Disabled, Hedefli: Hayır |
| **T10** | Commission + Hesaplamasız + With Target | Commission | Hesaplamasız | Kademeli: Disabled, Hedefli: Evet |
| **T11** | Promotion Rental Fee + Hesaplamasız + No Target | Promotion Rental Fee | Hesaplamasız | Kademeli: Disabled, Hedefli: Hayır |
| **T12** | Promotion Rental Fee + Hesaplamasız + With Target | Promotion Rental Fee | Hesaplamasız | Kademeli: Disabled, Hedefli: Evet |
| **T13** | Promotion Marketing + Hesaplamasız + No Target | Promotion Marketing Activity | Hesaplamasız | Kademeli: Disabled, Hedefli: Hayır |
| **T14** | Promotion Marketing + Hesaplamasız + With Target | Promotion Marketing Activity | Hesaplamasız | Kademeli: Disabled, Hedefli: Evet |
| **T15** | Salary + Hesaplamasız + With Target | Salary | Hesaplamasız | Hedefli: Evet |
| **T16** | Bonus + Hesaplamasız + With Target | Bonus | Hesaplamasız | Hedefli: Evet |

---

## Key Observations

### Always Required (Konsisten)
- Başlangıç Tarihi
- Bitiş Tarihi
- Hesaplama Periyodu
- Faturalama Para Birimi
- Tutara KDV Dahil
- Fatura Tutarına KDV Dahil
- Net/Brüt
- Gölge Rebate Hesaplansın mı?
- Firmaya Fatura Edilsin mi?

### Always Disabled
- İşlem Para Birimi

### Always Optional
- Marka
- Açıklama
- Hesaplama Tutar Para Birimi

### Variable Status (Test'e göre değişir)
- **Temel Ölçü Birimi**: T2, T16'de Optional; diğerlerde Disabled
- **Tutar Çarpanlı**: T2, T3, T4, T16'da Required; diğerlerde Disabled
- **Birim Çarpanı**: T2, T3, T4, T16'da Required; diğerlerde Disabled
- **Hesaplama Tutar**: T3, T4, T6, T7, T9, T10'da Required; diğerlerde Disabled
- **Hesaplama Oran**: T3, T4, T6, T7'de Required; diğerlerde Disabled
- **Kişi Başı mı?**: T3-8'de Required veya Optional; T1,2,11-16'da Disabled
- **Maksimum kişi sayısı**: T3-8'de Optional; T1,2,11-16'da Disabled
- **Hedef Ciro**: T6'da Required; T3,5,12,14-16'da Disabled; diğerlerde Not Shown
- **Hedef Miktar**: T3'te Required; T5,6,8,12,14-16'da Disabled; diğerlerde Not Shown

---

## Statistical Summary

| Status | Count | Percentage |
|--------|-------|-----------|
| **Required (R)** | 104 | 28.1% |
| **Disabled (D)** | 137 | 37.1% |
| **Optional (O)** | 53 | 14.3% |
| **Not Shown (N)** | 42 | 11.3% |
| **Not Applicable (--)** | 48 | 9.2% |
| **Total Entries** | 384 | 100% |

---

## Patterns by Condition Type

### Salary Tests (T1, T15)
- Kademeli: Always Disabled
- Temel Ölçü Birimi: Always Disabled
- Tutar Çarpanlı: Always Disabled
- Birim Çarpanı: Always Disabled

### Bonus Tests (T2, T16)
- Kademeli: Always Disabled
- Temel Ölçü Birimi: Always Optional
- Tutar Çarpanlı: Required (different from Salary)
- Birim Çarpanı: Required (different from Salary)

### Commission Tests (T3-10)
- More dynamic field behavior
- Gradient selection affects field status
- Target selection affects Hedef Ciro/Miktar fields
- Kişi Başı mı? and Maksimum kişi sayısı become Required/Optional (not Disabled)

### Promotion Tests (T11-14)
- Similar to Salary base structure
- Most calculation fields remain Disabled
- Kişi Başı mı? and Maksimum kişi sayısı stay Disabled (like Salary)

