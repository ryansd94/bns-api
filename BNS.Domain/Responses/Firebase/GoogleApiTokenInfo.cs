using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Domain.Responses
{
    public class GoogleApiTokenInfo
    {

        public string sub { get; set; }

        /// <summary>
        /// The user's email address. This may not be unique and is not suitable for use as a primary key. Provided only if your scope included the string "email".
        /// </summary>
        public object email { get; set; }

        public object name { get; set; }

        /// <summary>
        /// The URL of the user's profile picture. Might be provided when:
        /// The request scope included the string "profile"
        /// The ID token is returned from a token refresh
        /// When picture claims are present, you can use them to update your app's user records. Note that this claim is never guaranteed to be present.
        /// </summary>
        public string picture { get; set; }

        public string given_name { get; set; }

        public string family_name { get; set; }

        public string locale { get; set; }

        public string alg { get; set; }

        public string kid { get; set; }
    }
}
