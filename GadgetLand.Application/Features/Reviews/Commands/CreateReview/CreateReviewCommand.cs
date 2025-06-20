﻿using ErrorOr;
using GadgetLand.Contracts;
using MediatR;

namespace GadgetLand.Application.Features.Reviews.Commands.CreateReview;

public record CreateReviewCommand(int ProductId, double Rating, string Comment) : IRequest<ErrorOr<OperationResponse>>;
