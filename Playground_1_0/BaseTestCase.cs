using Progile.ATE.TestFramework;

namespace Playground_Model
{
    public class TestCase
    {
        protected PlaygroundApp App { get; set; }

        [SetupTest]
        public virtual bool Setup(ITester t)
        {
            App = new PlaygroundApp(t);
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
