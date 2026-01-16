using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Core;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.DistributionAndTransportation;

namespace RetailTRUI.Tests.Tests;

public class DistributionAndTransportationTests : TestBase
{
    [Fact]
    public async Task DistributionAndTransportation_CreateDistributionAndAddProduct_ShouldSucceed()
    {
        // Arrange
        Driver.SetPage(Page);
        var globalPage = new GlobalPage();
        var createDistributionPage = new CreateDistributionPage();
        string warehouse = "KAPIKULE-SANAL";
        string productCode = "103";

        // Act & Assert - Navigate to create distribution page
        await globalPage.ClickDistributionAndTransportationDropdownToggleAsync();
        await globalPage.ClickCreateDistributionAndTransportationLinkAsync();
        await createDistributionPage.VerifyCreateDistributionPageIsDisplayedAsync();

        // Fill distribution form
        await createDistributionPage.FillDistributionFormWithValidDataAsync(warehouse);
        await createDistributionPage.SaveNewRecordAsync();
        await createDistributionPage.VerifyDistributionNumberGeneratedAsync();
        await createDistributionPage.VerifyProductsFrameAsync();

        // Add product to distribution
        await createDistributionPage.AddProductToDistributionAsync(productCode);
        await createDistributionPage.VerifyProductAddedToDistributionAsync();

        // Distribution detail selection for EYK
        await createDistributionPage.DistributionDetailSelectionForEYKAsync();
        await createDistributionPage.VerifyDistributionNumberAsync();

        // Send to transportation
        await createDistributionPage.SentToTransportationAsync();
        await createDistributionPage.ConfirmTransportationProcessAsync();
        // Note: VerifyTransportationProcessSuccess has BUG: TM-3853
    }

    [Fact]
    public async Task DistributionAndTransportation_EYKFullProcess_ShouldCompleteSuccessfully()
    {
        // Arrange
        Driver.SetPage(Page);
        var globalPage = new GlobalPage();
        var createDistributionPage = new CreateDistributionPage();
        var eykWaitingPage = new EykWaitingPage();
        var createEykPage = new CreateEykPage();
        var eykListingPage = new EykListingPage();
        string warehouse = "KAPIKULE-SANAL";
        string productCode = "106";

        // Act & Assert - Step 1: Create distribution and transportation
        await globalPage.ClickDistributionAndTransportationDropdownToggleAsync();
        await globalPage.ClickCreateDistributionAndTransportationLinkAsync();
        await createDistributionPage.VerifyCreateDistributionPageIsDisplayedAsync();
        await createDistributionPage.FillDistributionFormWithValidDataAsync(warehouse);
        await createDistributionPage.SaveNewRecordAsync();
        await createDistributionPage.VerifyDistributionNumberGeneratedAsync();
        await createDistributionPage.AddProductToDistributionAsync(productCode);
        await createDistributionPage.VerifyProductAddedToDistributionAsync();
        await createDistributionPage.DistributionDetailSelectionForEYKAsync();
        await createDistributionPage.VerifyDistributionNumberAsync();
        await createDistributionPage.SentToTransportationAsync();
        await createDistributionPage.ConfirmTransportationProcessAsync();

        // Step 2: Navigate to EYK Waiting page
        await globalPage.ClickDistributionAndTransportationDropdownToggleAsync();
        await globalPage.ClickEYKWaitingPageLinkAsync();
        await eykWaitingPage.VerifyEykWaitingProcessesPageIsDisplayedAsync();

        // Step 3: Select warehouse and search for EYK waiting
        await eykWaitingPage.SelectWarehouseAndSearchForEYKWaitingAsync(warehouse);

        // Step 4: Create EYK preparation
        await eykWaitingPage.CreateEYKPreparationWorkflowAsync();

        // Step 5: Send to counting process
        await eykWaitingPage.SendToCountingProcessWorkflowAsync();

        // Step 6: Navigate to Creating EYK page
        await globalPage.ClickDistributionAndTransportationDropdownToggleAsync();
        await globalPage.ClickCreatingEYKLinkAsync();
        await createEykPage.VerifyCreatingEYKPageIsDisplayedAsync();

        // Step 7: Open EYK update for creation
        await createEykPage.OpenEykUpdateForCreationEykAsync();

        // Step 8: Save EYK no for verification
        await createEykPage.SaveEYKNoForVerifyRecordAsync();

        // Step 9: Navigate to EYK Listing page
        await globalPage.ClickDistributionAndTransportationDropdownToggleAsync();
        await globalPage.ClickEYKListingPageLinkAsync();

        // Step 10: Verify EYK is completed successfully
        await eykListingPage.VerifyEYKIsCompletedSuccessfullyAsync();
    }
}
