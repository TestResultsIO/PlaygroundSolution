using Progile.ATE.Common;
using Progile.ATE.TestFramework;
using Progile.TRIO.BaseModel;
using Progile.TRIO.EnvironmentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using static TestImages.Playground;

namespace Playground_Model
{
    public partial class PlaygroundApp : IAppBasics
    {
        public static bool versionLogged; // this needs to be static, because we want to log the version only once over all instances
        private readonly Dictionary<string, object> _testCaseDataStorage = new Dictionary<string, object>();

        public PlaygroundApp(ITester t)
        {
            Tester = t;
            SystemHelpers = new SystemHelpers(t);

            t.Debug.HintLevel = TesterHintLevels.All;
            t.Properties.DefaultOcrEngine = OcrEngines.NativeEngine;
            BaseScreen.DefaultCanScrollToFindElement = true;

            // Log version of all required models
            LogSwModelVersion();
            SystemHelpers.LogEnvironmentModelVersion();
            BaseModelHelpers.LogBaseModelVersion(t);

            // Initialize Application Settings
            AppSettings = new Dictionary<string, string>();

            // Set this to the culture of the environment that will be used
            var language = t.GetVariable("language", "en-CH");
            SutLocale = new SutLocale(t, SystemHelpers, language);

            // Set this to the size of your application or inherit from Progile.TRIO.EnvironmentModel.Window and use its ScreenSelect
            Window = new Select(SystemHelpers.FullScreen);

            // Init Screens
            InitScreens();
        }

        public ITester Tester { get; }
        public ISystemHelpers SystemHelpers { get; }
        public Select Window { get; }
        public Dictionary<string, string> AppSettings { get; }
        public ISutLocale SutLocale { get; }


        partial void InitScreens();

        public IScroller GetScroller(Select searchRectangle = null, bool requiresFocus = false)
        {
            return new DetectedVerticalScroller(this, searchRectangle);
        }

        /// <summary>
        /// Stores a variable with the specified name, so that it can be used later in the test case.
        /// </summary>
        public void Store(string name, object value)
        {
            _testCaseDataStorage[name] = value;
        }

        /// <summary>
        /// Retrieves a previously stored variable with the specified name. Replace T with the type of the variable (e.g. string)
        /// </summary>
        public T Get<T>(string name)
        {
            if (!_testCaseDataStorage.ContainsKey(name))
                return default(T);

            return (T)_testCaseDataStorage[name];
        }

        private void LogSwModelVersion()
        {
            if (!versionLogged)
            {
                var assembly = Assembly.GetExecutingAssembly();
                Tester.Log("* SW Model Name:         " + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title);
                Tester.Log("* SW Model Version:      " + BaseModelHelpers.GetVersionString(assembly.GetName().Version));
                versionLogged = true;
            }
        }
    }
}