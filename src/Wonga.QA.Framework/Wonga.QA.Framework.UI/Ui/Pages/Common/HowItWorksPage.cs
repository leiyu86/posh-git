﻿using System;
using OpenQA.Selenium;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.Elements;
using Wonga.QA.Framework.UI.UiElements.Pages;
using Wonga.QA.Framework.UI.UiElements.Pages.Interfaces;
using Wonga.QA.Framework.UI.Mappings;

namespace Wonga.QA.Framework.UI.UiElements.Pages.Common
{
    public class HowItWorksPage : BasePage
    {
        public SlidersElement Sliders { get; set; }

        public HowItWorksPage(UiClient client)
            : base(client)
        {
            Sliders = new SlidersElement(this);
        }

        public PersonalDetailsPage ApplyForLoan(int amount, int duration)
        {

            Sliders.HowMuch = amount.ToString();
            Sliders.HowLong = duration.ToString();
            return Sliders.Apply() as PersonalDetailsPage;
        }

        public string GetTextFromPage
        {
            get { return Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.HowItWorksText)).Text; }
        }

        public string GetTransparencyHeader
        {
            get
            {
                Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.Transparency)).Click();
                return Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.TransparencyHeader)).Text;
            }
        }

        public string GetTransparencyText
        {
            get
            {
                Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.Transparency)).Click();
                return Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.TransparencyText)).Text;
            }
        }

        public string GetPersonalGuaranteeHeader
        {
            get
            {
                Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.PersonalGuarantee)).Click();
                return Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.PersonalGuaranteeHeader)).Text;
            }
        }

        public string GetPersonalGuaranteeText
        {
            get
            {
                Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.PersonalGuarantee)).Click();
                return Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.PersonalGuaranteeText)).Text;
            }
        }

        public string GetFailureToStickHeader
        {
            get
            {
                Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.FailureToStick)).Click();
                return Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.FailureToStickHeader)).Text;
            }
        }

        public string GetFailureToStickText
        {
            get
            {
                Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.FailureToStick)).Click();
                return Client.Driver.FindElement(By.CssSelector(Ui.Get.HowItWorksPage.FailureToStickText)).Text;
            }
        }
    }
}
