# INCENTIVE KONDISYON - WEB ELEMENT MAPPING RAPORU

## Spec'teki Tüm Alanlar ve Yıcak Test Framework Yöntemeleri

### Başlangıç ve Bitiş Tarihleri
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| Başlangıç Tarihi | Required | `FillStartDateAsync(date)` | Date picker - Kendo DatePicker |
| Bitiş Tarihi | Required | `FillEndDateAsync(date)` | Date picker -Kendo DatePicker |

### Para Birimleri
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| İşlem Para Birimi | Disabled | N/A | Tüm configurasyonlarda DISABLED |
| Faturalama Para Birimi | Required | `SelectBillingCurrencyAsync(currency)` | Kendo DropDownList |
| Hesaplama Tutar Para Birimi | Optional/Conditional | `SelectCalculationCurrencyAsync(currency)` | Kendo DropDownList |

### KDV ve Tutar Alanları
| Spec Alan Adı | Required/Optional | Test Method Name | Notler |
|---|---|---|---|
| Tutara Kdv Dahil | Required | `SelectTutaraKdvAsync(option)` | YesNo combo |
| Fatura Tutarına Kdv Dahil | Required | `SelectFaturaKdvAsync(option)` | Yes/No combo |

### Hesaplama Tipi ve Hedef Tipi
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| Hesaplama Periyodu | Required | `SelectCalculationPeriodAsync(period)` | Kendo DropDownList |
| Hedef Tipi | Configuration-based | `SelectTargetTypeAsync(type)` | Kendo DropDownList - Satış Adedi, Satış Cirosu, Hesaplamasız |
| Net/Brüt | Required/Disabled/NotShown | `SelectNetBrutAsync(option)` | (Test tarafından kontrol edilir) |

### Kademeli, Hedefli, Çoklu Ödül Kontrolleri
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| Kademeli | Required/Disabled | `SelectYesNoFieldAsync("Kademeli", bool)` | Checkbox |
| Hedefli | Required/Disabled/NotShown | `SelectYesNoFieldAsync("Hedefli", bool)` | Checkbox |
| Çoklu Ödül | Required/Optional | `SelectYesNoFieldAsync("Çoklu Ödül", bool)` | Checkbox |

### Hedef Alanları
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| Hedef Ciro | Required/Disabled/NotShown | `FillTargetNetSalesAsync(amount)` | Numeric input |
| Hedef Miktar | Required/NotShown | `FillTargetQuantityAsync(amount)` | Numeric input |
| Kişi Başı mı? | Required/Optional | `SelectYesNoFieldAsync("Kişi Başı mı?", bool)` | Checkbox |
| Maksimum Kişi Sayısı | Required/Optional | `FillMaxPersonCountAsync(count)` | Numeric input |

### Hesaplama Alanları
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| Tutar Çarpanlı | Required/Optional/Disabled | `SelectYesNoFieldAsync("Tutar Çarpanlı", bool)` | Checkbox/Yes-No field |
| Temel Ölçü Birimi | Required/Optional/Disabled | `SelectBaseMeasureUnitAsync(unit)` | Kendo DropDownList |
| Birim Çarpanı | Required/Optional/Disabled | `FillUnitMultiplierAsync(value)` | Numeric input |
| Hesaplama Tutar | Required (Conditional)/Disabled | `FillCalculationAmountAsync(amount)` | Numeric input - Mutually exclusive with Hesaplama Oran |
| Hesaplama Oran | Required (Conditional)/Disabled | `FillCalculationRateAsync(rate)` | Numeric input - Mutually exclusive with Hesaplama Tutar |

### Metadata Alanları
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| Marka | Optional | `SelectBrandAsync(brand)` | Checkbox list / Multi-select |
| Açıklama | Optional | `FillDescriptionAsync(text)` | Textarea |

### Satış Kontrol Alanları
| Spec Alan Adı | Required/Optional | Test Method Name | Notlar |
|---|---|---|---|
| Sadece Barkodlu Satışlar mı? | Required | `SelectYesNoFieldAsync("Sadece Barkodlu Satışlar mı?", bool)` | Checkbox |
| Firmaya Fatura Edilsin mi? | Required | `SelectYesNoFieldAsync("Firmaya Fatura Edilsin mi?", bool)` | Checkbox |

---

## Incentive Form Açılış Süreci

```csharp
// 1. Contract Definition page'e git
await contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();

// 2. Contract bul ve aç
await contractDefPage.FillContractNameAsync("CONTRACT_NAME");
await contractDefPage.ClickSearchButtonAsync();
await contractDefPage.ClickFirstEditButtonAsync();

// 3. Incentive tab'ına tıkla
await contractDefPage.ClickIncentiveTabAsync();

// 4. Yeni Incentive oluştur
await contractDefPage.ClickNewIncentiveButtonAsync();

// 5. Incentive formu açıldığını kontrol et
await incentiveConditionPage.VerifyFormIsDisplayedAsync();

// 6. Form alanlarını doldur
await incentiveConditionPage.SelectConditionTypeAsync("Incentive");
await incentiveConditionPage.SelectTargetTypeAsync("Satış Adedi");
await incentiveConditionPage.SelectYesNoFieldAsync("Kademeli", false);
// ... diğer alanlar
```

---

## Açılan Dropdown/Combobox'lar

### Frame Information
- Modal içindeki iframe: `data-role='window'` ve `iframe[src*='Incentive/Create']`
- Frame URL pattern: `.../Incentive/Create`

### Kendo Dropdown Elements
| Element Purpose | Possible IDs/Selectors | Status |
|---|---|---|
| Condition Type | `ContractIncentiveTypeId`, `span[aria-owns='ContractIncentiveTypeId_listbox']` | ✅ Mapped |
| Target Type | `ContractIncentiveTargetTypeId`, `span[aria-owns='ContractIncentiveTargetTypeId_listbox']` | ✅ Mapped |
| Billing Currency | `BillingCurrencyId`, `span[aria-owns='BillingCurrencyId_listbox']` | 🔍 To be discovered |
| Calculation Period | `CalculationPeriodId`, `span[aria-owns='CalculationPeriodId_listbox']` | 🔍 To be discovered |
| Base Measure Unit | `BaseMeasureUnitId`, `span[aria-owns='BaseMeasureUnitId_listbox']` | 🔍 To be discovered |

---

## Element Discovery Gerekli Bilgiler

Aşağıdaki bilgileri frame'den programatik olarak yakalamak için:

1. **Input Elements**: ID, Name, Type, Label text, Placeholder, Disabled, Required status
2. **Dropdown Elements**: ID, Class, Data-Role, Aria-Owns
3. **Checkbox Elements**: ID, Name, Associated label text

### Örnek Discovery Call:
```csharp
await incentiveConditionPage.DiscoverAllElementsAsync();
// -> Çıktı: incentive_elements_discovery.txt dosyası
```

---

## Test Validation Metodları (Spec'e göre implement edilmelT)

```csharp
// Mandatory field validation
await incentiveConditionPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");

// Optional field validation
await incentiveConditionPage.VerifyFieldIsOptionalAsync("Marka");

// Disabled field validation
await incentiveConditionPage.VerifyFieldIsDisabledAsync("İşlem Para Birimi");

// Hidden field validation
await incentiveConditionPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
```

---

**Notlar:**
- Conditional Required alanları: Kullanıcı bir alanı doldurarsa diğeri disable olacak (Tutar vs Oran)
- Kademeli seçimi True yapılırsa, bazı alanlar Disabled olacak
- Hedefli seçimi False yapılırsa, Hedef Ciro ve Hedef Miktar alanları görünmez olacak
- Çoklu Ödül seçimi yapılırsa, spesifik alanlar devre dışı kalacak

