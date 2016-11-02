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
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Account>(null);

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);

        }

        [TestMethod]
        public void GetBalance_DBThrowsException_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.Get(It.IsAny<int>()))
                .Throws<Exception>();

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal),opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);

        }

        [TestMethod]
        public void Withdraw_AccountIsOK_ReturnsNewBalance()
        {
            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = true
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Verifiable();


            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.WithDraw(1, 1);

            //assert
            Assert.IsTrue(opResult.Success);
            Assert.AreEqual(12 - 1, opResult.Result);

            Mock.Verify(mockRepo);
        }

        [TestMethod]
        public void Withdraw_AccountDoesNotExists_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Account>(null);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.WithDraw(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Withdraw_AccountHasNotEnoughMoney_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = true
            };

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.WithDraw(1, 13);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.NotEnoughMoneyOnAccount, opResult.ErrorMessage);

        }

        [TestMethod]
        public void Withdraw_AccountIsClosed_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = false
            };

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.WithDraw(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountIsDisabled, opResult.ErrorMessage);

        }

        [TestMethod]
        public void Withdraw_DBThrowsException_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();
            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = true
            };
            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Throws<Exception>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.WithDraw(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);

        }

        //depositTests
        [TestMethod]
        public void Deposit_AccountIsOK_ReturnsNewBalance()
        {
            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = true
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Verifiable();


            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsTrue(opResult.Success);
            Assert.AreEqual(12 + 1, opResult.Result);

            Mock.Verify(mockRepo);
        }

        [TestMethod]
        public void Deposit_AccountDoesNotExists_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Account>(null);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Deposit_AccountIsClosed_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = false
            };

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountIsDisabled, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Deposit_DBThrowsException_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();
            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = true
            };
            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Throws<Exception>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);
        }

        //DisableAccountTests
        [TestMethod]
        public void Disable_AccountCanBeClosed_ReturnsSuccess()
        {
            var testAccount = new Account()
            {
                UserId = 123,
                Balance = 0,
                Enabled = true
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Verifiable();


            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsTrue(opResult.Success);

            Mock.Verify(mockRepo);
        }

        [TestMethod]
        public void Disable_AccountAlreadyClosed_ReturnsErrorMessage()
        {
            var testAccount = new Account()
            {
                UserId = 123,
                Balance = 0,
                Enabled = false
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(ErrorMessages.AccountIsDisabled, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Disable_AccountHasMoney_ReturnsErrorMessage()
        {
            var testAccount = new Account()
            {
                UserId = 123,
                Balance = 15,
                Enabled = true
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(ErrorMessages.AccountHasMoney, opResult.ErrorMessage);
        }


        [TestMethod]
        public void Disable_DBThrowsException_ReturnsErrorMessage()
        {
            
            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Throws<Exception>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);
        }

    }
}
