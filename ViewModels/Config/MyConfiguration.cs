﻿using System;
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
        public MailConfig MailConfig { get; set; } = new MailConfig();
        public RabbitMQ RabbitMQ { get; set; } = new RabbitMQ();


    }
    public class MailConfig
    {
        public string SmtpUserName { get; set; } = "noreply@ayasan-service.com";
        public string SmtpPassword { get; set; } = "@y@5@nN0R3p/y";
        public string SmtpHost { get; set; } = "cpanel02wh.bkk1.cloud.z.com";
        public string SmtpPort { get; set; } = "587";
        public string SmtpEmailAddress { get; set; } = "noreply@ayasan-service.com";

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
    public class RabbitMQ
    {
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Hostnames { get; set; }
    }
}
