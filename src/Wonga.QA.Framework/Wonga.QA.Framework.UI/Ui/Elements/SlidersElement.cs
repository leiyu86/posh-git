﻿using System;
using OpenQA.Selenium;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.UiElements.Pages;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Framework.UI.UiElements.Pages.Interfaces;
using Wonga.QA.Framework.UI.Mappings;
using Wonga.QA.Framework.UI.UiElements.Pages.Wb;

namespace Wonga.QA.Framework.UI.Elements
{
    public class SlidersElement : BaseElement
    {
        private readonly IWebElement _form;
        private readonly IWebElement _loanAmount;
        private readonly IWebElement _loanDuration;
        private readonly IWebElement _submit;
        private readonly IWebElement _totalAmount;
        private readonly IWebElement _totalFees;
        private readonly IWebElement _totalToRepay;
        private readonly IWebElement _repaymentDate;

        public SlidersElement(BasePage page) : base(page)
        {
            _form = Page.Content.FindElement(By.CssSelector(Ui.Get.SlidersElement.FormId));
            _loanAmount = _form.FindElement(By.CssSelector(Ui.Get.SlidersElement.LoanAmount));
            _loanDuration = _form.FindElement(By.CssSelector(Ui.Get.SlidersElement.LoanDuration));
            _submit = _form.FindElement(By.CssSelector(Ui.Get.SlidersElement.SubmitButton));
            switch(Config.AUT)
            {
                case(AUT.Ca):
                case(AUT.Za):
                    _totalAmount = _form.FindElement(By.CssSelector(Ui.Get.SlidersElement.TotalAmount));
                    _totalFees = _form.FindElement(By.CssSelector(Ui.Get.SlidersElement.TotalFees));
                    _totalToRepay = _form.FindElement(By.CssSelector(Ui.Get.SlidersElement.TotalToRepay));
                    _repaymentDate = _form.FindElement(By.CssSelector(Ui.Get.SlidersElement.RepaymentDate));
                    break;
            }
        }

        public String HowMuch
        {
            get { return _loanAmount.GetValue(); }
            set { _loanAmount.SendValue(value); }
        }
        public String HowLong
        {
            get { return _loanDuration.GetValue(); }
            set { _loanDuration.SendValue(value); }
        }
        public String GetTotalAmount
        {
            get { return _totalAmount.Text; }
        }
        public String GetTotalFees
        {
            get { return _totalFees.Text; }
        }
        public String GetTotalToRepay
        {
            get { return _totalToRepay.Text; }
        }
        public String GetRepaymentDate
        {
            get { return _repaymentDate.Text; }
        }


        public IApplyPage Apply()
        {
            _submit.Click();
            if (Config.AUT == AUT.Wb)
                return new EligibilityQuestionsPage(Page.Client);
            if (Config.AUT == AUT.Uk || Config.AUT == AUT.Za || Config.AUT == AUT.Ca)
                return new PersonalDetailsPage(Page.Client);
            return null;
        }
    }
}