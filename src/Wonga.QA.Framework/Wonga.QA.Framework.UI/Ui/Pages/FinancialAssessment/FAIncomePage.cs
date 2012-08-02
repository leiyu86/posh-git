﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.Mappings;
using Wonga.QA.Framework.UI.Ui.Validators;
using Wonga.QA.Framework.UI.UiElements.Pages;

namespace Wonga.QA.Framework.UI.UiElements.Pages.FinancialAssessment
{
    public class FAIncomePage : BasePage
    {
        private IWebElement _salaryAfterTax;
        private IWebElement _partnerSalaryAfterTax;
        private IWebElement _jobseekerAllowance;
        private IWebElement _incomeSupport;
        private IWebElement _workingTaxCredit;
        private IWebElement _childTaxCredit;
        private IWebElement _statePension;
        private IWebElement _privateOrWorkPension;
        private IWebElement _pensionCredit;
        private IWebElement _other;
        private IWebElement _maintenenceOrChildSupport;
        private IWebElement _incomeFromBoardersOrLodgers;
        private IWebElement _studentLoansOrGrants;
        private IWebElement _otherIncome;
        private readonly IWebElement _totalIncome;
        private readonly IWebElement _buttonPrevious;
        private readonly IWebElement _buttonNext;

        public FAIncomePage(UiClient client, Validator validator = null)
            : base(client, validator)
        {
            _salaryAfterTax = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.SalaryAfterTax));
            _partnerSalaryAfterTax = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.PartnerSalaryAfterTax));
            _jobseekerAllowance = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.JobseekerAllowance));
            _incomeSupport = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.IncomeSupport));
            _workingTaxCredit = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.WorkingTaxCredit));
            _childTaxCredit = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.ChildTaxCredit));
            _statePension = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.StatePension));
            _privateOrWorkPension = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.PrivateOrWorkPension));
            _pensionCredit = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.PensionCredit));
            _other = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.Other));
            _maintenenceOrChildSupport = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.MaintenenceOrChildSupport));
            _incomeFromBoardersOrLodgers = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.IncomeFromBoardersOrLodgers));
            _studentLoansOrGrants = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.StudentLoansOrGrants));
            _otherIncome = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.OtherIncome));
            _totalIncome = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.TotalIncome));
            _buttonPrevious = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.ButtonPrevious));
            _buttonNext = Content.FindElement(By.CssSelector(UiMap.Get.FinancialAssessmentIncomePage.ButtonNext));
        }

        public string SalaryAfterTax
        {
            get { return _salaryAfterTax.GetValue(); }
            set
            {
                _salaryAfterTax.Clear();
                _salaryAfterTax.SendKeys(value);
            }
        }

        public string PartnerSalaryAfterTax
        {
            get { return _partnerSalaryAfterTax.GetValue(); }
            set
            {
                _partnerSalaryAfterTax.Clear();
                _partnerSalaryAfterTax.SendKeys(value);
            }
        }

        public string JobseekerAllowance
        {
            get { return _jobseekerAllowance.GetValue(); }
            set
            {
                _jobseekerAllowance.Clear();
                _jobseekerAllowance.SendKeys(value);
            }
        }

        public string IncomeSupport
        {
            get { return _incomeSupport.GetValue(); }
            set
            {
                _incomeSupport.Clear();
                _incomeSupport.SendKeys(value);
            }
        }

        public string WorkingTaxCredit
        {
            get { return _workingTaxCredit.GetValue(); }
            set
            {
                _workingTaxCredit.Clear();
                _workingTaxCredit.SendKeys(value);
            }
        }

        public string ChildTaxCredit
        {
            get { return _childTaxCredit.GetValue(); }
            set
            {
                _childTaxCredit.Clear();
                _childTaxCredit.SendKeys(value);
            }
        }

        public string StatePension
        {
            get { return _statePension.GetValue(); }
            set
            {
                _statePension.Clear();
                _statePension.SendKeys(value);
            }
        }

        public string PrivateOrWorkPension
        {
            get { return _privateOrWorkPension.GetValue(); }
            set
            {
                _privateOrWorkPension.Clear();
                _privateOrWorkPension.SendKeys(value);
            }
        }

        public string PensionCredit
        {
            get { return _pensionCredit.GetValue(); }
            set
            {
                _pensionCredit.Clear();
                _pensionCredit.SendKeys(value);
            }
        }

        public string Other
        {
            get { return _other.GetValue(); }
            set
            {
                _other.Clear();
                _other.SendKeys(value);
            }
        }

        public string MaintenenceOrChildSupport
        {
            get { return _maintenenceOrChildSupport.GetValue(); }
            set
            {
                _maintenenceOrChildSupport.Clear();
                _maintenenceOrChildSupport.SendKeys(value);
            }
        }

        public string IncomeFromBoardersOrLodgers
        {
            get { return _incomeFromBoardersOrLodgers.GetValue(); }
            set
            {
                _incomeFromBoardersOrLodgers.Clear();
                _incomeFromBoardersOrLodgers.SendKeys(value);
            }
        }

        public string StudentLoansOrGrants
        {
            get { return _studentLoansOrGrants.GetValue(); }
            set
            {
                _studentLoansOrGrants.Clear();
                _studentLoansOrGrants.SendKeys(value);
            }
        }

        public string OtherIncome
        {
            get { return _otherIncome.GetValue(); }
            set
            {
                _otherIncome.Clear();
                _otherIncome.SendKeys(value);
            }
        }

        public string TotalIncome
        {
            get { return _totalIncome.GetValue(); }
        }

        public BasePage PreviousClick()
        {
            _buttonPrevious.Click();
            return new FAAboutYouPage(Client);
        }

        public BasePage NextClick(bool error = false)
        {
            _buttonNext.Click();
            if (error)
            {
                var validator = new ValidatorBuilder().WithoutErrorsCheck().Build();
                return new FAIncomePage(Client, validator);
            }
            return new FAExpenditurePage(Client);
        }
    }
}
