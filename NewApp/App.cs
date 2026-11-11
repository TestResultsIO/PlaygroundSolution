using Progile.ATE.Common;
using Progile.ATE.TestFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using TestResults.BaseModel;
using TestResults.EnvironmentModel;
using static TestImages.NewApp;

namespace NewApp_Model
{
    public partial class NewAppApp : IAppBasics
    {
        public NewAppApp(IAppBasics appBasics)
        {
            var t = Tester = appBasics.Tester;
            SystemHelpers = appBasics.SystemHelpers;

            t.Debug.HintLevel = TesterHintLevels.All;

            LogSwModelVersion();

            // Set this to the culture of the environment that will be used
            var language = t.GetVariable("language", "en-CH");
            SutLocale = new SutLocale(t, SystemHelpers, language);

            // Set this to the size of your application or inherit from TestResults.EnvironmentModel.Window and use its ScreenSelect
            Window = new Select(SystemHelpers.UsableScreen);

            InitScreens();
        }

        public ITester Tester { get; }
        public ISystemHelpers SystemHelpers { get; }
        public Select Window { get; }
        public ISutLocale SutLocale { get; }
        public ModelDefaultRegistry ModelDefaults { get; } = new ModelDefaultRegistry();

        partial void InitScreens();

        public IScroller GetScroller(Select searchRectangle = null)
        {
            // The DetectedVerticalScroller will be automatically detected at the right edge of the searchRectangle.
            // If the scroller in your application is not close to the right edge of the screens or is not detected for other reasons,
            // you can define an IScroller by either:
            //
            // A. In case of a Windows application
            // return WindowsScroller.GetScroller(Tester, searchRectangle ?? Window);
            //
            // B. Use the GenericScroller with the required images
            // return new GenericScroller(this, Images.MyScroller.UpArrow, Images.MyScroller.DownArrow, 
            //    Images.MyScroller.AtTop, Images.MyScroller.AtBottom, searchRectangle ?? Window);
            //
            // C. Use the KeyboardScroller (scrolls with PgUp, PgDown, Home and End keys) or the MouseWheelScroller
            // with an image of the scroller thumb (to detect if a scroller is present):
            // return new MouseWheelScroller(this, Images.MyScroller.Thumb, searchRectangle ?? Window);
            // return new KeyboardScroller(this, Images.MyScroller.Thumb, searchRectangle ?? Window);
            //
            // D. Use your own IScroller implementation 
            //
            // or return null if no scroller is used

            return new DetectedVerticalScroller(this, searchRectangle ?? Window);
        }

        private void LogSwModelVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            Tester.Log("* SW Model Name:         " + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title);
            Tester.Log("* SW Model Version:      " + BaseModelHelpers.GetVersionString(assembly.GetName().Version));
        }
    }
}