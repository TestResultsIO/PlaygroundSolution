[TestCase(1)]
public class EndToEndTest : TestCase
{

    [TestStep(1,
        TestInput = @"Login with default user credentials")]
    public void Step1(ITester t)
    {
        App.LoginScreen.EmailAddress.Enter($@"welcome@testresults.io");
        App.LoginScreen.Password.Enter($@"HelloWorld");
        t.Testee.TakeScreenshot($@"Login credentials filled");
        App.LoginScreen.LOGIN.Click(App.LoginScreen.WaitForDisappear);
    }


    [TestStep(2,
        TestInput = @"Read confirmation code from mobile and enter on login confirmation page")]
    public void Step2(ITester t)
    {
        App.LoginConfirmation.MobileLink.Click(App.Mobile.WaitForAppear);
        t.SetVariable("confirmationCode", App.Mobile.ConfirmationCode.ReadValue());
        t.Testee.TakeScreenshot($@"Mobile Confirmation Code");
        App.Mobile.WebapplicationLink.Click(App.LoginConfirmation.WaitForAppear);
        App.LoginConfirmation.ConfirmationCode.Enter("{{confirmationCode}}");
        App.LoginConfirmation.Language.SelectValue($@"English");
        App.LoginConfirmation.RememberLanguage.Check();
        t.Testee.TakeScreenshot($@"Confirmation Page");
        App.LoginConfirmation.CONFIRM.Click(App.LoginConfirmation.WaitForDisappear);
        t.Report.PassStep($@"Login was successful");
    }


    [TestStep(3,
        TestInput = @"Open last invoice for recent user 'Demo User'")]
    public void Step3(ITester t)
    {
        App.RecentSign_InActivity.RecentSign_InActivityTable.GetRow(App.RecentSign_InActivity.RecentSign_InActivityTable.UserName, $@"Demo User")
            .GetElementInRow(App.RecentSign_InActivity.PDFIconButton)
        .Click(App.InvoicePDF.WaitForAppear);

    }


    [TestStep(4,
        TestInput = @"Verify the invoice total",
        ExpectedResults = @"The invoice total is shown as $93.50")]
    public void Step4(ITester t)
    {
        t.Report.PassFailStep(
            App.InvoicePDF.Total.VerifyValue($@"$93.50", out var actualValue),
            $@"The expected value {/*value*/ $@"$93.50"} was shown correctly for {/*element*/ App.InvoicePDF.Total}.",
            $@"The value for {/*element*/ App.InvoicePDF.Total} was {actualValue} instead of the expected value {/*value*/ $@"$93.50"}.");

    }
}
