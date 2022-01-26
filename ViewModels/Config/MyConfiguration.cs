using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels
{
    public class MyConfiguration
    {
        public DefaultElement Default { get; set; } = new DefaultElement();
        public TokenElement Tokens { get; set; } = new TokenElement();
        public ConnectionElement ConnectionStrings { get; set; } = new ConnectionElement();
        public FirebaseElement Firebase { get; set; } = new FirebaseElement();
        public Elasticsearch Elasticsearch { get; set; } = new Elasticsearch();

        
    }
    public class Elasticsearch
    {
        public string url { get; set; } = "https://elastic:FzHAi3MM3OFitgTkPT9LVzeM@bns.es.us-east4.gcp.elastic-cloud.com:9243/";
        public string index { get; set; } = "bns";
    }
    public class FirebaseElement
    {
        public string FireBaseAccount { get; set; } = "ConfigFile/firebase-account.json";
    }
    public class TokenElement
    {
        public string Key { get; set; } = "0123456789ABCDEF";
        public string Issuer { get; set; } = "https://webapi.tedu.com.vn";
    }
    public class ConnectionElement
    {
        public string bnsConnection { get; set; } = "Data Source=125.212.226.105,1968;Initial Catalog=test_bidv_2;User ID=sa;Password=TGn<@7qY";

        public string jmConnection { get; set; } = "Data Source=125.212.226.105,1968;Initial Catalog=test_bidv_3;User ID=sa;Password=TGn<@7qY";

    }
}
