using AutoMapper;
using ErrorOr;
using GadgetLand.Application.Common.Errors;
using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Users.Commands.UpdateUserInfo;

public class UpdateUserInfoCommandHandler(
    IUsersRepository usersRepository,
    ISecurityService securityService,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserInfoCommand, ErrorOr<OperationResponse>>
{
    public async Task<ErrorOr<OperationResponse>> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(Convert.ToInt32(securityService.GetUserIdFromToken()));

        if (user is null) return UserErrors.NotFound;

        var updatedUser = mapper.Map(request, user);

        usersRepository.Update(updatedUser);
        await unitOfWork.CommitChangesAsync();

        return new OperationResponse("ذخیره تغییرات با موفقیت انجام شد.");
    }
}
