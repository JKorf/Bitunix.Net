using CryptoExchange.Net.Objects.Errors;

namespace Bitunix.Net
{
    internal static class BitunixErrors
    {
        public static ErrorMapping SpotErrors { get; } = new ErrorMapping(
            [
            new ErrorInfo(ErrorType.SystemError, false, "System error", "500"),

            new ErrorInfo(ErrorType.MissingParameter, false, "Parameter error", "70003"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "2"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity error", "10058"),

            new ErrorInfo(ErrorType.Unauthorized, false, "Invalid user", "100004", "10016"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Signature error", "100005"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Request expired", "100008"),
            ]
            );

        public static ErrorMapping Errors { get; } = new ErrorMapping(
            [
            new ErrorInfo(ErrorType.Unauthorized, false, "API key unauthorized", "403"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key not authorized for IP", "10004"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Invalid signature", "10007"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Account not allowed to trade", "20011"),

            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "10005", "10006"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "10002", "10008"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Invalid timestamp", "10011"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Market does not exist", "20001"),
            new ErrorInfo(ErrorType.UnknownOrder, false, "Unknown order", "20007"),
            new ErrorInfo(ErrorType.NoPosition, false, "Position does not exist", "30004"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "20003"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Amount too low", "20008", "30016", "30017"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Max quantity exceeded", "30013", "30039"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Invalid price", "30002", "30003", "30014", "30015"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order ID", "30042"),
            ]
            );
    }
}
