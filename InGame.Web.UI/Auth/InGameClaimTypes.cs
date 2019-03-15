using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.UI.Auth
{
    public class InGameClaimTypes
    {
        public static List<string> ClaimsList { get; set; } = new List<string> { "Delete Product", "Add Product", "product_view" };
    }
}
