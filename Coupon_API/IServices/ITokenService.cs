using Coupon_API.Models;

namespace Coupon_API.IServices;

public interface ITokenService
{
    string GerarToken(string key, string issuer, string audience, UserLogin user);
}
