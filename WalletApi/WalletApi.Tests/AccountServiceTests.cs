using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WalletApi.Repositories.Account;
using WalletApi.DataLayer;
using System.Collections.Generic;
using WalletApi.ServiceLayer;
using WalletApi.Utilities;
using System.Linq;

namespace WalletApi.Tests
{
    [TestClass]
    public class AccountServiceTests
    {


        //GetAccountTests
        [TestMethod]
        public void GetAccount_CorrectUserID_ReturnsAccountId()
        {
            //arrange
            var testAccount = new Account()
            {
                Id = 1
            };

            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Returns(new List<Account>() { testAccount });

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetAccountId(1);

            //assert
            Assert.IsTrue(opResult.Success);
            Assert.AreEqual(1, opResult.Result);
        }

        [TestMethod]
        public void GetAccount_UserIDNotExists_ReturnsAccountNotFound()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Returns(Enumerable.Empty<Account>());

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetAccountId(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(0,opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);
        }

        [TestMethod]
        public void GetAccount_DbNotAvailable_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Throws<Exception>();

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetAccountId(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(0,opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);
        }

        //getbalanceTests
        [TestMethod]
        public void GetBalance_CorrectAccount_ReturnsExactBalance()
        {
            var testAccount = new Account()
            {
                Balance = 12
            };

            var mockRepo = new Mock<IAccountRepository>();
            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsTrue(opResult.Success);
            Assert.AreEqual(12, opResult.Result);
        }

        [TestMethod]
        public void GetBalance_AccountDoesNotExists_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Returns(Enumerable.Empty<Account>());

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.IsNull(opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);

        }

        [TestMethod]
        public void GetBalance_DBThrowsException_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Throws<Exception>();

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.IsNull(opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);

        }

        [TestMethod]
        public void Withdraw_AccountIsOK_ReturnsNewBalance()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Withdraw_AccountDoesNotExists_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Withdraw_AccountHasNotEnoughMoney_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Withdraw_AccountIsClosed_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Withdraw_DBThrowsException_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        //depositTests
        [TestMethod]
        public void Deposit_AccountIsOK_ReturnsNewBalance()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Deposit_AccountDoesNotExists_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Deposit_AccountIsClosed_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Deposit_DBThrowsException_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        //DisableAccountTests
        [TestMethod]
        public void Disable_AccountCanBeClosed_ReturnsSuccess()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Disable_AccountAlreadyClosed_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Disable_AccountHasMoney_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }


        [TestMethod]
        public void Disable_DBThrowsException_ReturnsErrorMessage()
        {
            throw new NotImplementedException();
        }

    }
}
