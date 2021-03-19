using System;
using System.Collections.Generic;
using BLL.exception;
using BLL.impl;
using BLL.Intarfaces;
using BLL_TEST.TestConstant;
using DAL;
using DAL.Entity;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BLL_TEST
{
    public class UserServiceImplTest
    {
        private IUserService userService;
        private Mock<IUserRepository> userRepository;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            userRepository = new Mock<IUserRepository>();
            userService = new UserService(userRepository.Object);
        }

        [Test]
        public void findByEmailAllCorrect()
        {
            User user = ServicesTestConstant.getAddreser();
            userRepository.Setup(s => s.findByName(It.IsAny<string>())).Returns(user);

            User result = userService.findByName(user.UserName);

            Assert.AreEqual(user, result);
            userRepository.Verify(
                place =>
                    place.findByName(It.IsAny<string>()),
                Times.Once());
        }

        [Test]
        public void findByEmailIncorrectEmail()
        {
            User user = ServicesTestConstant.getAddreser();
            userRepository.Setup(s => s.findByEmail(It.IsAny<string>())).Returns((User) null);

            var actualResult =
                Assert.Throws<UsernameNotFoundException>(() => userService.findByName(user.UserName));

            Assert.AreEqual(typeof(UsernameNotFoundException), actualResult.GetType());
        }

        [Test]
        public void replenishAccountBalanceAllCorrect()
        {
            User expected = ServicesTestConstant.getAddreser();
            User setIn = ServicesTestConstant.getAddreser();
            setIn.UserMoneyInCents = 0L;
            expected.UserMoneyInCents = 10L;
            long paymentSum = 10L;
            userRepository.Setup(s => s.findByName(It.IsAny<string>()))
                .Returns(setIn);

            User result = userService.ReplenishAccountBalance(expected.UserName, paymentSum);

            userRepository.Verify(
                place =>
                    place.Save(),
                Times.Once());

            Assert.AreEqual(expected, result);
            Assert.AreEqual(10L, setIn.UserMoneyInCents);
        }

        [Test]
        public void replenishAccountBalanceNoSuchUser()
        {
            userRepository.Setup(s => s.findByName(It.IsAny<string>()))
                .Returns((User) null);

            var actualResult =
                Assert.Throws<NoSuchUserException>(() =>
                    userService.ReplenishAccountBalance(ServicesTestConstant.getUserId(), 10));

            Assert.AreEqual(typeof(NoSuchUserException), actualResult.GetType());
        }

        [Test]
        public void replenishAccountBalanceToMuchMoneyException()
        {
            User user = new User(null, null, long.MaxValue);
            userRepository.Setup(s => s.findByName(It.IsAny<string>()))
                .Returns(user);

            var actualResult =
                Assert.Throws<ToMuchMoneyException>(() =>
                    userService.ReplenishAccountBalance(ServicesTestConstant.getUserId(), 10));
            Assert.AreEqual(typeof(ToMuchMoneyException), actualResult.GetType());
        }
    }
}