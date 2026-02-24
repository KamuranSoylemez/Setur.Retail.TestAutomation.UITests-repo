# Temsilci Maliyet İşlemleri Ekranı

**URL:** `https://dfs-retail-ui-staging.azurewebsites.net/ApplicationManagement/ContractRepresentativePayroll/Index`

**Sayfa Başlığı:** Temsilci Maliyet İşlemleri
**Credentails : staging.users.yml 

---

## Ekran Bileşenleri

### Filtre Alanları (Sol Kolon)
| Alan Adı | Tip | Varsayılan Değer |
|----------|-----|------------------|
| Firma | Text Input + Arama Butonu (🔍) | Boş |
| Faturalama PB | Dropdown | "Lütfen Seçiniz" |
| Maliyet PB | Dropdown | "Lütfen Seçiniz" |
| Sözleşme Adı | Text Input | Boş |

### Filtre Alanları (Sağ Kolon)
| Alan Adı | Tip | Varsayılan Değer |
|----------|-----|------------------|
| Maliyet Durumu | Dropdown | "Lütfen Seçiniz" |
| Temsilci Tutar PB | Dropdown | "Lütfen Seçiniz" |
| Maliyet Tarihi | Date Picker (📅) | Boş |
| Temsilci Kondisyon Tipi | Dropdown | "Lütfen Seçiniz" |

### Butonlar
| Buton | Konum | Renk |
|-------|-------|------|
| Alacak Kaydı Oluştur | Sol alt | Gri/Beyaz |
| Sorgula | Sağ alt | Mavi (🔍 Sorgula) |

### Sonuç Tablosu Kolonları
| Kolon | Açıklama |
|-------|----------|
| ☐ | Checkbox (seçim) |
| 📝 | Düzenle ikonu (yeşil) |
| ⚙️ | Ayarlar ikonu (mavi) |
| Maliyet No | Kayıt numarası |
| Sözleşme Adı | Sözleşme kodu (örn: PMI-2025-CFR) |
| Kondisyon Tipi | Salary / Incentive |
| Firma Adı | Firma tam adı |
| Maliyet Tarihi | DD.MM.YYYY formatında |
| Temsilci Tutarı | Sayısal değer (virgüllü format) |
| Temsilci Tutar PB | Para birimi (EUR, TRY vs.) |
| Maliyet Tutarı | Sayısal değer |
| Maliyet PB | Para birimi |
| Faturalama PB | Para birimi |
| Maliyet Durumu | "Alacak Oluşturuldu" / "Onaylandı" |

---

## Test Senaryoları

### T1: Temsilci Personel Ekleme
**Ön Koşul:** -
**Adımlar:**
1. Temsilci Kondisyon Güncelleme sayfasında Personel tabını aç

**Beklenen Sonuç:**
- Excel yükleme butonu görünmeli
- Excel indir butonu görünmeli

---

### T2: Excel Format İndirme
**Ön Koşul:** -
**Adımlar:**
1. Excel Yükleme butonuna tıkla
2. "Format indir" linkine tıkla

**Beklenen Sonuç:**
- Uygun excel formatı indirilebilmeli

---

### T3: Excel İle Personel Yükleme
**Ön Koşul:** -
**Adımlar:**
1. Sicil No, Setur Bölge Kodu, Tutar alanlarını doldur
2. "Select File" butonuna tıkla
3. İlgili excel dosyasını ekle

**Beklenen Sonuç:**
- Excel dosyası başarıyla yüklenmeli
- Kayıt grid alanında görünmeli

---

### T4: Excel İndirme
**Ön Koşul:** -
**Adımlar:**
1. "Excel indir" butonuna tıkla

**Beklenen Sonuç:**
- Sisteme eklenmiş kayıtlar excel formatında indirilmeli

---


### T6: Firma ile Sorgulama
**Ön Koşul:** -
**Adımlar:**
1. Firma alanını doldur
2. "Sorgula" butonuna tıkla

**Beklenen Sonuç:**
- Uygun firma bilgisi grid alanında listelenmeli

---

### T7: Fatura PB ile Sorgulama
**Ön Koşul:** -
**Adımlar:**
1. Fatura PB alanını seç
2. "Sorgula" butonuna tıkla

**Beklenen Sonuç:**
- Uygun Fatura PB bilgisi grid alanında listelenmeli

---

### T8: Maliyet Tarihi ile Sorgulama
**Ön Koşul:** -
**Adımlar:**
1. Maliyet tarihi alanını seç
2. "Sorgula" butonuna tıkla

**Beklenen Sonuç:**
- Uygun Maliyet tarihi bilgisi grid alanında listelenmeli

---

### T9: Maliyet Durumu ile Sorgulama
**Ön Koşul:** -
**Adımlar:**
1. Maliyet durumu alanını seç
2. "Sorgula" butonuna tıkla

**Beklenen Sonuç:**
- Uygun Maliyet durumu bilgisi grid alanında listelenmeli

---

### T10: Hesaplama PB ile Sorgulama
**Ön Koşul:** -
**Adımlar:**
1. Hesaplama PB alanını seç
2. "Sorgula" butonuna tıkla

**Beklenen Sonuç:**
- Uygun Hesaplama PB bilgisi grid alanında listelenmeli

---

### T11: Açıklama ile Sorgulama
**Ön Koşul:** -
**Adımlar:**
1. Açıklama alanını doldur
2. "Sorgula" butonuna tıkla

**Beklenen Sonuç:**
- Uygun açıklama bilgisi grid alanında listelenmeli

---

### T12: Personel Maliyet Excel Upload - Format İndirme
**Ön Koşul:** Kayıt "Hazırlanıyor" durumunda olmalı
**Adımlar:**
1. Listeli kayıtlardan birinin detay butonuna tıkla
2. "Personel Maliyet Excel Upload" butonuna tıkla

**Beklenen Sonuç:**
- Uygun excel dosyası indirilmeli

---

### T13: Personel Maliyet Excel Upload - Yükleme
**Ön Koşul:** -
**Adımlar:**
1. Personel Maliyet Excel Upload dosyası için Sicil No, Setur Bölge Kodu, Tutar alanlarını doldur
2. "Select File" butonuna tıkla
3. İlgili excel dosyasını ekle

**Beklenen Sonuç:**
- Excel dosyası başarıyla yüklenmeli
- Kayıt grid alanında görünmeli

---

### T14: Personel Maliyet Excel Upload - Olumsuz Senaryo
**Ön Koşul:** -
**Adımlar:**
1. Excel Upload dosyası içine uygun olmayan veriler doldur veya boş bırak
2. İlgili excel dosyasını ekle

**Beklenen Sonuç:**
- Boş veya uygun olmayan veriler için doğru hata mesajları gelmeli

---

### T15: Personel Maliyet Excel Update - Format İndirme
**Ön Koşul:** -
**Adımlar:**
1. Listeli kayıtlardan birinin detay butonuna tıkla
2. "Personel Maliyet Excel Update" butonuna tıkla

**Beklenen Sonuç:**
- Uygun excel dosyası indirilmeli

---

### T16: Personel Maliyet Excel Update - Güncelleme
**Ön Koşul:** -
**Adımlar:**
1. Kayıtlı personelin güncellenmesi için Sicil No, Setur Bölge Kodu, Tutar alanlarını doldur
2. "Select File" butonuna tıkla
3. İlgili excel dosyasını ekle

**Beklenen Sonuç:**
- Excel dosyası başarıyla yüklenmeli
- Güncellenmiş kayıt grid alanında görünmeli

---

### T17: Personel Maliyet Excel Update - Olumsuz Senaryo
**Ön Koşul:** -
**Adımlar:**
1. Excel Update dosyası içine uygun olmayan veriler doldur veya boş bırak
2. İlgili excel dosyasını ekle

**Beklenen Sonuç:**
- Boş veya uygun olmayan veriler için doğru hata mesajları gelmeli

---

### T18: Temsilci Maliyet Düzenleme
**Ön Koşul:** Kayıt "Hazırlanıyor" veya "Kategori Onayı Bekleniyor" durumunda olmalı
**Adımlar:**
1. İlgili kaydın açıklama alanını güncelle
2. "Güncelleme" butonuna tıkla

**Beklenen Sonuç:**
- Güncelleme işlemi başarıyla gerçekleşmeli

---

### T19: IK Onayına Gönderme
**Ön Koşul:** -
**Adımlar:**
1. "IK Onayına Gönder" butonuna tıkla

**Beklenen Sonuç:**
- "IK onayına göndermek istediğinize emin misiniz?" uyarı popup'ı gelmeli
- Onaylandığında işlem başarıyla kaydedilmeli
- Kayıt "IK Onayı Bekleniyor" durumuna gelmeli

---

### T20: IK Personel Maliyet Excel Update - Format İndirme
**Ön Koşul:** Kayıt IK Onayına gönderilmiş olmalı
**Adımlar:**
1. "IK Personel Maliyet Excel Update" butonuna tıkla
2. "Format indir" linkine tıkla

**Beklenen Sonuç:**
- Uygun excel formatı indirilebilmeli

---

### T21: IK Personel Maliyet Excel Update - Güncelleme
**Ön Koşul:** -
**Adımlar:**
1. Sicil No, Tutar alanlarını doldur
2. "Select File" butonuna tıkla
3. İlgili excel dosyasını ekle

**Beklenen Sonuç:**
- Excel dosyası başarıyla yüklenmeli
- Güncellenmiş kayıt grid alanında görünmeli

---

### T22: Geri Çekme İşlemi
**Ön Koşul:** Kayıt IK Onayına gönderilmiş olmalı
**Adımlar:**
1. "Geri Çek" butonuna tıkla
2. "Temsilci Maliyetini geri çekme nedeninizi belirtiniz:" popup'ında açıklama alanını doldur
3. Onay butonuna tıkla

**Beklenen Sonuç:**
- Kayıt başarıyla geri çekilmeli

---

### T23: Onay İşlemi
**Ön Koşul:** -
**Adımlar:**
1. İlgili kayıt için "Onayla" butonuna tıkla

**Beklenen Sonuç:**
- "Akışı onaylamak istediğinize emin misiniz?" uyarı popup'ı gelmeli
- Onaylandığında işlem başarıyla gerçekleşmeli
- Kayıt durumu "Onaylandı" olarak değişmeli

---

### T24: Alacak Oluşturma
**Ön Koşul:** Kayıt "Onaylandı" durumunda olmalı
**Adımlar:**
1. İlgili kaydın "Alacak Oluştur" butonuna tıkla

**Beklenen Sonuç:**
- "Alacak Oluşturmak istediğinize emin misiniz?" uyarı popup'ı gelmeli
- Onaylandığında kayıt "Alacak Oluşturuldu" durumuna gelmeli

---

### T25: Hazırlanıyor Durumundaki Kaydın İlerletilmesi - Olumsuz
**Ön Koşul:** Personel tabı boş olmalı
**Adımlar:**
1. Personel tabı boş olan bir kaydın detayını aç
2. Kaydın içeriğini kontrol et

**Beklenen Sonuç:**
- "Kategori Onayına Gönder" butonu görünmemeli

---

### T26: Kategori Onayına Gönder Butonu Görünürlüğü
**Ön Koşul:** -
**Adımlar:**
1. Herhangi bir kaydın detayını aç
2. Personel yoksa Excel Upload ile yükle

**Beklenen Sonuç:**
- "Kategori Onayına Gönder" butonu görünmeli

---

### T27: Hazırlanıyor Durumundaki Kaydın İlerletilmesi
**Ön Koşul:** -
**Adımlar:**
1. Herhangi bir kaydın detayını aç
2. Personel yoksa Excel Upload ile yükle
3. "Kategori Onayına Gönder" butonuna tıkla

**Beklenen Sonuç:**
- Kategori Onayına Gönder işlemi başarıyla gerçekleşmeli

---

### T28: Kategori Onayı Bekleniyor - Excel Upload ve Update Kontrolü
**Ön Koşul:** Kayıt "Kategori Onayı Bekleniyor" durumunda olmalı
**Adımlar:**
1. Excel Upload kontrol et
2. Excel Update kontrol et

**Beklenen Sonuç:**
- Excel Upload düzgün çalışmalı
- Excel Update düzgün çalışmalı

---

## Örnek Test Verileri

| Maliyet No | Sözleşme Adı | Kondisyon Tipi | Firma Adı | Maliyet Tarihi | Temsilci Tutarı | Maliyet Durumu |
|------------|--------------|----------------|-----------|----------------|-----------------|----------------|
| 278 | PMI-2025-CFR | Salary | PHILIP MORRIS INTERNATIONAL | 31.12.2025 | 120,00 EUR | Alacak Oluşturuldu |
| 37 | MRT-2024-EXW | Incentive | MERTRA TRADING LTD | 31.01.2024 | 2.000,00 EUR | Onaylandı |
| 192 | DPL-2024-CIP | Salary | LOREAL DE LUXE INT | 31.10.2024 | 3.318,00 EUR | Onaylandı |
| 277 | ALFADIS-2025-FCA | Incentive | ALFADIS TRADING LIMITED | 31.12.2025 | 1.200,00 EUR | Alacak Oluşturuldu |

---

## Demo Prompt

"Yukarıdaki test senaryolarını .NET xUnit ve Playwright kullanarak test otomasyonu olarak yaz. Page Object Model pattern'ini kullan. Test dosyası adı `RepresentativeCostTests.cs`, page object dosyası adı `RepresentativeCostPage.cs` olsun."

---

## Test Implementation Guidelines (CRITICAL - READ BEFORE CODING)

### 1. Test Class Pattern
```csharp
// ✅ CORRECT: Use TestBase for automatic login & browser management
public class RepresentativeCostTests : TestBase
{
    private RepresentativeCostPage? _representativeCostPage;
    
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();  // This handles login automatically
        _representativeCostPage = new RepresentativeCostPage();
    }
}

// ❌ WRONG: Don't use IAsyncLifetime directly unless you have a special case
// ❌ WRONG: Don't manually initialize LoginPage or call login methods
```

### 2. Page Context Pattern (CRITICAL!)
```csharp
[Fact]
public async Task TestMethod()
{
    // ✅ ALWAYS add these 2 lines at the START of EVERY test method
    var page = await Driver.GetPageAsync();
    Driver.SetPage(page);
    
    // Then proceed with test logic
    await _somePage!.NavigateToPageAsync();
    // ... rest of test
}

// ❌ WRONG: Missing Driver.SetPage(page) causes "Page not initialized" error
// This is due to AsyncLocal<IPage> behavior in parallel test execution
```

### 3. Page Object Pattern
```csharp
// ✅ CORRECT: Inherit from BasePage
public class RepresentativeCostPage : BasePage
{
    // Add using statements
    using Microsoft.Playwright;
    using RetailTRUI.Tests.Core;
    using RetailTRUI.Tests.Pages.Common;
    
    // Use Page property from BasePage (inherited)
    private ILocator SomeButton => Page.Locator("button#id");
}
```

### 4. Navigation Pattern
```csharp
public async Task NavigateToRepresentativeCostPageAsync()
{
    var config = ConfigurationManager.Instance;
    var url = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/ContractRepresentativePayroll/Index";
    
    await Page.GotoAsync(url, new PageGotoOptions 
    { 
        WaitUntil = WaitUntilState.DOMContentLoaded,
        Timeout = 30000
    });
    
    await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
}
```

### 5. Common Mistakes to Avoid

❌ **NEVER:**
- Create LoginPage in test class and call login methods manually
- Forget `Driver.SetPage(page)` at the start of test methods
- Use `IAsyncLifetime` directly without calling `base.InitializeAsync()`
- Call `Page.Goto()` before ensuring Page is initialized
- Forget to add `using RetailTRUI.Tests.Core;` in Page Objects

✅ **ALWAYS:**
- Inherit from `TestBase` for automatic browser & login management
- Call `await base.InitializeAsync()` first in custom InitializeAsync
- Add `Driver.SetPage(page)` at start of each test method
- Inherit Page Objects from `BasePage`
- Use `ConfigurationManager.Instance` for configuration values
- Add proper using statements in Page Objects

### 6. File Structure
```
RetailTRUI.Tests/
  Pages/
    Supplier/
      RepresentativeCostPage.cs    ← Page Object here
  Tests/
    RepresentativeCostTests.cs     ← Test class here
```

### 7. Example Complete Test Structure
```csharp
using Microsoft.Playwright;
using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;
using Xunit;
using Xunit.Abstractions;

namespace RetailTRUI.Tests.Tests;

public class RepresentativeCostTests : TestBase
{
    private readonly ITestOutputHelper _output;
    private RepresentativeCostPage? _representativeCostPage;

    public RepresentativeCostTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync(); // Handles login automatically
        _representativeCostPage = new RepresentativeCostPage();
    }

    [Fact]
    [Trait("Category", "PersonelTab")]
    [Trait("TestId", "T1")]
    public async Task T1_TestDescription()
    {
        // CRITICAL: Set Page context for async execution
        var page = await Driver.GetPageAsync();
        Driver.SetPage(page);
        
        // Now safe to use page objects
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        
        // ... test logic
    }
}
```

### 8. Why These Patterns?

**Driver.SetPage(page) is required because:**
- Tests run in parallel with `AsyncLocal<IPage>`
- Each test method runs in its own async context
- Without SetPage, the Page reference is lost between InitializeAsync and test method
- This is a known issue with xUnit + Playwright + async/await pattern

**TestBase is preferred because:**
- Automatically handles browser initialization
- Automatically logs in before each test
- Provides clean disposal after each test
- Eliminates boilerplate code

**BasePage inheritance is required because:**
- Provides thread-safe access to Page instance
- Offers helper methods for common operations
- Ensures consistent pattern across all page objects

### 9. Authorization & Navigation

**Page Navigation Pattern:**
```csharp
// ✅ CORRECT: Direct URL navigation (like other tests)
var config = ConfigurationManager.Instance;
var url = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/ContractRepresentativePayroll/Index";
await Page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

// ❌ WRONG: Using custom NavigateToPageAsync() before understanding authorization
// May cause redirect to login page if user doesn't have permission
```

**Authorization Issues:**
- If URL shows `ReturnUrl` parameter → User doesn't have access permission
- Example: `.../Login/Index?ReturnUrl=%2fApplicationManagement%2f...`
- Solution: Use correct user role in TestBase or check UserDataReader for appropriate credentials
- Some pages require specific roles (e.g., RETAIL_DIRECTOR, RETAIL_MANAGER)

**Debug Authorization:**
```csharp
// Add this to debug which page you landed on
_output.WriteLine($"Current URL: {Page.Url}");
Assert.Contains("ContractRepresentativePayroll", Page.Url); // Verify not redirected
```