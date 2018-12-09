using System;
using System.Collections.Generic;
using System.Text;

namespace News.Model.Response
{
    public class ResponseModel
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public dynamic Data { get; set; }
    }
}
