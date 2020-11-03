﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lavanderia.Rest
{
    public enum httpVerb { 
    GET,
    POST,
    PUT,
    DELETE
    
    }
    class RestClient
    {
        public string endPoint { get; set;}
        public httpVerb httpMethod { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public string makeRequest() { 

        string strResponseValue =string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

        request.Method = httpMethod.ToString();

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) 
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException("error Code" + response.StatusCode.ToString());
            }
            using (Stream responseStream = response.GetResponseStream()) {
                if (responseStream != null)
                { 
                using(StreamReader reader = new StreamReader(responseStream)){

                    strResponseValue = reader.ReadToEnd();
                }
                
                }
            
            }
        
        }

        return strResponseValue;
        }
    }
}
