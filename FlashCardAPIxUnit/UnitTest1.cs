using FlashCardApi.Controllers;
using FlashCardApi.CoreServices.ServiceInterface;
using FlashCardApi.Infrastructure.RepositoryInterface;
using FlashCardApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlashCardAPIxUnit
{
    public class UnitTest1
    {
        private FlashcardController _controller;
        private Mock<IFlashCardService> _mockService;
        private Mock<IFlashCardRepo> _mockRepo;

        public UnitTest1() 
        {
            _mockService = new Mock<IFlashCardService>();
            _mockRepo = new Mock<IFlashCardRepo>();
            _controller = new FlashcardController(_mockService.Object);
        }
        [Fact]
        public async Task DeleteCard_Success()
        {
            //Arrange
            var fc = new FlashCard { Id = 1, Question = "1+2", Answer = "3", createdBy = "Apple" };
            _mockService.Setup(service => service.AddCard(fc)).ReturnsAsync(fc);
            _mockRepo.Setup(repo => repo.AddCard(fc)).ReturnsAsync(fc);
            _mockService.Setup(service => service.DeleteCard(1));
            _mockRepo.Setup(repo => repo.DeleteCard(1));
            //Act
            var result = await _controller.DeleteFlashCard(1);
            //Assert  
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task UpdateFlashCard_Success() 
        {
            //Arrange
            var fc = new FlashCard { Id = 1, Question = "3+1", Answer = "4", createdBy = "Apple" };
            var fc1 = new FlashCard { Id = 1, Question = "3+3", Answer = "6", createdBy = "Apples" };

            _mockService.Setup(service => service.AddCard(fc));
            _mockRepo.Setup(repo => repo.AddCard(fc));
            _mockService.Setup(service => service.UpdateCard(fc1));
            _mockRepo.Setup(repo=>repo.UpdateCard(fc1));

            var result = await _controller.PutFlashCard(fc1);

            var noContent = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContent.StatusCode);

        }
    }
}