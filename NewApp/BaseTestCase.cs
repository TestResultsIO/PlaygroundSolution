using Progile.ATE.TestFramework;
using TestResults.EnvironmentModel;

namespace NewApp_Model
{
    public class TestCase
    {
        protected NewAppApp NewAppApp { get; set; }

        [SetupTest]
        public virtual bool Setup(ITester t)
        {
            var envApp = new EnvironmentApp(t);
            NewAppApp = new NewAppApp(envApp);
            return true;
        }

        //[PreconditionStep(
        //    TestInput = "",
        //    ExpectedResults = "")]
        //public virtual void PreconditionStep(ITester t)
        //{ }

        //[CleanupStep(
        //    TestInput = "",
        //    ExpectedResults = "")]
        //public virtual void CleanupStep(ITester t)
        //{ }

        //[TearDownTest]
        //public virtual bool TearDown(ITester t)
        //{
        //    return true;
        //}
    }
}
