using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace UniSharp.Common.Utilities
{
    public struct RequestResult
    {
        private string _result;
        private HttpStatusCode _resultCode;

        public string ResultString => _result;
        public HttpStatusCode ResultCode => _resultCode;

        public bool Success => _resultCode == HttpStatusCode.OK;

        public RequestResult(string result, HttpStatusCode statusCode)
        {
            _result = result;
            _resultCode = statusCode;
        }
    }

    public static class HttpUtility
    {
        public static RequestResult GetHttp(string url, params KeyValuePair<string, string>[] headers)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            if (headers != null && headers.Length > 0)
            {
                int length = headers.Length;
                for (int i = 0; i < length; i++)
                    request.Headers.Add(headers[i].Key, headers[i].Value);
            }

            //request.Timeout = 4500;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                string responseString = string.Empty;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
                else
                {
                    responseString = response.StatusDescription;
                }
                RequestResult result = new RequestResult(responseString, response.StatusCode);
                response.Close();
                return result;
            }
            catch (WebException webException)
            {
                var result = HandleWebException(webException);
                response?.Close();
                return result;
            }
            catch (Exception E)
            {
                response?.Close();
#if DEVELOPMENT
                Debug.LogWarning(E.ToString());
#endif
                return new RequestResult(E.ToString(), HttpStatusCode.ExpectationFailed);
            }
        }

        public static RequestResult PostHttpJson<T>(string url, T data, byte[] hashCode = null, Encoding encoding = null, params KeyValuePair<string, string>[] headers)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            string jsonString = string.Empty;
            if (data is string)
                jsonString = data as string;
            else
                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            //#if DEVELOPMENT
            //            Debug.Log(jsonString);
            //#endif
            byte[] bytes = encoding.GetBytes(jsonString);
            HttpWebRequest request = WebRequest.CreateHttp(url);
            if (headers != null && headers.Length > 0)
            {
                int length = headers.Length;
                for (int i = 0; i < length; i++)
                    request.Headers.Add(headers[i].Key, headers[i].Value);
            }
            request.ContentType = "application/json";
            request.Method = "POST";
            request.ContentLength = bytes.Length;

            if (hashCode != null)
            {
                byte[] hash = null;
                using (HMACSHA256 encription = new HMACSHA256(hashCode))
                {
                    hash = encription.ComputeHash(bytes);
                }
                string hashString = Convert.ToBase64String(hash);
                request.Headers.Add(HttpRequestHeader.Authorization, hashString);
            }
            HttpWebResponse response = null;
            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
                string responseString = string.Empty;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
                else
                {
                    responseString = response.StatusDescription;
                }
                RequestResult result = new RequestResult(responseString, response.StatusCode);
                response.Close();
                return result;
            }
            catch (WebException webException)
            {
                var result = HandleWebException(webException);
                response?.Close();
                return result;
            }
            catch (Exception E)
            {
                response?.Close();
#if DEVELOPMENT
                Debug.LogWarning(E.ToString());
#endif
                return new RequestResult(E.ToString(), HttpStatusCode.ExpectationFailed);
            }
        }

        public static RequestResult PostHttpText(string url, string data, byte[] hashCode = null, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
#if DEVELOPMENT
            Debug.Log(data);
#endif
            byte[] bytes = encoding.GetBytes(data);
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.ContentLength = bytes.Length;
            request.ContentType = "text/plain";
            request.Method = "POST";
            if (hashCode != null)
            {
                byte[] hash = null;
                using (HMACSHA256 encription = new HMACSHA256(hashCode))
                {
                    hash = encription.ComputeHash(bytes);
                }
                string hashString = Convert.ToBase64String(hash);
                request.Headers.Add(HttpRequestHeader.Authorization, hashString);
            }
            HttpWebResponse response = null;
            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
                string responseString = string.Empty;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
                else
                {
                    responseString = response.StatusDescription;
                }
                RequestResult result = new RequestResult(responseString, response.StatusCode);
                response.Close();
                return result;
            }
            catch (WebException webException)
            {
                var result = HandleWebException(webException);
                response?.Close();
                return result;
            }
            catch (Exception E)
            {
                response?.Close();
#if DEVELOPMENT
                Debug.LogWarning(E.ToString());
#endif
                return new RequestResult(E.ToString(), HttpStatusCode.ExpectationFailed);
            }
        }

        public static RequestResult HandleWebException(WebException webException)
        {
#if DEVELOPMENT
            Debug.LogWarning(webException.Status + " , " + webException.Message);
#endif
            HttpStatusCode code;
            if (webException.Response != null)
            {
                code = ((HttpWebResponse)webException.Response).StatusCode;
                webException.Response.Close();
            }
            else
            {
                code = HttpStatusCode.NotFound;
                if (webException.Status == WebExceptionStatus.ConnectFailure)
                    code = HttpStatusCode.ServiceUnavailable;
            }
            return new RequestResult(webException.Message, code);
        }

    }
}
