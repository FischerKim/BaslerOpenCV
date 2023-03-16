using System;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using PostMultipart;


namespace PU1
{
    public class CRestfulAPI : IDisposable
    {
        #region VARIABLE
        private HttpHelper m_OHelper;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public CRestfulAPI()
        {
            try
            {
                this.m_OHelper = new HttpHelper();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }

        ~CRestfulAPI()
        {
            this.Dispose();   
        }
        #endregion

        #region FUNCTION
        public void Dispose()
        {
            try 
            { 
           // this.m_OClient.Dispose();
            this.m_OHelper.Dispose();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }

        }

        public async Task<HttpHelper.Result> PostImage(Bitmap OImage)
        {
            ImageConverter converter = new ImageConverter();

            var ByteImage = converter.ConvertTo(OImage, typeof(byte[])) as byte[];//await File.ReadAllBytesAsync(path);

            this.m_OHelper.AddData(ByteImage);
            var response = this.m_OHelper.PostMultipart("http://192.168.1.150:8001/smaseq/upload", 10000);
            return response;
        }

        public async Task<HttpHelper.Result> GetLicenseInfo()
        {
            string StrTargetURL = "http://192.168.1.150:7788/license_info";
            return await Get(StrTargetURL);
        }

        public async Task<HttpHelper.Result> Reboot()
        {
            string StrTargetURL = "http://192.168.1.150:17999/reboot";
            return this.m_OHelper.PostMultipart(StrTargetURL, 10000);
        }

        public async Task<HttpHelper.Result> Get(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                   // return await response.Content.ReadAsStringAsync();
                   return new HttpHelper.Result((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception($"Failed to GET from {url}. Response status code: {response.StatusCode}");
                }
            }
        }

        //public async Task<string> Post(string url, string jsonContent)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync(url, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return await response.Content.ReadAsStringAsync();
        //        }
        //        else
        //        {
        //            throw new Exception($"Failed to POST to {url}. Response status code: {response.StatusCode}");
        //        }
        //    }
        //}
        #endregion
    }


}
