﻿using System;
using OpenQA.Selenium;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.UiElements.Pages.Interfaces;
using Wonga.QA.Framework.UI.Mappings;

namespace Wonga.QA.Framework.UI.UiElements.Pages.Common
{
    public class AddressDetailsPage : BasePage,IApplyPage
    {
        private readonly IWebElement _postCode;
        private readonly IWebElement _form;
        private readonly IWebElement _lookup;
        private readonly IWebElement _next;
        private readonly IWebElement _flatNumber;
        private readonly IWebElement _district;
        private readonly IWebElement _county;
        private readonly IWebElement _town;
        private readonly IWebElement _street;
        private readonly IWebElement _addressPeriod;
        private IWebElement _addressOptions;

        public String PostCode { set { _postCode.SendValue(value); } }
        public String SelectedAddress { set { _addressOptions.SelectOption(value); } }
        public String FlatNumber { set { _flatNumber.SendValue(value); } }
        public String District { set { _district.SendValue(value); } }
        public String County { set { _county.SendValue(value); } }
        public String Town { set { _town.SendValue(value); } }
        public String Street { set { _street.SendValue(value); } }
        public String AddressPeriod { set { _addressPeriod.SelectOption(value); } }

        public AddressDetailsPage(UiClient client) : base(client)
        {

            _form = Content.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.FormId));
            _postCode = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.Postcode));
            _flatNumber = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.FlatNumber));
            _district = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.District));
            _county = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.County));
            _addressPeriod = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.AddressPeriod));
            _next = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.NextButton));

            switch (Config.AUT)
            {
                case(AUT.Wb):
                    _lookup = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.LookupButton));
                    break;
                case(AUT.Za):
                    _street = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.Street));
                    _town = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.Town));
                    break;
            }
        }

        public void LookupByPostCode()
        {
            _lookup.Click();
        }

        public void GetAddressesDropDown()
        {
            _addressOptions = _form.FindElement(By.CssSelector(Elements.Get.AddressDetailsPage.AddressOptions));
        }

        public AccountDetailsPage Next()
        {
            _next.Click();
            return new AccountDetailsPage(Client);
        }

    }
}