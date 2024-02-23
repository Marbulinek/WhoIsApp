using System.Text;
using Whois.NET;
using WhoIsApp.models;
using WhoIsApp.services;

namespace WhoIsApp
{
    public class WhoisManager
    {
        private WhoisParser _parserService;

        public WhoisManager()
        {
            _parserService = new WhoisParser();
        }

        public Result<WhoIsResult> RetrieveData(string url)
        {
            var result = new Result<WhoIsResult>();
            try
            {
                WhoisResponse response = WhoisClient.Query(url, encoding: Encoding.UTF8);
                if (response != null)
                {
                    result.Payload = _parserService.Parse(response.Raw);
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }
            }catch(Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result; 
        }
    }
}
