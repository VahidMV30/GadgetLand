using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Payments;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace GadgetLand.Infrastructure.Services;

public class ZarinPalPaymentService(IConfiguration configuration, HttpClient httpClient) : IPaymentService
{
    private readonly string _merchantId = configuration["ZarinPal:MerchantId"]!;
    private readonly string _callbackUrl = configuration["ZarinPal:CallbackUrl"]!;

    public async Task<CreatePaymentResult> CreatePaymentAsync(long amount)
    {
        var payload = new
        {
            merchant_id = _merchantId,
            amount,
            callback_url = _callbackUrl,
            description = "گجت لند - خرید اینترنتی"
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("https://sandbox.zarinpal.com/pg/v4/payment/request.json", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreatePaymentResponse>(responseString);

        if (result?.Data?.Code != 100)
        {
            return new CreatePaymentResult
            {
                IsSuccess = false,
                Message = "خطا در ایجاد درخواست پرداخت. لطفاً بعداً دوباره تلاش کنید."
            };
        }

        var authority = result.Data.Authority;
        var paymentUrl = $"https://sandbox.zarinpal.com/pg/StartPay/{authority}";

        return new CreatePaymentResult
        {
            IsSuccess = true,
            Authority = authority,
            PaymentUrl = paymentUrl
        };
    }

    public async Task<VerifyPaymentResult> VerifyPaymentAsync(long amount, string authority)
    {
        var payload = new
        {
            merchant_id = _merchantId,
            amount,
            authority
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("https://sandbox.zarinpal.com/pg/v4/payment/verify.json", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<VerifyPaymentResponse>(responseString);

        if (result?.Data is null) return new VerifyPaymentResult { IsSuccess = false, Message = "پاسخی معتبر از زرین‌پال دریافت نشد. لطفاً دوباره تلاش کنید." };

        if (result.Data.Code == 100) return new VerifyPaymentResult { IsSuccess = true, Message = "پرداخت با موفقیت انجام شد." };

        return new VerifyPaymentResult
        {
            IsSuccess = false,
            Message = "پرداخت با شکست مواجه شد. در صورت کسر وجه از حساب شما، مبلغ مربوطه حداکثر ظرف ۷۲ ساعت کاری به حساب شما بازخواهد گشت."
        };
    }
}

internal record CreatePaymentResponse
{
    public CreatePaymentData? Data { get; set; }
    public List<object>? Errors { get; set; }
}

internal record CreatePaymentData
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("authority")]
    public string Authority { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("ref_id")]
    public long RefId { get; set; }

    [JsonProperty("fee_type")]
    public string FeeType { get; set; } = string.Empty;

    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}

internal record VerifyPaymentResponse
{
    public VerifyPaymentData? Data { get; set; }
    public List<VerifyPaymentError>? Errors { get; set; }
}

internal record VerifyPaymentData
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("card_hash")]
    public string CardHash { get; set; } = string.Empty;

    [JsonProperty("card_pan")]
    public string CardPan { get; set; } = string.Empty;

    [JsonProperty("ref_id")]
    public long RefId { get; set; }
}

internal record VerifyPaymentError
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}
