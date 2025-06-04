namespace GadgetLand.Contracts.Reviews;

public record CreateReviewRequest(int ProductId, double Rating, string Comment);
