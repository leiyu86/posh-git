﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wonga.QA.Framework.Core;

namespace Wonga.QA.Framework.Tests.Core
{
    [Explicit]
    [TestFixture]
    public class ConfigTests
    {
        [Test]
        public void ItCanReadUiHomePage()
        {
            var sut = Config.SUT;
            Assert.IsNotNull(Config.Ui.Home);
        }

        [Test]
        public void ReConfigureWithoutConfigsFileShouldWork()
        {
            Config.Configure(testTarget: "uk_local");
            Assert.IsTrue(Config.AUT == AUT.Uk && Config.SUT == SUT.Dev);
        }
    }
}
