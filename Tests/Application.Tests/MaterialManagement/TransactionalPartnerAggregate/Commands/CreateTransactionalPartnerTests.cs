using Application.Interfaces;
using Application.Interfaces.Reads;
using Application.Interfaces.Write;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel.Enumerations;
using FluentAssertions;
using Moq;
using DomainErrors = Domain.MaterialManagement.DomainErrors;

namespace Application.Tests.MaterialManagement.TransactionalPartnerAggregate.Commands;

//https://www.youtube.com/watch?v=a6Qab5l-VLo&t=103s&ab_channel=MilanJovanovi%C4%87
public class CreateTransactionalPartnerTests
{
    private readonly Mock<ITransactionalPartnerRepository> _transactionalPartnerRepositoryMock;
    private readonly Mock<ITransactionalPartnerQuery> _transactionalPartnerQueryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    
    public CreateTransactionalPartnerTests()
    {
        _transactionalPartnerRepositoryMock = new Mock<ITransactionalPartnerRepository>();
        _transactionalPartnerQueryMock = new Mock<ITransactionalPartnerQuery>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }
    
    [Fact]
    public async Task Handle_should_return_failure_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var result = await commandHandler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken);
    }
    
    [Fact]
    public async Task Handle_should_not_call_Save_on_repository_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = await commandHandler.Handle(command, default);

        _transactionalPartnerRepositoryMock.Verify(
            x => x.Save(It.IsAny<TransactionalPartner>()), 
            Times.Never);
    }
    
    [Fact]
    public async Task Handle_should_not_call_SaveChangesAsync_on_UnitOfWork_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = await commandHandler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(default), 
            Times.Never);
    }
    
    [Fact]
    public async Task Handle_should_return_success_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var result = await commandHandler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task Handle_should_call_save_on_repository_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = await commandHandler.Handle(command, default);

        _transactionalPartnerRepositoryMock.Verify(
            x => x.Save(It.IsAny<TransactionalPartner>()), 
            Times.Once);
    }
    
    [Fact]
    public async Task Handle_should_call_SaveChangesAsync_on_UnitOfWork_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = await commandHandler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(default), 
            Times.Once);
    }

    private static CreateTransactionalPartnerCommand GetCreateTransactionalCommand()
    {
        var address = new AddressCommand()
        {
            City = "city",
            District = "district",
            Street = "street",
            Ward = "ward",
            ZipCode = "12345",
            CountryId = Country.VietNam.Id
        };
        var command = new CreateTransactionalPartnerCommand
        (
            "Name 1",
            "1234567890",
            string.Empty,
            "aabbcc ddd",
            string.Empty,
            "abcxxx@gmail.com",
            address,
            TransactionalPartnerType.Supplier.Id,
            CurrencyType.VND.Id,
            LocationType.Domestic.Id
        );

        return command;
    }
}