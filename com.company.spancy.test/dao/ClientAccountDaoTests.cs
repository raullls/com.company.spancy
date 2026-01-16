using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class ClientAccountDaoTests : BaseTest
    {
        public IClientDao ClientDao { get; set; }
        public IAccountDao AccountDao { get; set; }
        public IClientAccountDao ClientAccountDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.ClientAccountDao.GetAll().Count);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            bool status = false;
            Client c1 = new Client();
            c1.Name = "Alexander Mahone";
            this.ClientDao.SaveEntity(c1);

            Account a1 = new Account();
            a1.Number = "Credit Account";
            this.AccountDao.SaveEntity(a1);

            IList<Client> clientList = this.ClientDao.LoadAllEntities();
            Client client = clientList[0];

            IList<Account> accountList = this.AccountDao.LoadAllEntities();
            Account account = accountList[0];
            client.Accounts.Add(account);

            this.ClientDao.SaveEntity(client);

            IList<Client> clientList2 = this.ClientAccountDao.IsPresent(client.Id, account.Id);
            if (null != clientList2)
            {
                if (clientList2.Count > 0)
                {
                    status = true;
                }
            }
            Assert.IsTrue(status);
        }
    }
}
