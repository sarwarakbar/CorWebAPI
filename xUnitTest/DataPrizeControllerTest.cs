using System;
using Xunit;
using Moq;
using BAL;
using CoreWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using DAL.Model;
using System.Collections.Generic;

namespace xUnitTest
{
    public class DataPrizeControllerTest
    {
        private readonly Mock<IRepositoryBAL> _mockRepo;
        private readonly DataPrizeController _controller;

        public DataPrizeControllerTest()
        {
            _mockRepo = new Mock<IRepositoryBAL>();
            _controller = new DataPrizeController(_mockRepo.Object);
        }

    
        [Fact]
        public void GetWithParameters_Result_NotNull()
        {
            string cat = "chemistry";
            string year = "1975";

            // Act
            var actualResult = _controller.GetByCategoryYear(cat, year);

            // Assert
            Assert.NotNull(actualResult);
        }

        [Fact]
        public void GetByID_Result_NotNull()
        {
            int id = 2;
            _mockRepo.Setup(repo => repo.GetById(id))
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

            

            // Act
            var actualResult = _controller.GetById(id);

            // Assert
            Assert.NotNull(actualResult);
            Assert.NotEmpty(actualResult);
            
        }

        [Fact]
        public void GetAllMethod_Result_NotNull()
        {

            // Act
            var actualResult = _controller.GetAllPrizes();

            // Assert
            Assert.NotNull(actualResult);
        }

        [Fact]
        public void GetAllMethod_Result_Bool_True()
        {
            _mockRepo.Setup(repo => repo.GetAll())
              .Returns(new List<Prize>() {
                    new Prize {
                      Year="2024",
                      Category="Chemistry",
                      OverallMotivation="abc",
                      PrizeId=1                                         
                    },
                    
                    new Prize {
                      Year = "2025",
                      Category = "Physics",
                      OverallMotivation = "xyz",
                      PrizeId = 2 }
              });

            bool a = false;

            // Act
            var actualResult = _controller.GetAllPrizes() as List<Prize>;
            if(actualResult!=null)
            {
                a = true; 
            }

            // Assert
            Assert.True(a);
        }

        [Fact]
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

            var viewResult = Assert.IsType<List<Prize>>(result);
            var prizewinner = Assert.IsType<List<Prize>>(viewResult);
            Assert.Equal(2, prizewinner.Count);
            Assert.NotEmpty(result);
        }






    }
}
