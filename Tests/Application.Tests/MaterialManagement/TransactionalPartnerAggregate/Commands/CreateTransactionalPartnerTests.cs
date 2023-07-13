using Application.Interfaces;
using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands;
using Application.MaterialManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Domain.Errors;
using Domain.MaterialManagement.TransactionalPartnerAggregate;
using Domain.SharedKernel;
using FluentAssertions;
using Moq;

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
    public void Handle_should_return_failure_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var result = commandHandler.Handle(command, default).Result;

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken);
    }
    
    [Fact]
    public void Handle_should_not_call_Save_on_repository_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = commandHandler.Handle(command, default).Result;

        _transactionalPartnerRepositoryMock.Verify(
            x => x.Save(It.IsAny<TransactionalPartner>()), 
            Times.Never);
    }
    
    [Fact]
    public void Handle_should_not_call_SaveChangesAsync_on_UnitOfWork_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = commandHandler.Handle(command, default).Result;

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(default), 
            Times.Never);
    }
    
    [Fact]
    public void Handle_should_return_success_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var result = commandHandler.Handle(command, default).Result;

        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void Handle_should_call_save_on_repository_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = commandHandler.Handle(command, default).Result;

        _transactionalPartnerRepositoryMock.Verify(
            x => x.Save(It.IsAny<TransactionalPartner>()), 
            Times.Once);
    }
    
    [Fact]
    public void Handle_should_call_SaveChangesAsync_on_UnitOfWork_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        _transactionalPartnerQueryMock.Setup(
            x => x.ExistByContactInfoAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var commandHandler = new CreateTransactionalPartnerCommandHandler(_transactionalPartnerRepositoryMock.Object,
            _transactionalPartnerQueryMock.Object, _unitOfWorkMock.Object);

        var _ = commandHandler.Handle(command, default).Result;

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
        {
            Name = "Name 1",
            TaxNo = "1234567890",
            Website = string.Empty,
            ContactPersonName = "aabbcc ddd",
            TelNo = string.Empty,
            Email = "abcxxx@gmail.com",
            Address = address,
            TransactionalPartnerTypeId = TransactionalPartnerType.Supplier.Id,
            CurrencyTypeId = CurrencyType.VND.Id,
            LocationTypeId = LocationType.Domestic.Id
        };

        return command;
    }
}