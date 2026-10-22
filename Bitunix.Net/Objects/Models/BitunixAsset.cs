using Bitunix.Net.Enums;
using System.Text.Json.Serialization;

namespace Bitunix.Net.Objects.Models;

/// <summary>
/// Asset info
/// </summary>
public record BitunixAsset
{
    /// <summary>
    /// ["<c>name</c>"] Name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>fullName</c>"] Full name
    /// </summary>
    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>logo</c>"] Logo
    /// </summary>
    [JsonPropertyName("logo")]
    public string Logo { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>track</c>"] Track
    /// </summary>
    [JsonPropertyName("track")]
    public string Track { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>isHot</c>"] Is hot
    /// </summary>
    [JsonPropertyName("isHot")]
    public bool IsHot { get; set; }
    /// <summary>
    /// ["<c>isOpen</c>"] Is open
    /// </summary>
    [JsonPropertyName("isOpen")]
    public bool IsOpen { get; set; }
    /// <summary>
    /// ["<c>isShow</c>"] Is show
    /// </summary>
    [JsonPropertyName("isShow")]
    public bool IsShow { get; set; }
    /// <summary>
    /// ["<c>depositOpen</c>"] Deposit open
    /// </summary>
    [JsonPropertyName("depositOpen")]
    public bool DepositOpen { get; set; }
    /// <summary>
    /// ["<c>withdrawOpen</c>"] Withdraw open
    /// </summary>
    [JsonPropertyName("withdrawOpen")]
    public bool WithdrawOpen { get; set; }
    /// <summary>
    /// ["<c>withdrawMin</c>"] Withdraw minimum
    /// </summary>
    [JsonPropertyName("withdrawMin")]
    public decimal WithdrawMin { get; set; }
    /// <summary>
    /// ["<c>withdrawMax</c>"] Withdraw max
    /// </summary>
    [JsonPropertyName("withdrawMax")]
    public decimal? WithdrawMax { get; set; }
    /// <summary>
    /// ["<c>networks</c>"] Networks
    /// </summary>
    [JsonPropertyName("networks")]
    public BitunixAssetNetwork[] Networks { get; set; } = [];
}

/// <summary>
/// Network info
/// </summary>
public record BitunixAssetNetwork
{
    /// <summary>
    /// ["<c>contractPrecision</c>"] Contract precision
    /// </summary>
    [JsonPropertyName("contractPrecision")]
    public int Precision { get; set; }
    /// <summary>
    /// ["<c>contractAddress</c>"] Contract address
    /// </summary>
    [JsonPropertyName("contractAddress")]
    public string ContractAddress { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>requireMemo</c>"] Require memo
    /// </summary>
    [JsonPropertyName("requireMemo")]
    public MemoRequired RequireMemo { get; set; }
    /// <summary>
    /// ["<c>chain</c>"] Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>depositConfirm</c>"] Deposit confirm
    /// </summary>
    [JsonPropertyName("depositConfirm")]
    public int? DepositConfirm { get; set; }
    /// <summary>
    /// ["<c>addressRegular</c>"] Address regex
    /// </summary>
    [JsonPropertyName("addressRegular")]
    public string AddressRegex { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>network</c>"] Network full name
    /// </summary>
    [JsonPropertyName("network")]
    public string NetworkName { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>isOpen</c>"] Is open
    /// </summary>
    [JsonPropertyName("isOpen")]
    public bool IsOpen { get; set; }
    /// <summary>
    /// ["<c>isShow</c>"] Is show
    /// </summary>
    [JsonPropertyName("isShow")]
    public bool IsShow { get; set; }
    /// <summary>
    /// ["<c>depositOpen</c>"] Deposit open
    /// </summary>
    [JsonPropertyName("depositOpen")]
    public bool DepositOpen { get; set; }
    /// <summary>
    /// ["<c>withdrawOpen</c>"] Withdraw open
    /// </summary>
    [JsonPropertyName("withdrawOpen")]
    public bool WithdrawOpen { get; set; }
    /// <summary>
    /// ["<c>withdrawMax</c>"] Withdraw max
    /// </summary>
    [JsonPropertyName("withdrawMax")]
    public decimal? WithdrawMax { get; set; }
    /// <summary>
    /// ["<c>withdrawMin</c>"] Withdraw min
    /// </summary>
    [JsonPropertyName("withdrawMin")]
    public decimal WithdrawMin { get; set; }
    /// <summary>
    /// ["<c>depositMin</c>"] Deposit min
    /// </summary>
    [JsonPropertyName("depositMin")]
    public decimal DepositMin { get; set; }
}

