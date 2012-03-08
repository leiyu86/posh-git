﻿using System.Threading;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Tests.Core;
using System.Linq;
using System;

namespace Wonga.QA.Tests.Ui
{
    /// <summary>
    /// Slider tests for Za
    /// </summary>
    /// 
    class SliderTests : UiTest
    {
        private int _amountMax;
        private int _amountMin;
        private int _termMax;
        private int _termMin;
        private string _repaymentDate;
        private ApiResponse _response;
        private DateTime _actualDate;


        [SetUp, JIRA("QA-149")]
        public void GetInitialValues()
        {
            ApiRequest request;
            switch (Config.AUT)
            {
                case AUT.Uk:
                    request = new GetFixedTermLoanOfferUkQuery();
                    break;
                case AUT.Za:
                    request = new GetFixedTermLoanOfferZaQuery();
                    break;
                case AUT.Ca:
                    request = new GetFixedTermLoanOfferCaQuery();
                    break;
                default:
                    throw new NotImplementedException();
            }


            _response = Driver.Api.Queries.Post(request);
            _amountMax = (int)Decimal.Parse(_response.Values["AmountMax"].Single());
            _amountMin = (int)Decimal.Parse(_response.Values["AmountMin"].Single());
            _termMax = Int32.Parse(_response.Values["TermMax"].Single());
            _termMin = Int32.Parse(_response.Values["TermMin"].Single());

        }

        [Test, AUT(AUT.Za), JIRA("QA-149")]
        public void MovingSlidersRepaymentDateShouldBeCorrect()
        {
            var page = Client.Home();
            int randomDuration = _termMin + (new Random()).Next(_termMax - _termMin);
            page.Sliders.HowLong = randomDuration.ToString();

            string[] dateArray = page.Sliders.GetRepaymentDate.Split(' ');
            string day = Char.IsDigit(dateArray[1].ElementAt(1)) ? dateArray[1].Remove(2, 2) : dateArray[1].Remove(1, 2);
            _repaymentDate = day + " " + dateArray[2] + " " + dateArray[3];

            _actualDate = DateTime.Now.AddDays(randomDuration);
            Assert.AreEqual(_repaymentDate, String.Format("{0:d MMM yyyy}", _actualDate));
        }

        [Test, AUT(AUT.Za), JIRA("QA-149")]
        public void MovingSlidersLoanSummaryShouldBeCorrect()
        {
            var page = Client.Home();
            int randomAmount = _amountMin + (new Random()).Next(_amountMax - _amountMin);
            int randomDuration = _termMin + (new Random()).Next(_termMax - _termMin);

            page.Sliders.HowMuch = randomAmount.ToString();
            page.Sliders.HowLong = randomDuration.ToString();

            _response = Driver.Api.Queries.Post(new GetFixedTermLoanCalculationZaQuery { LoanAmount = randomAmount, Term = randomDuration });

            string totalRepayable = _response.Values["TotalRepayable"].Single();
            Assert.AreEqual(page.Sliders.GetTotalToRepay.Remove(0, 1), totalRepayable);


        }
        [Test, AUT(AUT.Za), JIRA("QA-149")]
        public void MovingSlidersBeyondMaxIsNotAllowedByFrontEnd()
        {
            var page = Client.Home();
            int amountBiggerThanMax = _amountMax + 1000;
            page.Sliders.HowMuch = amountBiggerThanMax.ToString();
            Assert.AreEqual(_amountMax.ToString(), page.Sliders.GetTotalAmount.Remove(0, 1));
        }

        [Test, AUT(AUT.Ca)]
        public void VariableInterestisCalculatedCorrectly()
        {
            var homePage = Client.Home();
            homePage.Sliders.HowMuch = "100";
            homePage.Sliders.HowLong = "30";
            Console.WriteLine("Sliders: {0} for {1}", homePage.Sliders.HowLong, homePage.Sliders.HowLong);
            Thread.Sleep(500);
            //0.5 sec pause

            Assert.AreEqual(homePage.Sliders.GetTotalToRepay, "$121.00");
            //maximum charge is 21$ for each 100$ borrowed for 30 days.

        }

        [Test, AUT(AUT.Ca, AUT.Za), JIRA("QA-156", "QA-238")]
        public void L0DefaultAmountSliderValueShouldBeCorrect()
        {
            var page = Client.Home();
            switch (Config.AUT)
            {
                case AUT.Za:
                    Assert.AreEqual(page.Sliders.HowMuch, "1335");
                    break;
                case AUT.Ca:
                    Assert.AreEqual(page.Sliders.HowMuch, "265");
                    break;
            }

        }

        [Test, AUT(AUT.Ca, AUT.Za), JIRA("QA-156", "QA-238")]
        public void LNDefaultAmountSliderValueShouldBeCorrect()
        {
            var loginPage = Client.Login();
            string email = Data.GetEmail();
            CustomerBuilder.New().WithEmailAddress(email).Build();
            loginPage.LoginAs(email);

            var page = Client.Home();
            switch (Config.AUT)
            {
                case AUT.Za:
                    Assert.AreEqual(page.Sliders.HowMuch, "1335");
                    break;
                case AUT.Ca:
                    Assert.AreEqual(page.Sliders.HowMuch, "265");
                    break;
            }
        }
        [Test, AUT(AUT.Ca, AUT.Za), JIRA("QA-241", "QA-159")]
        public void L0DefaultDurationSliderValueShouldBeCorrect()
        {
            var page = Client.Home();
            switch (Config.AUT)
            {
                case AUT.Za:
                    string[] dateArray = page.Sliders.GetRepaymentDate.Split(' ');
                    string day = Char.IsDigit(dateArray[1].ElementAt(1)) ? dateArray[1].Remove(2, 2) : dateArray[1].Remove(1, 2);
                    _repaymentDate = day + " " + dateArray[2] + " " + dateArray[3];

                    var today = DateTime.Today;
                    var expectedDate = today.Day <= 25
                                           ? new DateTime(today.Year, today.Month, 25)
                                           : new DateTime(today.Year, today.Month, 25).AddMonths(1);
                    Assert.AreEqual(String.Format("{0:d MMM yyyy}", expectedDate), _repaymentDate);
                    break;
                case AUT.Ca:
                    Assert.AreEqual(page.Sliders.HowLong, "11");
                    break;
            }

        }

        [Test, AUT(AUT.Ca, AUT.Za), JIRA("QA-241", "QA-159")]
        public void LNDefaultDurationSliderValueShouldBeCorrect()
        {
            var loginPage = Client.Login();
            string email = Data.GetEmail();
            CustomerBuilder.New().WithEmailAddress(email).Build();
            loginPage.LoginAs(email);

            var page = Client.Home();
            switch (Config.AUT)
            {
                case AUT.Za:
                    string[] dateArray = page.Sliders.GetRepaymentDate.Split(' ');
                    string day = Char.IsDigit(dateArray[1].ElementAt(1)) ? dateArray[1].Remove(2, 2) : dateArray[1].Remove(1, 2);
                    _repaymentDate = day + " " + dateArray[2] + " " + dateArray[3];

                    var today = DateTime.Today;
                    var expectedDate = today.Day <= 25
                                           ? new DateTime(today.Year, today.Month, 25)
                                           : new DateTime(today.Year, today.Month, 25).AddMonths(1);
                    Assert.AreEqual(String.Format("{0:d MMM yyyy}", expectedDate), _repaymentDate);
                    break;
                case AUT.Ca:
                    Assert.AreEqual(page.Sliders.HowLong, "11");
                    break;
            }

        }

        [Test, AUT(AUT.Ca, AUT.Za), JIRA("QA-237", "QA-153")]
        public void ChangingAmountBeyondMinIsNotAllowedByFrontEnd()
        {
            var product = Driver.Db.Payments.Products.FirstOrDefault();
            int minAmountValue = (int)product.AmountMin;
            int setAmountValue = minAmountValue - 1;
            var page = Client.Home();
            page.Sliders.HowMuch = setAmountValue.ToString();
            page.Sliders.HowLong = "10"; //To lost focus
            page.Help.HelpTriggerClick();
            Assert.AreEqual(minAmountValue.ToString(), page.Sliders.HowMuch);
        }

        [Test, AUT(AUT.Ca, AUT.Za), JIRA("QA-239", "QA-158")]
        public void L0MaxDurationSliderValueShouldBeCorrect()
        {
            var product = Driver.Db.Payments.Products.FirstOrDefault();
            int maxLoanDuration = product.TermMax;
            int setLoanDuration = maxLoanDuration + 1;
            var page = Client.Home();
            page.Sliders.HowLong = setLoanDuration.ToString();
            page.Sliders.HowMuch = "10"; //To lost focus
            page.Help.HelpTriggerClick();
            Assert.AreEqual(maxLoanDuration.ToString(), page.Sliders.HowLong);
        }

        [Test, AUT(AUT.Ca, AUT.Za), JIRA("QA-239", "QA-158")]
        public void LNMaxDurationSliderValueShouldBeCorrect()
        {
            var loginPage = Client.Login();
            string email = Data.GetEmail();
            CustomerBuilder.New().WithEmailAddress(email).Build();
            loginPage.LoginAs(email);

            var page = Client.Home();
            var product = Driver.Db.Payments.Products.FirstOrDefault();
            int maxLoanDuration = product.TermMax;
            int setLoanDuration = maxLoanDuration + 1;

            page.Sliders.HowLong = setLoanDuration.ToString();
            page.Sliders.HowMuch = "10"; //To lost focus
            page.Help.HelpTriggerClick();
            Assert.AreEqual(maxLoanDuration.ToString(), page.Sliders.HowLong);
        }




    }
}