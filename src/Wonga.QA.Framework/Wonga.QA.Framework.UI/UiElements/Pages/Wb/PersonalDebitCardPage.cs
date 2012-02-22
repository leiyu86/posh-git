﻿using System;
using OpenQA.Selenium;
using Wonga.QA.Framework.UI.Mappings;

namespace Wonga.QA.Framework.UI.UiElements.Pages.Wb
{
    public class PersonalDebitCardPage : BasePage
    {
        private readonly IWebElement _next;
        private readonly IWebElement _form;

        public Sections.DebitCardSection DebitCardSection { get; set; }
        public Sections.MobilePinVerificationSection MobilePinVerification { get; set; }

        public PersonalDebitCardPage(UiClient client) : base(client)
        {
            _form = Content.FindElement(By.CssSelector(Elements.Get.PersonalDebitCardDetailsPage.FormId));
            _next = _form.FindElement(By.CssSelector(Elements.Get.PersonalDebitCardDetailsPage.NextButton));
            MobilePinVerification = new Sections.MobilePinVerificationSection(this);
            DebitCardSection = new Sections.DebitCardSection(this);
        }

        public Wb.BusinessDetailsPage Next()
        {
            _next.Click();
            return new Wb.BusinessDetailsPage(Client);
        }
    }
}