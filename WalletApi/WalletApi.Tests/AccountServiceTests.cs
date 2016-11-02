using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WalletApi.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        

        //GetAccountTests
        [TestMethod]
        public void GetAccount_CorrectUserID_ReturnsAccountId()
        {
            
        }

        [TestMethod]
        public void GetAccount_UserIDNotExists_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void GetAccount_DbNotAvailable_ReturnsErrorMessage()
        {
        }

        //getbalanceTests
        [TestMethod]
        public void GetBalance_CorrectAccount_ReturnsExactBalance()
        {
        }

        [TestMethod]
        public void GetBalance_AccountDoesNotExists_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void GetBalance_DBThrowsException_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void Withdraw_AccountIsOK_ReturnsNewBalance()
        {
        }

        [TestMethod]
        public void Withdraw_AccountDoesNotExists_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void Withdraw_AccountHasNotEnoughMoney_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void Withdraw_AccountIsClosed_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void Withdraw_DBThrowsException_ReturnsErrorMessage()
        {
        }

        //depositTests
        [TestMethod]
        public void Deposit_AccountIsOK_ReturnsNewBalance()
        {
        }

        [TestMethod]
        public void Deposit_AccountDoesNotExists_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void Deposit_AccountIsClosed_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void Deposit_DBThrowsException_ReturnsErrorMessage()
        {
        }

        //DisableAccountTests
        [TestMethod]
        public void Disable_AccountCanBeClosed_ReturnsSuccess()
        {
        }

        [TestMethod]
        public void Disable_AccountAlreadyClosed_ReturnsErrorMessage()
        {
        }

        [TestMethod]
        public void Disable_AccountHasMoney_ReturnsErrorMessage()
        {
        }


        [TestMethod]
        public void Disable_DBThrowsException_ReturnsErrorMessage()
        {
        }

    }
