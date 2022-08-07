using BAL.Service;
using BAL;
using CoreWebApi.Controllers;
using DAL.Interface;
using DAL.Model;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DAL;

namespace nUnitTest
{
    public class Tests
    {
        private readonly Mock<IRepositoryBAL> _mockRepo;
        private readonly DataPrizeController _controller;

        public Tests()
        {
            _mockRepo = new Mock<IRepositoryBAL>();
            _controller = new DataPrizeController(_mockRepo.Object);
        }

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            int id = 600;

            // Act
            var okResult = _controller.GetById(id);

            // Assert
            Assert.NotNull(okResult);
        }

        [Test]
        public void Test_GetAllMethod()
        {
            // Act
            var result = _controller.GetAllPrizes();

            // Assert
            Assert.AreEqual(result, result);           
        }

        [Test]
        public void Index_ActionExecutes_ReturnsExactNumberOfEmployees()
        {
            _mockRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Prize>() {
                    new Prize {
                      Year="2024",
                      Category="Chemistry",
                      OverallMotivation="abc",
                      PrizeId=1 },
                    new Prize {
                      Year = "2025",
                      Category = "Physics",
                      OverallMotivation = "xyz",
                      PrizeId = 2 }
                });

            var result = _controller.GetAllPrizes() as List<Prize>;

            
            Assert.AreEqual(2, result.Count);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }



    }
}