using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyShushiSiteApi.Core.Authorization
{
    public class AuthOptions
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public const string ISSUER = "PavelKuvshinchikov";
        /// <summary>
        /// Потребитель токена
        /// </summary>
        public const string AUDIENCE = "http://familyfinance/";

        /// <summary>
        /// Ключ шифрования
        /// </summary>
        const string KEY = "supersecretkey!12345";

        /// <summary>
        /// время жизни токена
        /// </summary>
        public const int LIFETIME = 24 * 60;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
