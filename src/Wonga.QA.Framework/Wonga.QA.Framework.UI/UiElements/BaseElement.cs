﻿using OpenQA.Selenium;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.UiElements.Pages;

namespace Wonga.QA.Framework.UI.UiElements
{
    public abstract class BaseElement
    {
        public BasePage Page;
        public IWebElement MenuContent;

        protected BaseElement(BasePage page)
        {
            Page = page;
            MenuContent = Do.Until(() => Page.Client.Driver.FindElement(By.ClassName("menu")));
        }
    }
}