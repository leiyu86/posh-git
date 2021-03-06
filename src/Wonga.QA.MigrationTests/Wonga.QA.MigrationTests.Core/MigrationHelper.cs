﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Wonga.QA.Framework;

namespace Wonga.QA.MigrationTests.Core
{
    public class MigrationHelper
    {
        //private readonly DataDriver _drive = new DataDriver();

        private static int _migrationRunId;

        private int GetLatestMigrationRunId()
        {
            DataSet runId = new DataSet();
            //get top 1 users this will be replaced by a lite version of db and can't seem to do this with drive.data
            String query = @"SELECT TOP 1 [MigrationRunId],[StartDatetime],[EndDatetime],[DurationSeconds],[MigrationStatus]
                                FROM [MigrationStaging].[migration].[MigrationRun]
                                where MigrationStatus = 'completed'
                                order by MigrationRunId desc";

            SqlCommand cmd = new SqlCommand(query, new SqlConnection("Data Source='(local)';Initial Catalog=MigrationStaging;Integrated Security=True"));
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(runId);

            return (int)runId.Tables[0].Rows[0].ItemArray[0];
        }

        private int GetTotalUserInOpsAccounts(string forYear = null)
        {
            var opsAccounts = Drive.Data.Ops.Db.Accounts;
            var totalUsers = 0;
            var year = Convert.ToDateTime("01/01/" + forYear);

            totalUsers = string.IsNullOrEmpty(forYear) ? opsAccounts.All().Count() : opsAccounts.All().Where(opsAccounts.CreatedOn >= year).Count();

            return totalUsers;
        }

        public MigratedUser GetMigratedAccountLogin()
        {
            var migratedUser = new MigratedUser();
            var controlTableLogin = new DataSet();
            //get top 1 users this will be replaced by a lite version of db and can't seem to do this with drive.data
            const string query = @"SELECT TOP 1 [AccountId],[Login],[MigrationRunId]FROM [MigrationStaging].[test].[AcceptanceTestControl]";
            var cmd = new SqlCommand(query, new SqlConnection("Data Source='(local)';Initial Catalog=MigrationStaging;Integrated Security=True"));
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(controlTableLogin);

            migratedUser.AccountId = (Guid)controlTableLogin.Tables[0].Rows[0].ItemArray[0];
            migratedUser.Login = controlTableLogin.Tables[0].Rows[0].ItemArray[1].ToString();
            migratedUser.MigratedRunId = controlTableLogin.Tables[0].Rows[0].ItemArray[2].ToString();
            migratedUser.Password = GetMigratedAccountLoginPassword(migratedUser.Login);

            return migratedUser;
        }


        public MigratedUser GetMigratedAccountLogin(dynamic callUser = null, string forYear = null)
        {
            var migratedUser = new MigratedUser();
            var opsAccounts = Drive.Data.Ops.Db.Accounts;
            var totalUsers = 1000;
            if (!string.IsNullOrEmpty(forYear))
            {
                totalUsers = GetTotalUserInOpsAccounts(forYear);
            }

            if (callUser == null || Convert.ToInt64(callUser) > Convert.ToInt64(totalUsers) || Convert.ToInt64(callUser) == 0)
            {
                var user = new Random();
                callUser = user.Next(1, totalUsers);
            }

            var userName = opsAccounts.FindByAccountId(callUser);

            migratedUser.AccountId = userName.ExternalId;
            migratedUser.Login = userName.Login;
            migratedUser.MigratedRunId = "-1";
            migratedUser.Password = "Passw0rd";

            return migratedUser;
        }

        public void FillAcceptanceTestControlTable()
        {
            _migrationRunId = GetLatestMigrationRunId();
            DataSet opsUsersToControlTable = new DataSet();
            //get top 1000 users this will be replaced by a lite version of db and can't seem to do this with drive.data
            String query = "SELECT TOP 10 [ExternalId],[Login] FROM [Ops].[ops].[Accounts]";
            SqlCommand cmd = new SqlCommand(query, new SqlConnection("Data Source='(local)';Initial Catalog=GreyfaceShell;Integrated Security=True"));
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(opsUsersToControlTable);

            var acceptanceTestControl = Drive.Data.MigrationStaging.Db.test.AcceptanceTestControl;

            for (int counter = 0; counter < opsUsersToControlTable.Tables[0].Rows.Count; counter++)
            {
                var user = opsUsersToControlTable.Tables[0].Rows[counter];
                acceptanceTestControl.Insert(AccountId: user.ItemArray[0], Login: user.ItemArray[1], MigrationRunId: _migrationRunId);
            }
        }

        public string GetMigratedAccountLoginPassword(string login)
        {
            var greyfaceUsers = Drive.Data.WongaWholeStaging.Db.greyface.Users;

            var passwordHashKey = greyfaceUsers.FindByuser_name(login).password;

            DataSet dsDecryptedPassword = new DataSet();
            String query = "select * from [GreyfaceShell].[dbo].[GetUserPassword](@password)"; //GetUserPassword is table-valued n it can't be done with simple.Data
            SqlCommand cmd = new SqlCommand(query, new SqlConnection("Data Source='(local)';Initial Catalog=GreyfaceShell;Integrated Security=True"));
            cmd.Parameters.Add("password", SqlDbType.VarBinary).Value = passwordHashKey;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dsDecryptedPassword);

            return dsDecryptedPassword.Tables[0].Rows[0].ItemArray[0].ToString();
        }
        //public void StoreTestResults(string batchId, string testName, MigratedUser migratedUser, DateTime testStartTime, DateTime testEndTime, byte testResult)
        public void StoreTestResults(AcceptanceTestResults acceptanceTestResults)
        {
            var acceptanceTestResultsTable = Drive.Data.MigrationStaging.Db.test.AcceptanceTestResults;

            acceptanceTestResultsTable.Insert(RunId: acceptanceTestResults.MigratedUser.MigratedRunId, BatchId: acceptanceTestResults.BatchId,
                                            TestName: acceptanceTestResults.TestName, AccountId: acceptanceTestResults.MigratedUser.AccountId,
                                              Login: acceptanceTestResults.MigratedUser.Login, TestParameters: acceptanceTestResults.TestParameters.ToString(),
                                              TestStartDate: acceptanceTestResults.TestStartDate, TestEndDate: acceptanceTestResults.TestEndDate,
                                              TestResult: acceptanceTestResults.TestResult);

            var acceptanceTestControlTable = Drive.Data.MigrationStaging.Db.test.AcceptanceTestControl;

            acceptanceTestControlTable.DeleteByAccountId(acceptanceTestResults.MigratedUser.AccountId);
        }

        public void UpdateTestResultsTable(AcceptanceTestResults acceptanceTestResults)
        {
            
            //var test = Drive.Data.MigrationStaging.Db.test.AcceptanceTestResults;
            //var acceptanceTestResultsTable = test.FindbyAccountId(accountId);
            //acceptanceTestResultsTable.TestParameters = testParameters;
            //Console.WriteLine(acceptanceTestResultsTable.Count());
            //Console.WriteLine(test.Count());
            
           // acceptanceTestResultsTable.TestEndDate = testEndTime;
            //acceptanceTestResultsTable.TestResult = testResult;


            //Drive.Data.MigrationStaging.Db.test.AcceptanceTestResults.Update(acceptanceTestResultsTable);

            var acceptanceTestResultsTable = Drive.Data.MigrationStaging.Db.test.AcceptanceTestResults;
            
            acceptanceTestResultsTable.UpdateByAccountId(acceptanceTestResults.MigratedUser.AccountId, RunId: acceptanceTestResults.MigratedUser.MigratedRunId, BatchId: acceptanceTestResults.BatchId,
                                            TestName: acceptanceTestResults.TestName, AccountId: acceptanceTestResults.MigratedUser.AccountId,
                                              Login: acceptanceTestResults.MigratedUser.Login, TestParameters: acceptanceTestResults.TestParameters.ToString(),
                                              TestStartDate: acceptanceTestResults.TestStartDate, TestEndDate: acceptanceTestResults.TestEndDate,
                                              TestResult: acceptanceTestResults.TestResult);
        }

        public bool IsControlTableEmpty()
        {
            var acceptanceTestControl = Drive.Data.MigrationStaging.Db.test.AcceptanceTestControl;
            if (acceptanceTestControl.All().Count() > 0)
            {
                return false;
            }
            return true;
        }

        public bool KeepRunningTests()
        {
            var acceptanceTestControl = Drive.Data.MigrationStaging.Db.test.AcceptanceTestControl;
            if (acceptanceTestControl.All().Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
