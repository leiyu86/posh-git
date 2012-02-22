﻿using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Iovation
{
    [Parallelizable(TestScope.All)]
    public class IovationServiceTests
    {
        [Test, AUT]
        public void IovationServiceIsRunning()
        {
            Assert.IsTrue(Driver.Svc.Iovation.IsRunning());
        }
    }
}