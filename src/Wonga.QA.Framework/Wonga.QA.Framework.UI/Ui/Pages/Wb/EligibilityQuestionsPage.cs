﻿using System;
using OpenQA.Selenium;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Framework.UI.UiElements.Pages.Interfaces;
using Wonga.QA.Framework.UI.Mappings;

namespace Wonga.QA.Framework.UI.UiElements.Pages.Wb
{
    public class EligibilityQuestionsPage : BasePage, IApplyPage
    {
        private readonly IWebElement _form;

        private readonly IWebElement _resident;
        private readonly IWebElement _director;
        private readonly IWebElement _activeCompany;
        private readonly IWebElement _guarantee;
        private readonly IWebElement _debitCard;
        private readonly IWebElement _next;

        public Boolean CheckResident { set { _resident.Toggle(value); } }
        public Boolean CheckDirector { set { _director.Toggle(value); } }
        public Boolean CheckActiveCompany { set { _activeCompany.Toggle(value); } }
        public Boolean CheckGuarantee
        {
            get { return _guarantee.Selected; }
            set { _guarantee.Toggle(value); }
        }
        public Boolean CheckDebitCard { set { _debitCard.Toggle(value); } }

        public EligibilityQuestionsPage(UiClient client)
            : base(client)
        {
            _form = Content.FindElement(By.CssSelector(UiMap.Get.EligibilityQuestionsPage.FormId));

            _director = _form.FindElement(By.CssSelector(UiMap.Get.EligibilityQuestionsPage.CheckDirector));
            _resident = _form.FindElement(By.CssSelector(UiMap.Get.EligibilityQuestionsPage.CheckResident));
            _activeCompany = _form.FindElement(By.CssSelector(UiMap.Get.EligibilityQuestionsPage.CheckActiveCompany));
            _guarantee = _form.FindElement(By.CssSelector(UiMap.Get.EligibilityQuestionsPage.CheckGuarantee));
            _debitCard = _form.FindElement(By.CssSelector(UiMap.Get.EligibilityQuestionsPage.CheckDebitCard));
            _next = _form.FindElement(By.CssSelector(UiMap.Get.EligibilityQuestionsPage.NextButton));
        }

        public PersonalDetailsPage Submit()
        {
            _next.Click();
            return new PersonalDetailsPage(Client);
        }
        public void ClickNextButton()
        {
            _next.Click();
        }
    }
}
