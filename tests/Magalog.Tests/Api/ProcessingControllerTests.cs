using Magalog.API.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;

namespace Magalog.Tests.Api;

public class ProcessingControllerTests
{
    [Fact]
    public async void Post_InvalidFile_MustReturnBadRequest()
    {
        //arrange       
        var mocker = new AutoMocker();
        var fileMock = mocker.GetMock<IFormFile>();
        var stream = new MemoryStream();
        fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
        fileMock.Setup(f => f.Length).Returns(0); 
        
        var controller = mocker.CreateInstance<ProcessamentoController>();
                
        //act
        var result = await controller.Post(fileMock.Object);

        //assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Arquivo inválido.", badRequestResult.Value);
    }

 
    [Theory]
    [InlineData(1, "25-01-2021", "22-01-2021")]
    [InlineData(2, "30-01-2021", "25-01-2021")]
    public async void Get_InvalidStartEndDate_MustReturnBadRequest(int orderId, string startDateString, string endDateString)
    {
        //arrange
        var mocker = new AutoMocker();
        var controller = mocker.CreateInstance<ProcessamentoController>();

        var startDate = DateOnly.ParseExact(startDateString, "dd-MM-yyyy");
        var endDate = DateOnly.ParseExact(endDateString, "dd-MM-yyyy");
        
        //act
        var result = await controller.Get(orderId, startDate, endDate);


        //assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("A data inicial não pode ser maior que a data final.", badRequestResult.Value);

    }

    [Theory]
    [InlineData(1, "20-01-2021", "22-01-2021")]
    [InlineData(2, "25-02-2021", "26-02-2021")]
    public async void Get_ValidStartEndDate_MustReturnNotFound(int orderId, string startDateString, string endDateString)
    {
        //arrange
        var mocker = new AutoMocker();
        var controller = mocker.CreateInstance<ProcessamentoController>();

        var startDate = DateOnly.ParseExact(startDateString, "dd-MM-yyyy");
        var endDate = DateOnly.ParseExact(endDateString, "dd-MM-yyyy");

        //act
        var result = await controller.Get(orderId, startDate, endDate);

        //assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Nenhum registro encontrado.", notFoundResult.Value);
    }
}
