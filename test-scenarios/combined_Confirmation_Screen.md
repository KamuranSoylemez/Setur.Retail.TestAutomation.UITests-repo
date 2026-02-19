# ContractConfirmation Testlerinin Yeni Ekrana Taşınması

## Özet
ContractConfirmation ekranı, Condition (Koşul) onaylarıyla birleştirilmiş ve tek bir ApprovalOperations ekranına taşınmıştır. Mevcut testlerin bu yeni ekrana uyarlanması gerekmektedir.

---

## 🎯 Bağlam ve Ürün Değişikliği

### Eski Durum
- ContractConfirmation: Ayrı bir ekran
- Test Suite: Bağımsız test sınıfları

### Yeni Durum
- **Birleşik Ekran**: ApprovalOperations
- **Yeni URL**: `https://dfs-retail-ui-staging.azurewebsites.net/ApplicationManagement/ApprovalOperations/Index`
- **Test Suite**: `SupplierCombinedApprovalScreenTests` altında konsolidasyon

---

## 📋 Yapılacaklar

### 1. Mevcut Testleri Analiz Et
- [ ] `ContractConfirmationManagerTests.cs` ve `ContractConfirmationDirectorTests.cs` içeriğini incele
- [ ] Hangi test senaryolarının yeni ekrana taşınacağını belirle
- [ ] Eski ekrana özel selector/URL referanslarını kayıt et

### 2. Page Object Katmanını Güncelle
- [ ] **Yeni Page Class**: `ApprovalOperationsPage.cs` (veya mevcut isimlendirme)
- [ ] **Yeni URL**: Constructor'da güncelle
- [ ] **Document Type Selector**: "Belge Tipi" dropdown'ı modele ekle
  ```csharp
  public async Task SelectDocumentType(string documentType)
  ```
- [ ] Mevcut Contract approval selector'larını yeni yapıya uyarla

### 3. Test Senaryolarını Taşı
- [ ] Tüm Contract approval testlerini `SupplierCombinedApprovalScreenTests.cs` içine taşı
- [ ] Test method'larını anlamlı şekilde grupla ve yeniden adlandır

### 4. Test Akışı Düzeltmeleri
- **Document Type Filtresi**: Her test sayfaya gittiğinde "Sözleşme (Contract)" seçilmiş olacak
  ```csharp
  await page.SelectDocumentType("Sözleşme"); // veya projedeki eşdeğeri
  ```
- **Approval Workflow'u Test Et**: Onayla/Reddet/Beklemede durumlarını doğrula
- **Condition Onaylarını Doğrula**: Aynı ekranda Condition onaylarının da görüntülenip çalıştığını test et

---

## ✅ Assertion Kontrol Listesi

- [ ] Belge Tipi dropdown'ı görünüyor ve "Sözleşme" seçimi yapılabiliyor
- [ ] Sözleşme tipinde belgeler listeleniyor
- [ ] Onayla/Reddet/Bekliyor aksiyonları görünüyor
- [ ] Aksiyonlara tıklama sonrası doğru state değişimi oluyor
- [ ] Condition onayları aynı ekranda beklenen bölümlerde görünüyor
- [ ] Form submit/action işlemleri başarılı

---

## 🛠️ Kod Kalitesi Kontrolleri

- [ ] **Test Isimlendirmesi**: Açıklayıcı ve BDD tarzında (örn: `WhenContractIsSelectedThenApprovalActionsAreVisible`)
- [ ] **Helper Metotlar**: Tekrarlanan adımları `TestBase` veya helper sınıflara taşı
- [ ] **Selector Stratejisi**: 
  - Tercih sırası: `data-test-id` > `data-qa` > `aria-label` > `class` (son çare)
  - Tüm selector'ları sabit (`const`) olarak tanımla
- [ ] **Waits**: Explicit wait'ler kullan, random sleep kaçın
- [ ] **Kod Tasnifi**: Arrange → Act → Assert (AAA pattern)

---

## 🚀 Teslimat Çıktıları

### Dosyalar
1. **Test Sınıfı**: `SupplierCombinedApprovalScreenTests.cs`
   - Tüm ContractConfirmation testleri konsolide
   - Yeni Condition onay senaryoları eklenmiş

2. **Page Object**: `ApprovalOperationsPage.cs`
   - Yeni URL ve Document Type dropdown selector'ları
   - Contract approval metotları
   - Condition onay metotları

3. **Eski Testler**: 
   - `ContractConfirmationManagerTests.cs` → **AKTIF** (legacy, referans için tutuldu)
   - `ContractConfirmationDirectorTests.cs` → **AKTIF** (legacy, referans için tutuldu)

### Yazılacak Testler
- T1 ManagerApproval_WithCancellationStatus_ShouldShowLimitedButtons
Aksiyon;
    Belge Tipi:Sözleşme
    işlem Tipi : Sözleşme
    Firma Kodu: SWRI
    Firma Adı: SWAROVSKI INTERNATIONAL
    Sözleşme Adı: SWRI-2025-CFR
    Durumu: İptal Onayı Bekleniyor

Filtreler yukarıdaki şekilde seçilir.
Sorgula butonuna basılır.Gelen kayıt için "detay" butonuna basılır(yeşil)

Beklenen Sonuuç:

Sözleşme Güncelleme Ekranının açıldığı ve ekranda 
"Güncelle" 
"Kapat" 
görülür.


- T2
- T3
- T4

---

## ⚠️ Önemli Notlar

- **Document Type Zorunluluğu**: Test başlamadan önce "Sözleşme" seçilmiş olmalı
- **Eski Ekran**: Artık kullanılmıyor (referans almayın)
- **URL Değişikliği**: Eski ContractConfirmation URL'i proje kodundan temizle
- **Backward Compatibility**: Condition testleri etkilenebilir → `GeneralConditionTests` kontrolü gerekebilir

---

## 📌 Varsayımlar
- Proje yapısı `Pages/` ve `Tests/` dizinlerini kullanıyor
- xUnit test framework'ü kullanılıyor
- Page Object Pattern mevcuttur
- Staging ortamı stabil ve erişilebilir
