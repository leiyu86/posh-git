﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Wonga.QA.Framework.UI.UiElements.Pages;
using Wonga.QA.Framework.UI.Mappings;

namespace Wonga.QA.Framework.UI.UiElements.Sections
{
    public class DebitCardSection : BaseSection
    {
        private readonly IWebElement _cardType;
        private readonly IWebElement _cardNumber;
        private readonly IWebElement _cardName;
        private readonly IWebElement _cardExpiryDateMonth;
        private readonly IWebElement _cardExpiryDateYear;
        private readonly IWebElement _cardStartDateMonth;
        private readonly IWebElement _cardStartDateYear;
        private readonly IWebElement _cardSecurity;

        public String CardType { set { _cardType.SelectOption(value); } }
        public String CardNumber { set { _cardNumber.SendValue(value); } }
        public String CardName { set { _cardName.SendValue(value); } }
        public String CardSecurity { set { _cardSecurity.SendValue(value); } }
        public String ExpiryDate
        {
            set
            {
                var date = value.Split('/');
                _cardExpiryDateMonth.SelectOption(date[0]);
                _cardExpiryDateYear.SelectOption(date[1]);
            }
        }
        public String StartDate
        {
            set
            {
                var date = value.Split('/');
                _cardStartDateMonth.SelectOption(date[0]);
                _cardStartDateYear.SelectOption(date[1]);
            }
        }

        public DebitCardSection(BasePage page) : base(Elements.Get.DebitCardElement.Legend, page)
        {
            _cardType = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardType));
            _cardNumber = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardNumber));
            _cardName = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardName));
            _cardExpiryDateMonth = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardExpiryDateMonth));
            _cardExpiryDateYear = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardExpiryDateYear));
            _cardStartDateMonth = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardStartDateMonth));
            _cardStartDateYear = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardStartDateYear));
            _cardSecurity = Section.FindElement(By.Name(Elements.Get.DebitCardElement.CardSecurityNumber));
        }
    }
}