yeni bir test yazacağız fakat bu test daha önce kullandığımız pageleri kullanacak, fakat bu pagelerde değişiklik yaparken her adımda benden onay alacaksın
yeni bir feature açalım, adı rebateInvoiceCreate.feature olsun
önce sistemi aç her zamanki login kuralları ile gir
alacak havuzu ekranını aç
Kondisyon Hesaplama Tarihi alanına şu datayı gir:31.03.2026
Sözleşme adı alanına şu datayı gir: PMI-2026-FCA
Sorgula butonuna bas
Gelen kaydın sol tarafındaki checkbox ı tıkla; <input type="checkbox" value="213" name="ContractReceivableInvoiceGridId34bb4230084f4ab983c1f982d39cd732" onchange="Setur.Control.Grid.OnChangeGridCheckbox(event,ContractReceivableInvoiceGridIdArray, &quot;ContractReceivableInvoiceGridId34bb4230084f4ab983c1f982d39cd732&quot;);">
Rebate Faturası Oluştur butonuna bas: <input type="button" id="checkboxReceivableInvoice" name="checkboxReceivableInvoice" value="Rebate Faturası Oluştur" class="k-button" onclick="return OnCheckAndCreateRebateInvoice(&quot;/ApplicationManagement/ContractReceivableInvoice/CreateRebateInvoicePopup&quot;, t, ContractReceivableInvoiceGridIdArray, null, OnReceivableInvoiceError);">
Rebate Faturası Oluştur frame inin açıldığını doğrula.
Açılan framede bulunan Açıklama alanına 'TEST AUTOMATION' yaz.
Rebate Faturası Oluştur frame de yer alan kaydet butonuna bas.
Kaydetme işlemi sonrası ekran 'alacak Havuzu' ekranına geri dönecek.
Aşağıdaki listede aynı kayıt olacak. Bu kayıdın Fatura No sütunun altına oluşturulan fatıra atılmış olacak.
Bu fatura no bir link, ona tıkla.
Rebate Fatura Güncelleme frame i açılacak.
Bu framede bulunan Geri Çek butonua tıkla
Rebate Faturasını geri çekme nedeninizi belirtiniz: sorusu ile yeni bir pop up açılacak.BU POP!!! da bulunan text alan şudur;<input class="ajs-input" type="text" onkeypress="Setur.TextBoxUpperCaseOnKeyPress(this,event)" onpaste="Setur.TextBoxUpperCaseOnPaste(this,event)">
Bu alanı 'TEST REVERSE' olarak doldur ve yine bu pop upda bulunan ONAY a bas; <button class="ajs-button ajs-ok">Onay</button>
Ekranda İşleminiz başarıyla gerçekleştirildi. mesajının geldiğini doğrula. 
