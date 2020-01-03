using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Denggaopan.ApolloAspNetDemo
{
    public partial class Apollo : Page
    {
        public string Key;
        public string Value;
        protected void Page_Load(object sender, EventArgs e)
        {
            Key = Request["key"];
            Value = string.IsNullOrEmpty(Key) ? "key is null" : Global.Configuration[Key]??"undefined";
        }
    }
}