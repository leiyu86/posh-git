﻿using System;

namespace Wonga.QA.Framework.UI.Mappings.Pages
{
    public sealed class AddressDetailsPage
    {
        public String FormId { get; set; }
        public String PostCode { get; set; }
        public String FlatNumber { get; set; }
        public String District { get; set; }
        public String County { get; set; }
        public String AddressPeriod { get; set; }
        public String AddressOptions { get; set; }

        public String LookupButton { get; set; }
        public String NextButton { get; set; }
    }
}