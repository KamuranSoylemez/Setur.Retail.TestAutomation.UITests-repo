package pages.SupplierPages;

import com.microsoft.playwright.*;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;
import com.microsoft.playwright.options.SelectOption;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;


public class ContractConfirmationPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator firmCodeInput = page.locator("#FilterFirmCode");
    Locator firmNameInput = page.locator("#FilterFirmName");
    Locator contractNameInput = page.locator("#FilterContractName");
    Locator searchButton = page.locator("#FilterButtonId");
    Locator editLinks = page.locator(".gridCmdBtn.cmdLink.ContractWaitingForApprovalGridCmd");



    /**
     * Sözleşme Onay İşlemleri sayfasının açıldığını doğrular.
     */

    public void verifyContractConfirmationPageIsDisplayed() {
        verifyTextElementUseTrim("Sözleşme Onay İşlemleri", pageTitle);

    }

    /**
     * Firma kodu, firma adı ve sözleşme adı alanlarını doldurur.
     */
    public void fillFirmCode() {
        firmCodeInput.fill("1350-SWRI");
    }

    public void fillFirmName() {
        firmNameInput.fill("SWAROVSKI INTERNATIONAL");
    }

    public void fillContractName() {
        contractNameInput.fill("SWRI-2025-CF");
    }

    /**
     * Başlangıç tarihi seçici butonuna tıklar.
     */
    public void selectStartDate() {
        page.locator("#FilterStartDate").fill("01.09.2025");
    }

    /**
     * Bitiş tarihi seçici butonuna tıklar.
     */
    public void selectEndDate() {
        page.locator("#FilterEndDate").fill("31.08.2026");
    }

    /**
     * Incoterm seçer.
     */
    public void selectIncoterm() {
        page.locator("span[aria-owns='FilterIncotermsId_listbox']").click();
        page.locator("li[role='option']:has-text('CFR - Cost and Freight')").click();

    }

    /**
     * Sözleşme Durumu seçer.
     */
    public void selectContractStatus(String status) {
        page.locator("span[aria-owns='FilterContractStatusId_listbox']").click();
        // Seçeneklerin yüklenmesini bekle
        page.waitForSelector("ul#FilterContractStatusId_listbox >> li[role='option']");


        List<ElementHandle> optionElements = page.querySelectorAll("ul#FilterContractStatusId_listbox >> li[role='option']");

        List<String> optionTexts = new ArrayList<>();
        for (ElementHandle option : optionElements) {
            String text = option.innerText().trim();
            optionTexts.add(text);
        }


        for (String text : optionTexts) {
            if (text.contains(status.trim())) {
                page.locator("ul#FilterContractStatusId_listbox >> li:has-text('" + text + "')").click();
                break;
            }

        }


    }


    /**
     * Sorgula butonuna tıklar.
     */
    public void clicktoSearchButton() {
        searchButton.click();
    }

    /**
     * İlk düzenleme bağlantısına tıklar.
     */
    public void clicktoEdit() {
        editLinks.first().click();
    }


    public Frame identifyIFramesAndReturnTargetFrame() {
        // Tüm iframe'leri al
        List<Frame> frames = page.frames();

        // Her bir iframe'in URL'sini yazdır
        for (Frame frame : frames) {
            System.out.println("Iframe URL: " + frame.url());
        }

        // Hedef iframe'i bul
        Frame targetFrame = null;
        for (Frame frame : frames) {
            if (frame.url().contains("/ApplicationManagement/Contract/Edit")) {
                targetFrame = frame;
                break;
            }
        }
        return targetFrame;
    }


    /**
     * Sözleşme onayla butonunun görünür olduğunu doğrular.
     */
    public void verifyContractCancellationApproveButtonIsVisible() {
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator approveButton = targetFrame.locator("#ContractApproveCancellation");
        System.out.println("Buton sayısı: " + approveButton.count());
        System.out.println("Görünür mü: " + approveButton.isVisible());
        assert approveButton.isVisible();

    }

    /**
     * Sözleşme reddet butonunun görünür olduğunu doğrular.
     */
    public void verifyContractCancellationRejectButtonIsVisible() {
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator rejectButton = targetFrame.locator("#ContractRejectCancellation");
        assert rejectButton.isVisible();

    }

    public void verifyContractApproveButtonIsVisible() {
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator rejectButton = targetFrame.locator("#ContractReject");
        assert rejectButton.isVisible();
    }

    public void verifyContractRejectButtonIsVisible() {
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator rejectButton = targetFrame.locator("#ContractApprove");
        assert rejectButton.isVisible();
    }

    public void countButtons() {
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator buttons = targetFrame.locator("button");
        int buttonCount = buttons.count();
        System.out.println("Buton sayısı: " + buttonCount);
        assert buttonCount == 2 : "Buton sayısı beklenen değerden farklı: " + buttonCount;
    }

    public void verifyCallBackButtonIsVisible(){
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator callBackButton = targetFrame.locator("#ContractWithdraw");
        assert callBackButton.isVisible();
    }

    public void verifyContractDirectorRejectButtonIsVisible(){
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator directorRejectButton = targetFrame.locator("#ContractDirectorReject");
        assert directorRejectButton.isVisible();
    }

    public void verifyContractDirectorApproveButtonIsVisible(){
        Frame targetFrame = identifyIFramesAndReturnTargetFrame();
        Locator directorApproveButton = targetFrame.locator("#ContractDirectorApprove");
        assert directorApproveButton.isVisible();
    }



}
