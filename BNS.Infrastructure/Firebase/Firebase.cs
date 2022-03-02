
using BNS.Domain.Responses;
using FirebaseAdmin.Auth;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Infrastructure
{
    public static class Firebase
    {
        public static async Task<GoogleApiTokenInfo> CheckTokenGoogle(string token, FirebaseAdmin.FirebaseApp defaultFirebaseApp)
        {
            var result = new GoogleApiTokenInfo();
            FirebaseToken decodedToken = await FirebaseAuth.GetAuth(defaultFirebaseApp)
.VerifyIdTokenAsync(token);
            if (decodedToken == null)
                return null;
            result.sub = decodedToken.Uid;
            result.email = decodedToken.Claims.Where(s => s.Key == "email").FirstOrDefault().Value;
            result.name = decodedToken.Claims.Where(s => s.Key == "name").FirstOrDefault().Value;
            return result;


        }
    }
}
