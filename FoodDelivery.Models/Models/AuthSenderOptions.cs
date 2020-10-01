using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Models
{
    public class AuthSenderOptions
    {
        private string user = "Alden Park";

        private string key = "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";

        public string SendGridUser { get { return user; } }

        public string SendGridKey { get { return key; } }
    }

}
