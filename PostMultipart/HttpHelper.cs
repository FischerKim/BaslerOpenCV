using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace PostMultipart
{
    public class HttpHelper : IDisposable
    {
        List<byte[]> _datas;
        int _timeout;
        CancellationTokenSource _cts;
        CancellationToken _ct;

        public HttpHelper()
        {
            _datas = new List<byte[]>();
        }
        public void AddData(byte[] data)
        {
            _datas.Add(data);
        }

        public async Task<Result> PostMultipartAsync(string url, int timeout)
        {
            Result result;
            _cts = new CancellationTokenSource(timeout);
            _ct = _cts.Token;

            using (var client = new HttpClient())
            {
                using (var requestContent = new MultipartFormDataContent())
                {
                    foreach (var data in _datas)
                    {
                        var imageContent = new ByteArrayContent(data);
                        imageContent.Headers.ContentType =
                            MediaTypeHeaderValue.Parse("multipart/form-data");
                        requestContent.Add(imageContent, "file", "image.bmp");
                    }
                    var res = await client.PostAsync(url, requestContent, _ct);
                    res.EnsureSuccessStatusCode();
                    result = new Result((int)res.StatusCode, await res.Content.ReadAsStringAsync());
                }
            }
            _datas.Clear();
            _cts.Dispose();
            return result;
        }

        public async Task<Result2> PostMultipartByteAsync(string url, int timeout)
        {
            Result2 result;
            _cts = new CancellationTokenSource(timeout);
            _ct = _cts.Token;

            using (var client = new HttpClient())
            {
                using (var requestContent = new MultipartFormDataContent())
                {
                    foreach (var data in _datas)
                    {
                        var imageContent = new ByteArrayContent(data);
                        imageContent.Headers.ContentType =
                            MediaTypeHeaderValue.Parse("multipart/form-data");
                        requestContent.Add(imageContent, "file", "image.png");
                    }
                    var res = await client.PostAsync(url, requestContent, _ct);
                    res.EnsureSuccessStatusCode();
                    result = new Result2((int)res.StatusCode, await res.Content.ReadAsByteArrayAsync());
                }
            }
            _datas.Clear();
            _cts.Dispose();
            return result;
        }

        public Result PostMultipart(string url, int timeout)
        {
            Result result;
            try
            {
                result = PostMultipartAsync(url, timeout).Result;
            }
            catch
            {
                result = new Result(400, "");
            }

            return result;
        }

        public Result2 PostMultipartByte(string url, int timeout)
        {
            Result2 result;
            try
            {
                result = PostMultipartByteAsync(url, timeout).Result;
            }
            catch
            {
                result = new Result2(400, new byte[0]);
            }

            return result;
        }

        public struct Result
        {
            public Result(int code, string content)
            {
                Code = code;
                Content = content;
            }
            public int Code { get; private set; }
            public string Content { get; private set; }
        }

        public struct Result2
        {
            public Result2(int code, byte[] content)
            {
                Code = code;
                Content = content;
            }
            public int Code { get; private set; }
            public byte[] Content { get; private set; }
        }

        public void Dispose()
        {
            _datas.Clear();
        }
    }
}
