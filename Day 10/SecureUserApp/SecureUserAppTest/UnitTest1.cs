using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SecureUserApp;
using System.IO;

namespace SecureUserAppTest
{
    [TestClass]
    public class SecurityTests
    {
        private SecurityService _security;
        private User _userLogic;

        [TestInitialize]
        public void Setup()
        {
            _security = new SecurityService();
            _userLogic = new User();
        }

        [TestMethod]
        public void PasswordHashing_ShouldCreateUniqueHash()
        {
            string pass = "Password123";
            string hash = _security.HashPassword(pass);

            Assert.AreNotEqual(pass, hash);
            Assert.AreEqual(hash, _security.HashPassword(pass)); 
        }

        [TestMethod]
        public void Encryption_ShouldDecryptToOriginalValue()
        {
            string sensitiveData = "mysecret@email.com";
            string encrypted = _security.EncryptData(sensitiveData);
            string decrypted = _security.DecryptData(encrypted);

            Assert.AreNotEqual(sensitiveData, encrypted);
            Assert.AreEqual(sensitiveData, decrypted);
        }

        [TestMethod]
        public void RegistrationAndLogin_ShouldBeSuccessful()
        {
            bool regSuccess = _userLogic.Register("Admin", "SecurePass", "admin@test.com");
            bool loginSuccess = _userLogic.Login("Admin", "SecurePass");

            Assert.IsTrue(regSuccess);
            Assert.IsTrue(loginSuccess);
        }

        [TestMethod]
        public void Logging_ShouldWriteToFile()
        {
            LoggerService.Log("Test Log Entry");

            Assert.IsTrue(File.Exists("app_log.txt"));
        }
    }
}
