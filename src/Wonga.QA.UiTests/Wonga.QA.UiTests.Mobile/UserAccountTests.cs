﻿using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Old;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.UiTests.Mobile
{
    public class UserAccountTests : UiMobileTest
    {
        [Test, AUT(AUT.Za)]
        public void UpdatePassword()
        {
            var login = Client.Login();
            string email = Get.RandomEmail();
            Customer customer = CustomerBuilder.New()
                .WithEmailAddress(email)
                .Build();
            Application application = ApplicationBuilder.New(customer).Build();
            application.RepayOnDueDate();
            var summaryPage = login.LoginAs(email, Get.GetPassword());
            var myPersonalDetailsPage = summaryPage.GoToMyPersonalDetailsPage();
            var refreshedMyPersonalDetailsPage = myPersonalDetailsPage.EditPassword(Get.GetPassword(), "Newpassw0rd");

            //logout and login with new password
            var homePage = refreshedMyPersonalDetailsPage.TabsElementMobile.LogOut();
            var newSummaryPage = homePage.Tabs.LogIn(email, "Newpassw0rd");
        }

        [Test, AUT(AUT.Za)]
        public void UpdateAddressDetails()
        {
            var login = Client.Login();
            string email = Get.RandomEmail();
            Customer customer = CustomerBuilder.New()
                .WithEmailAddress(email)
                .Build();
            Application application = ApplicationBuilder.New(customer).Build();
            application.RepayOnDueDate();
            var summaryPage = login.LoginAs(email, Get.GetPassword());
            var myPersonalDetailsPage = summaryPage.GoToMyPersonalDetailsPage();
            myPersonalDetailsPage.EditAddress();
        }

        [Test, AUT(AUT.Za)]
        public void UpdateHomeTelephoneNumber()
        {
            var login = Client.Login();
            string email = Get.RandomEmail();
            Customer customer = CustomerBuilder.New()
                .WithEmailAddress(email)
                .Build();
            Application application = ApplicationBuilder.New(customer).Build();
            application.RepayOnDueDate();
            var summaryPage = login.LoginAs(email, Get.GetPassword());
            var myPersonalDetailsPage = summaryPage.GoToMyPersonalDetailsPage();
            //edit home phone number
            const string number = "0213456789";
            var refreshedPersonalDetailsPage = myPersonalDetailsPage.EditHomeTelephoneNumber(number);
            Assert.IsTrue(refreshedPersonalDetailsPage.Phone.Text.Contains(number));
        }

        [Test, AUT(AUT.Za)]
        public void UpdateMobileTelephoneNumber()
        {
            var login = Client.Login();
            string email = Get.RandomEmail();
            Customer customer = CustomerBuilder.New()
                .WithEmailAddress(email)
                .Build();
            Application application = ApplicationBuilder.New(customer).Build();
            application.RepayOnDueDate();
            //var custhelp = VanillaCustomerHelper.New();
            //string email = custhelp.GetVanillaCustomer().Email;
            
            var summaryPage = login.LoginAs(email, Get.GetPassword());
            var myPersonalDetailsPage = summaryPage.GoToMyPersonalDetailsPage();
            //edit mobile number
            const string number = "0210006789";
            var refreshedPersonalDetailsPage = myPersonalDetailsPage.EditMobileTelephoneNumber(number);
            Assert.IsTrue(refreshedPersonalDetailsPage.Phone.Text.Contains(number));
        }


    }
}
