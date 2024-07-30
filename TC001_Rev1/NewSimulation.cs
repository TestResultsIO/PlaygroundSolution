using NewSimulation_Model.Screens;

[TestCase(1)]
public class NewSimulation : TestCase
{

    [TestStep(1)]
    public void Step1(ITester t)
    {
        App.LoginScreen.EmailAddress.Enter($@"welcome@testresults.io");
        App.LoginScreen.Password.Enter($@"HelloWorld");
        App.LoginScreen.LOGIN.Click(App.LoginScreen.WaitForDisappear);
    }


    [TestStep(2)]
    public void Step2(ITester t)
    {
        App.LoginConfirmation.MobileLink.Click(App.Mobile.WaitForAppear);
        t.SetVariable("confirmationCode", App.Mobile.ConfirmationCode.ReadValue());
        App.Mobile.WebapplicationLink.Click(App.LoginConfirmation.WaitForAppear);
        App.LoginConfirmation.ConfirmationCode.Enter("{{confirmationCode}}");
        App.LoginConfirmation.Language.SelectValue($@"English");
        App.LoginConfirmation.CONFIRM.Click(App.LoginConfirmation.WaitForDisappear);
    }


    [TestStep(3)]
    public void Step3(ITester t)
    {
        App.RecentSign_InActivity.RecentSign_InActivityTable.GetRow(App.RecentSign_InActivity.RecentSign_InActivityTable.UserName, $@"Demo User")
            .GetElementInRow(App.RecentSign_InActivity.PDFIconButton)
        .Click(App.InvoicePDF.WaitForAppear);

    }


    [TestStep(4)]
    public void Step4(ITester t)
    {
        t.Report.PassFailStep(
            App.InvoicePDF.Total.VerifyValue($@"$93.50", out var actualValue),
            $@"The expected value {/*value*/ $@"$93.50"} was shown correctly for {/*element*/ App.InvoicePDF.Total}.",
            $@"The value for {/*element*/ App.InvoicePDF.Total} was {actualValue} instead of the expected value {/*value*/ $@"$93.50"}.");

    }










}
