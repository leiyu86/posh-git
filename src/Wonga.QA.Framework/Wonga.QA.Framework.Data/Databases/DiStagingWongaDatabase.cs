﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Data;

namespace Wonga.QA.Framework.Data
{
    public class DiStagingWongaDatabase : QAFDatabase
    {
        public DiStagingWongaDatabase(string connectionString)
            : base(connectionString)
        {

        }
    }
}
