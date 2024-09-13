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









}
