using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNTSlipPortal_Repository.DTO
{
    public class AutoCompleteResponse
    {
        public AutoCompleteResponse(string id, string text)
        {
            this.id = id;
            this.text = text;
        }
        public string id { get; set; }
        public string text { get; set; }
    }
}
