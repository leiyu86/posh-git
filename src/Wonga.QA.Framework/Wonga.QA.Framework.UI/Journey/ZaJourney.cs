﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.UiElements.Pages;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;

namespace Wonga.QA.Framework.UI
{
    class ZaJourney : IConsumerJourney
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public BasePage CurrentPage { get; set; }

        public ZaJourney(BasePage homePage)
        {
            CurrentPage = homePage as HomePage;
            FirstName = Get.GetName();
            LastName = Get.RandomString(10);
        }
        public IConsumerJourney ApplyForLoan(int amount, int duration)
        {
            var homePage = CurrentPage as HomePage;
            homePage.Sliders.HowMuch = amount.ToString();
            homePage.Sliders.HowLong = duration.ToString();
            CurrentPage = homePage.Sliders.Apply() as PersonalDetailsPage;
            return this;
        }

        public IConsumerJourney FillPersonalDetails(string employerNameMask = null)
        {
            var email = Get.RandomEmail();
            string employerName = employerNameMask ?? Get.GetMiddleName();
            var personalDetailsPage = CurrentPage as PersonalDetailsPage;
            personalDetailsPage.YourName.FirstName = FirstName;
            personalDetailsPage.YourName.LastName = LastName;
            personalDetailsPage.YourName.Title = "Mr";
            personalDetailsPage.YourDetails.Number = "5710300020087";
            personalDetailsPage.YourDetails.DateOfBirth = "30/Oct/1957";
            personalDetailsPage.YourDetails.Gender = "Female";
            personalDetailsPage.YourDetails.HomeStatus = "Owner Occupier";
            personalDetailsPage.YourDetails.HomeLanguage = "English";
            personalDetailsPage.YourDetails.NumberOfDependants = "0";
            personalDetailsPage.YourDetails.MaritalStatus = "Single";
            personalDetailsPage.EmploymentDetails.EmploymentStatus = "Employed Full Time";
            personalDetailsPage.EmploymentDetails.MonthlyIncome = "3000";
            personalDetailsPage.EmploymentDetails.EmployerName = employerName;
            personalDetailsPage.EmploymentDetails.EmployerIndustry = "Accountancy";
            personalDetailsPage.EmploymentDetails.EmploymentPosition = "Administration";
            personalDetailsPage.EmploymentDetails.TimeWithEmployerYears = "9";
            personalDetailsPage.EmploymentDetails.TimeWithEmployerMonths = "5";
            personalDetailsPage.EmploymentDetails.WorkPhone = "0123456789";
            personalDetailsPage.EmploymentDetails.SalaryPaidToBank = true;
            personalDetailsPage.EmploymentDetails.NextPayDate = DateTime.Now.Add(TimeSpan.FromDays(5)).ToString("dd MMM yyyy");
            personalDetailsPage.EmploymentDetails.IncomeFrequency = "Monthly";
            personalDetailsPage.ContactingYou.CellPhoneNumber = "0720000098";
            personalDetailsPage.ContactingYou.EmailAddress = email;
            personalDetailsPage.ContactingYou.ConfirmEmailAddress = email;
            personalDetailsPage.PrivacyPolicy = true;
            personalDetailsPage.CanContact = "Yes";
            personalDetailsPage.MarriedInCommunityProperty =
                "I am not married in community of property (I am single, married with antenuptial contract, divorced etc.)";
            CurrentPage = personalDetailsPage.Submit() as AddressDetailsPage;
            return this;
        }

        public IConsumerJourney FillAddressDetails()
        {
            var addressPage = CurrentPage as AddressDetailsPage;
            addressPage.FlatNumber = "25";
            addressPage.Street = "high road";
            addressPage.Town = "Kuku";
            addressPage.County = "Province";
            addressPage.PostCode = "1234";
            addressPage.AddressPeriod = "2 to 3 years";
            CurrentPage = addressPage.Next() as AccountDetailsPage;
            return this;
        }

        public IConsumerJourney FillAccountDetails()
        {
            var accountDetailsPage = CurrentPage as AccountDetailsPage;
            accountDetailsPage.AccountDetailsSection.Password = Get.GetPassword();
            accountDetailsPage.AccountDetailsSection.PasswordConfirm = Get.GetPassword();
            accountDetailsPage.AccountDetailsSection.SecretQuestion = "Secret question'-.";
            accountDetailsPage.AccountDetailsSection.SecretAnswer = "Secret answer";
            CurrentPage = accountDetailsPage.Next();//returns PersonalBankAccountPage
            return this;
        }

        public IConsumerJourney FillBankDetails()
        {
            var bankDetailsPage = CurrentPage as PersonalBankAccountPage;
            bankDetailsPage.BankAccountSection.BankName = "Capitec";
            bankDetailsPage.BankAccountSection.BankAccountType = "Current";
            bankDetailsPage.BankAccountSection.AccountNumber = "1234567";
            bankDetailsPage.BankAccountSection.BankPeriod = "2 to 3 years";
            bankDetailsPage.PinVerificationSection.Pin = "0000";
            CurrentPage = bankDetailsPage.Next() as ProcessingPage;
            return this;
        }

        public IConsumerJourney FillCardDetails()
        {
            throw new NotImplementedException();
        }

        public IConsumerJourney WaitForAcceptedPage()
        {
            var processingPage = CurrentPage as ProcessingPage;
            CurrentPage = processingPage.WaitFor<AcceptedPage>() as AcceptedPage;
            return this;
        }

        public IConsumerJourney WaitForDeclinedPage()
        {
            var processingPage = CurrentPage as ProcessingPage;
            CurrentPage = processingPage.WaitFor<DeclinedPage>() as DeclinedPage;
            return this;
        }

        public IConsumerJourney FillAcceptedPage()
        {
            var acceptedPage = CurrentPage as AcceptedPage;
            acceptedPage.SignConfirmZA();
            CurrentPage = acceptedPage.Submit() as DealDonePage;
            return this;
        }

        public IConsumerJourney GoToMySummaryPage()
        {
            var dealDonePage = CurrentPage as DealDonePage;
            CurrentPage = dealDonePage.ContinueToMyAccount() as MySummaryPage;
            return this;
        }
    }
}