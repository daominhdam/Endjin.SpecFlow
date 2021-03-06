﻿namespace Endjin.SpecFlow.Owin.Hosting.Hooks
{
    #region Using Directives

    using System;
    using System.Configuration;
    using System.Linq;

    using Microsoft.Owin.Hosting;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class WebAppHooks
    {
        static IDisposable webApp;

        [BeforeFeature("web_app")]
        public static void FeatureSetup()
        {
            if (webApp == null)
            {
                webApp = WebApp.Start<WebStartup>(GetHostingUrl());
            }
        }

        [AfterFeature("web_app")]
        public static void FeatureTeardown()
        {
            if (webApp != null)
            {
                webApp.Dispose();
                webApp = null;
            }
        }

        [BeforeScenario("web_app")]
        public static void ScenarioSetup()
        {
            if (webApp == null)
            {
                webApp = WebApp.Start<WebStartup>(GetHostingUrl());
            }
        }

        [AfterScenario("web_app")]
        public static void ScenarioTeardown()
        {
            if (webApp != null)
            {
                webApp.Dispose();
                webApp = null;
            }
        }

        private static string GetHostingUrl()
        {
            string hostingUrl;

            if (ConfigurationManager.AppSettings.AllKeys.Contains(ConfigurationKeys.HostingUrl))
            {
                hostingUrl = ConfigurationManager.AppSettings[ConfigurationKeys.HostingUrl];
            }
            else
            {
                throw new InvalidOperationException("Could not find a Hosting Url AppSettings entry for " + ConfigurationKeys.HostingUrl);
            }

            return hostingUrl;
        }
    }
}