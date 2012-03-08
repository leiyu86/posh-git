﻿using OpenQA.Selenium;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.Elements;

namespace Wonga.QA.Framework.UI.UiElements.Pages
{
    public class HomePage : BasePage
    {
        //public IWebElement MenuContent;
        public SlidersElement Sliders { get; set; }
        public HelpElement Help { get; set; }
        public InternationalElement InternationalElements { get; set; }
        public LoginElement Login { get; set; }
        //public TabsElement Tabs { get; set; }

        public HomePage(UiClient client)
            : base(client)
        {
            Sliders = new SlidersElement(this);
            switch (Config.AUT)
            {
                case (AUT.Ca):
                case (AUT.Za):
                    Help = new HelpElement(this);
                    InternationalElements = new InternationalElement(this);
                    Login = new LoginElement(this);
                    break;
            }
            //Tabs = new TabsElement(this);

        }
    }
}