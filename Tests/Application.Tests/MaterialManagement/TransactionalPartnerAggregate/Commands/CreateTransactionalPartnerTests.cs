using Application.Interfaces;
using Application.Interfaces.Writes.TransactionalPartnerWrite;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands;
using Application.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate.Commands.CreateTransactionalPartner;
using Domain.SupplyAndProductionManagement.SupplyChainManagement.TransactionalPartnerAggregate;
using FluentAssertions;
using NSubstitute;
using SharedKernel.Enumerations;
using DomainErrors = Domain.SupplyAndProductionManagement.SupplyChainManagement.DomainErrors;

namespace Application.Tests.MaterialManagement.TransactionalPartnerAggregate.Commands;

//https://www.youtube.com/watch?v=a6Qab5l-VLo&t=103s&ab_channel=MilanJovanovi%C4%87
public class CreateTransactionalPartnerTests
{
    private readonly ITransactionalPartnerRepository _transactionalPartnerRepository = Substitute.For<ITransactionalPartnerRepository>();
    private readonly ITransactionalPartnerReadonlyRepository _transactionalPartnerQueryForWrite = Substitute.For<ITransactionalPartnerReadonlyRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CreateTransactionalPartnerCommandHandler _handler;

    public CreateTransactionalPartnerTests()
    {
        _handler = new(_transactionalPartnerRepository, _transactionalPartnerQueryForWrite, _unitOfWork);
    }

    [Fact]
    public async Task Handle_should_return_failure_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        SetupExistByContactInfoAsyncWithReturnTrue();

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.ContactPersonInformation.TelNoOrEmailIsTaken);
    }

    [Fact]
    public async Task Handle_should_not_call_Save_on_repository_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        SetupExistByContactInfoAsyncWithReturnTrue();

        var _ = await _handler.Handle(command, default);

        _transactionalPartnerRepository.DidNotReceive().Save(Arg.Any<TransactionalPartner>());
    }

    [Fact]
    public async Task Handle_should_not_call_SaveChangesAsync_on_UnitOfWork_when_contact_info_existed()
    {
        var command = GetCreateTransactionalCommand();
        SetupExistByContactInfoAsyncWithReturnTrue();

        var _ = await _handler.Handle(command, default);

        await _unitOfWork.DidNotReceive().SaveChangesAsync(default);
    }

    [Fact]
    public async Task Handle_should_return_success_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        SetupExistByContactInfoAsyncWithReturnFalse();

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_should_call_save_on_repository_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        SetupExistByContactInfoAsyncWithReturnFalse();

        var _ = await _handler.Handle(command, default);

        _transactionalPartnerRepository.Received(1).Save(Arg.Any<TransactionalPartner>());
    }

    [Fact]
    public async Task Handle_should_call_SaveChangesAsync_on_UnitOfWork_when_contact_info_does_not_exist()
    {
        var command = GetCreateTransactionalCommand();
        SetupExistByContactInfoAsyncWithReturnFalse();

        var _ = await _handler.Handle(command, default);

        await _unitOfWork.Received(1).SaveChangesAsync(default);
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

    private void SetupExistByContactInfoAsyncWithReturnTrue()
    {
        _transactionalPartnerQueryForWrite
            .ExistByContactInfoAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
    }
    
    private void SetupExistByContactInfoAsyncWithReturnFalse()
    {
        _transactionalPartnerQueryForWrite
            .ExistByContactInfoAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
    }
}