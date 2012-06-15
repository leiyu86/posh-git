﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Mobile.Ui.Pages;

//using Wonga.QA.Framework.UI.UiElements.Pages;
//using Wonga.QA.Framework.UI.UiElements.Pages.Common;

namespace Wonga.QA.Framework.Mobile.Journey
{
    class ZaMobileL0Journey : IL0MobileConsumerJourney
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Gender { get; set; }
        public BasePageMobile CurrentPage { get; set; }
        public String Email { get; set; }

        public ZaMobileL0Journey(BasePageMobile homePage)
        {
            CurrentPage = homePage as HomePageMobile;
            FirstName = Get.GetName();
            LastName = Get.RandomString(10);
            DateOfBirth = new DateTime(1957, 10, 30);
            Gender = "Female";
            NationalId = Get.GetNationalNumber(DateOfBirth, true);
            Email = Get.RandomEmail();
        }

        public IL0MobileConsumerJourney ApplyForLoan(int amount, int duration)
        {
            var homePage = CurrentPage as HomePageMobile;
            homePage.Sliders.HowMuch = amount.ToString();
            homePage.Sliders.HowLong = duration.ToString();
            // CurrentPage = homePage.Sliders.Apply() as PersonalDetailsPage; //migrationFixNeeded
            return this;
        }

        public IL0MobileConsumerJourney FillPersonalDetails(string firstName = null, string lastName = null, string middleNameMask = null, string gender = null, string employerNameMask = null, string email = null, string mobilePhone = null, bool submit = true)
        {
            string employerName = employerNameMask ?? Get.GetMiddleName();
            string middleName = middleNameMask ?? Get.GetMiddleName();
            //var personalDetailsPage = CurrentPage as PersonalDetailsPage;
            //personalDetailsPage.YourName.FirstName = firstName ?? FirstName;
            //personalDetailsPage.YourName.MiddleName = middleName;
            //personalDetailsPage.YourName.LastName = lastName ?? LastName;
            //personalDetailsPage.YourName.Title = "Mr";
            //personalDetailsPage.YourDetails.Number = NationalId.ToString();//"5710300020087";
            //personalDetailsPage.YourDetails.DateOfBirth = DateOfBirth.ToString("d/MMM/yyyy");
            //personalDetailsPage.YourDetails.Gender = gender ?? Gender;
            //personalDetailsPage.YourDetails.HomeStatus = "Owner Occupier";
            //personalDetailsPage.YourDetails.HomeLanguage = "English";
            //personalDetailsPage.YourDetails.NumberOfDependants = "0";
            //personalDetailsPage.YourDetails.MaritalStatus = "Single";
            //personalDetailsPage.EmploymentDetails.EmploymentStatus = "Employed Full Time";
            //personalDetailsPage.EmploymentDetails.MonthlyIncome = "3000";
            //personalDetailsPage.EmploymentDetails.EmployerName = employerName;
            //personalDetailsPage.EmploymentDetails.EmployerIndustry = "Accountancy";
            //personalDetailsPage.EmploymentDetails.EmploymentPosition = "Administration";
            //personalDetailsPage.EmploymentDetails.TimeWithEmployerYears = "9";
            //personalDetailsPage.EmploymentDetails.TimeWithEmployerMonths = "5";
            //personalDetailsPage.EmploymentDetails.WorkPhone = "0123456789";
            //personalDetailsPage.EmploymentDetails.SalaryPaidToBank = true;
            //personalDetailsPage.EmploymentDetails.NextPayDate = DateTime.Now.Add(TimeSpan.FromDays(5)).ToString("d/MMM/yyyy");
            //personalDetailsPage.EmploymentDetails.IncomeFrequency = "Monthly";
            //personalDetailsPage.ContactingYou.CellPhoneNumber = mobilePhone ?? Get.GetMobilePhone();
            //personalDetailsPage.ContactingYou.EmailAddress = email ?? Email;
            //personalDetailsPage.ContactingYou.ConfirmEmailAddress = email ?? Email;
            //personalDetailsPage.PrivacyPolicy = true;
            //personalDetailsPage.CanContact = "Yes";
            //personalDetailsPage.MarriedInCommunityProperty = "I am not married in community of property (I am single, married with antenuptial contract, divorced etc.)";

            //migrationFixNeeded
                
            //if (submit)
            //{
            //    //CurrentPage = personalDetailsPage.Submit() as AddressDetailsPage;
            //    //migrationFixNeeded
            //}

            //migrationFixNeeded
            return this;
        }

        public IL0MobileConsumerJourney FillAddressDetails(string postcode = null, string addresPeriod = null, bool submit = true)
        {
            //var addressPage = CurrentPage as AddressDetailsPage;
            //addressPage.HouseNumber = "25";
            //addressPage.Street = "high road";
            //addressPage.Town = "Kuku";
            //addressPage.County = "Province";
            //addressPage.PostCode = postcode ?? Get.GetPostcode();
            //addressPage.AddressPeriod = addresPeriod ?? "2 to 3 years";
            //if (submit)
            //{
            //    CurrentPage = addressPage.Next() as AccountDetailsPage;
            //}

            //migrationFixNeeded
            return this;
        }

        public IL0MobileConsumerJourney FillAccountDetails(string password = null, bool submit = true)
        {
            //var accountDetailsPage = CurrentPage as AccountDetailsPage;
            //accountDetailsPage.AccountDetailsSection.Password = password ?? Get.GetPassword();
            //accountDetailsPage.AccountDetailsSection.PasswordConfirm = password ?? Get.GetPassword();
            //accountDetailsPage.AccountDetailsSection.SecretQuestion = "Secret question";
            //accountDetailsPage.AccountDetailsSection.SecretAnswer = "Secret answer";
            //if (submit)
            //{
            //    CurrentPage = accountDetailsPage.Next();
            //}

            //migrationFixNeeded
            return this;
        }

        public IL0MobileConsumerJourney FillBankDetails(string accountNumber = null, string bankPeriod = null, string pin = null, bool submit = true)
        {
            //var bankDetailsPage = CurrentPage as PersonalBankAccountPage;
            //bankDetailsPage.BankAccountSection.BankName = "Capitec";
            //bankDetailsPage.BankAccountSection.BankAccountType = "Current";
            //bankDetailsPage.BankAccountSection.AccountNumber = accountNumber ?? "1234567";
            //bankDetailsPage.BankAccountSection.BankPeriod = bankPeriod ?? "2 to 3 years";
            //bankDetailsPage.PinVerificationSection.Pin = pin ?? "0000";
            //if (submit)
            //{
            //    CurrentPage = bankDetailsPage.Next() as ProcessingPage;
            //}

            //migrationFixNeeded
            return this;
        }

        public IL0MobileConsumerJourney FillCardDetails(string cardNumber = null, string cardSecurity = null, string cardType = null, string expiryDate = null, string startDate = null, string pin = null, bool submit = true)
        {
            throw new NotImplementedException();
        }

        public IL0MobileConsumerJourney WaitForAcceptedPage()
        {
            //var processingPage = CurrentPage as ProcessingPage;
            //CurrentPage = processingPage.WaitFor<AcceptedPage>() as AcceptedPage;

            //migrationFixNeeded
            return this;
        }


        public IL0MobileConsumerJourney WaitForDeclinedPage()
        {
            //var processingPage = CurrentPage as ProcessingPage;
            //CurrentPage = processingPage.WaitFor<DeclinedPage>() as DeclinedPage;

            //migrationFixNeeded
            return this;
        }


        public IL0MobileConsumerJourney FillAcceptedPage()
        {
            throw new NotImplementedException();
        }

        public IL0MobileConsumerJourney GoToMySummaryPage()
        {
            throw new NotImplementedException();
        }

        public IL0MobileConsumerJourney IgnoreAcceptingLoanAndReturnToHomePageAndLogin()
        {
            throw new NotImplementedException();
        }
    }
}