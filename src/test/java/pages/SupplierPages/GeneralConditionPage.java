package pages.SupplierPages;
import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import pages.commonPages.BasePage;


public class GeneralConditionPage extends BasePage {

    Locator descInput = page.locator("#Description");
    Locator brandInput = page.locator("div.k-widget.k-multiselect input.k-input.k-readonly");

    public void fillOutTheGeneralConditionFormAndSave(String brand, String desc) {
        // Açıklama alanını doldurur.
        descInput.fill(desc);
        // Marka alanını doldurur.
        brandInput.click();
        Locator brandOption = page.locator("li.k-item", new Page.LocatorOptions().setHasText(brand));
        brandOption.click();
    }

    public void clickToSaveButton() {
        // Kaydet butonuna tıklar.
        page.waitForSelector("k-content-frame");
        FrameLocator iframe = page.frameLocator("iframe[title='Setur']");
        iframe.locator("#SaveBtn").click();
    }

    // TODO: Following methods need to be implemented (from TODO list)
    public void selectConditionType(String conditionType) {
        throw new UnsupportedOperationException("TODO: selectConditionType not implemented yet");
    }

    public void selectMarginType(String marginType) {
        throw new UnsupportedOperationException("TODO: selectMarginType not implemented yet");
    }

    public void selectCalculationType(String calculationType) {
        throw new UnsupportedOperationException("TODO: selectCalculationType not implemented yet");
    }

    public boolean verifyFieldIsDisabled(String fieldName) {
        throw new UnsupportedOperationException("TODO: verifyFieldIsDisabled not implemented yet");
    }

    public boolean verifyFieldIsOptional(String fieldName) {
        throw new UnsupportedOperationException("TODO: verifyFieldIsOptional not implemented yet");
    }

    public boolean verifyFieldIsMandatory(String fieldName) {
        throw new UnsupportedOperationException("TODO: verifyFieldIsMandatory not implemented yet");
    }

    public boolean verifyFieldHasRequiredAsterisk(String fieldName) {
        throw new UnsupportedOperationException("TODO: verifyFieldHasRequiredAsterisk not implemented yet");
    }

    public void fillField(String fieldName, String value) {
        throw new UnsupportedOperationException("TODO: fillField not implemented yet");
    }

    public void clearField(String fieldName) {
        throw new UnsupportedOperationException("TODO: clearField not implemented yet");
    }

    public void selectRadioButton(String fieldName, String option) {
        throw new UnsupportedOperationException("TODO: selectRadioButton not implemented yet");
    }

    public boolean verifyFieldShowsValidationErrorOnSave(String fieldName) {
        throw new UnsupportedOperationException("TODO: verifyFieldShowsValidationErrorOnSave not implemented yet");
    }

    public boolean verifyFieldIsVisuallyDisabled(String fieldName) {
        throw new UnsupportedOperationException("TODO: verifyFieldIsVisuallyDisabled not implemented yet");
    }

    public boolean verifyFieldIsNotDisabled(String fieldName) {
        throw new UnsupportedOperationException("TODO: verifyFieldIsNotDisabled not implemented yet");
    }

}
