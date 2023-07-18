using MediatR;
using Moq;
using TestProjectSfApi.Application.SystemDataLoadLogItems.Queries;
using TestProjectSfApi.ConsoleApp.Wrapper;
using TestProjectSfApi.Domain.Entities;

namespace TestProjectSfApi.UnitTests
{
    [TestClass]
    public class SystemDataLoadLogWrapperTests
    {
        private Mock<ISender?> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mediator = new Mock<ISender?>();
        }

        [TestMethod]
        public void ShouldCompareRecordsResultsBeforeAndAfterRuntime()
        {
            //Arrange

            var getRecord = new GetRecordsQuery();
            _mediator.Setup(a => a.Send(It.IsAny<GetRecordsQuery>(), new CancellationToken()))
                .Callback<IRequest<QueryResponseDTO>, CancellationToken > ((r1,r2) => getRecord = (GetRecordsQuery)r1)
                .ReturnsAsync(new QueryResponseDTO()
                {
                    Records = new List<SystemDataLoadLog>() {
                        new SystemDataLoadLog() { ActualRawTotalAmount = 12.33m, ActualRawTotalRecordCount = 3 } }
                });

            var wrapper = new SystemDataLoadLogWrapper(_mediator.Object);

            //Act

            var test = wrapper.UpdateSfSystemDataLoadLog(3, 12.33m);

            //Assert

            Assert.IsNotNull(getRecord.QueryFilter);
        }

        [TestMethod]
        public void ShouldCompareRecordCount()
        {
            //Arrange

            var getRecord = new GetRecordsQuery();

            _mediator.Setup(a => a.Send(It.IsAny<GetRecordsQuery>(), new CancellationToken()))
                .Callback<IRequest<QueryResponseDTO>, CancellationToken>((r1, r2) => getRecord = (GetRecordsQuery)r1)
                .ReturnsAsync(new QueryResponseDTO()
                {
                    Records = new List<SystemDataLoadLog>() {
                        new SystemDataLoadLog() { ActualRawTotalAmount = 12.33m, ActualRawTotalRecordCount = 3 } }
                });

            var checkAmount = new SystemDataLoadLog();
            _mediator.Setup(a => a.Send(It.IsAny<SystemDataLoadLog>(), new CancellationToken()))
                .Callback<object, CancellationToken>((r1, r2) => checkAmount = (SystemDataLoadLog)r1);

            var wrapper = new SystemDataLoadLogWrapper(_mediator.Object);

            //Act

            var test = wrapper.UpdateSfSystemDataLoadLog(3, 12.33m);

            //Assert

            Assert.AreEqual(6, checkAmount.ActualRawTotalRecordCount);
        }

        [TestMethod]
        public void ShoulCheckThatFlowWasFinished()
        {
            //Arrange
            _mediator.Setup(a => a.Send(It.IsAny<GetRecordsQuery>(), new CancellationToken()))
                .ReturnsAsync(new QueryResponseDTO()
                {
                    Records = new List<SystemDataLoadLog>() {
                        new SystemDataLoadLog() { ActualRawTotalAmount = 12.33m, ActualRawTotalRecordCount = 3 } }
                });

            var wrapper = new SystemDataLoadLogWrapper(_mediator.Object);

            //Act

            var test = wrapper.UpdateSfSystemDataLoadLog(3, 12.33m);

            //Assert
            Assert.IsNotNull(test);
        }
    }
}