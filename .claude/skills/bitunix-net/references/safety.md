# Operational considerations

## Credentials and environments

Private REST and socket operations use `BitunixCredentials(key, secret)` without a passphrase. Configure futures private socket credentials directly; no listen key needs to be renewed. Keep credentials outside source and grant the permissions needed for the intended operations.

`BitunixEnvironment.Live` is the only named built-in environment. A custom environment requires spot REST/socket and futures REST/socket addresses and does not itself provide simulated trading.

## Orders and position modes

Direct spot placement has no client-order-ID, time-in-force, or quote-quantity parameter. It has no test-order counterpart. Limit orders can execute immediately if the price crosses the book. Cancellation can race with a fill; reconcile order status and executions after an uncertain cancellation.

Futures account reads and leverage operations require a margin asset such as `USDT`. In hedge mode, `TradeSide.Open` or `TradeSide.Close` is required; choose it alongside the intended position and side. In one-way mode, do not imply hedge semantics. Verify current position/margin mode and exposure before setting leverage, adjusting isolated margin, or changing mode. Apply `reduceOnly` according to the selected endpoint's supported semantics.

Futures orders can carry position/client IDs and TP/SL fields. Verify trigger and price types for inline, order, and position TP/SL workflows. `CloseAllPositionsAsync` and `CancelAllOrdersAsync` have broader scope than their single-item counterparts; preserve the requested symbol filter.

## Batch and nested outcomes

Futures cancellation calls returning `BitunixCancelResult` contain `Success` and `Failed` arrays. Outer request `Success` does not prove every order was canceled: inspect each failed item's `ErrorCode` and `ErrorMsg` before reconciling or retrying individual orders.

Spot order history is paginated, and history responses can wrap their records. Read the model's items and pagination information instead of treating every `Data` value as a flat list.

## Transfers and withdrawals

Withdrawals are restricted to addresses added for quick withdrawal. Preserve asset, network, destination, quantity, and optional memo/tag. Transfers use `TransferType` to specify spot/futures direction; verify the source account and available balance. Request success is distinct from completed withdrawal settlement.

## Ambiguous outcomes and retries

A timeout or lost response can occur after Bitunix accepted an order, cancellation, withdrawal, or account-setting change. Query order/transaction/account state before deciding whether to resubmit. Futures client-order-ID lookups can help correlate orders; direct spot placement does not offer that identifier. A transient error alone does not make an account-changing request safe to repeat. Bound public-read retries separately from submission recovery.
