using System;
using System.Net.Http;
using System.Collections.Generic;

using Newtonsoft.Json;
using PhoenixFutureApiSdk.Model;
using System.Threading.Tasks;

using System.Text;
using DataLayer.Api.Request;
using DataLayer.Api.Response;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

namespace PhoenixFutureApiSdk
{
    public class FutureSdk
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        public FutureSdk(string domain)
        {
            _client = new HttpClient();
            _baseUrl = string.Format(domain) + "/{0}";
        }

        #region Products
        public async Task<ResponseModel<List<ProductAndServiceDto>>> GetAllProducts(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/ProductAndService")).Result;
                return await ResponseHandler<List<ProductAndServiceDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ProductAndServiceDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> AddProduct(string token, ProductAndServiceDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/ProductAndService"), stringContent).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> EditeProduct(string token, ProductAndServiceDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PutAsync(string.Format(_baseUrl, "api/v1/ProductAndService"), stringContent).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> RemoveProduct(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.DeleteAsync(string.Format(_baseUrl, "api/v1/ProductAndService/" + id)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<ProductAndServiceDto>> GetProductByID(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/ProductAndService/" + id)).Result;
                return await ResponseHandler<ProductAndServiceDto>(response);

            }
            catch
            {
                return new ResponseModel<ProductAndServiceDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion
        #region Clients 
        public async Task<ResponseModel<List<ClientDto>>> GetAllClients(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Client")).Result;
                return await ResponseHandler<List<ClientDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ClientDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<ClientDto>> GetClientById(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Client/" + id)).Result;
                return await ResponseHandler<ClientDto>(response);

            }
            catch
            {
                return new ResponseModel<ClientDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<ClientDto>> AddClient(string token, ClientDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Client"), stringContent).Result;
                return await ResponseHandler<ClientDto>(response);

            }
            catch
            {
                return new ResponseModel<ClientDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<ClientDto>> EditeClient(string token, ClientDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PutAsync(string.Format(_baseUrl, "api/v1/Client/" + dto.id), stringContent).Result;
                return await ResponseHandler<ClientDto>(response);

            }
            catch
            {
                return new ResponseModel<ClientDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> RemoveClient(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.DeleteAsync(string.Format(_baseUrl, "api/v1/Client/" + id)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion
        #region Bank
        public async Task<ResponseModel<List<BankDto>>> GetAllBanks(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Bank")).Result;
                return await ResponseHandler<List<BankDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<BankDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> AddBank(string token, BankDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Bank"), stringContent).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> EditeBank(string token, BankDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PutAsync(string.Format(_baseUrl, "api/v1/Bank/" + dto.id), stringContent).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<BankDto>> GetBankById(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Bank/" + id)).Result;
                return await ResponseHandler<BankDto>(response);

            }
            catch
            {
                return new ResponseModel<BankDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion
        #region Factor
        public async Task<ResponseModel<int>> GetAllLastFactorNumber(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/GetLastFactorID")).Result;
                return await ResponseHandler<int>(response);

            }
            catch
            {
                return new ResponseModel<int>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<FactorDto>>> GetAllFactor(string token, GetFactor dto)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/GetFactors"), stringContent).Result;
                return await ResponseHandler<List<FactorDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<FactorDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<FactorDto>>> GetAllFactorinMonth(string token)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var factordto = new GetFactor()
                {
                    ClientID = 0,
                    EndDate = (pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetDayOfMonth(DateTime.Now)).ToString(),
                    StartDate = (pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/01").ToString(),
                    ISAllType = false,
                    FactorType = FactorType.Factor
                };
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(factordto), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/GetFactors"), stringContent).Result;
                return await ResponseHandler<List<FactorDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<FactorDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> GetFactorById(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/GetFactor/" + id)).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> AddPishFactor(string token, AddFactorViewModel dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/AddPishFactor"), stringContent).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> AddPartnerPishFactor(string token,int id, AddFactorViewModel dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Expert/ChangeExpertToFactor/"+id), stringContent).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> AddFactor(string token, AddFactorViewModel dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/AddFactor"), stringContent).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> EditeFactor(string token, int FactorId, AddFactorViewModel dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/UpdateFactor/" + FactorId), stringContent).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> EditePishFactor(string token, int FactorId, AddFactorViewModel dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/UpdatePishFactor/" + FactorId), stringContent).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> CheangToFactor(string token, int FactorId)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/CheangToFactor/" + FactorId)).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<FactorDto>> CheangToPishFactor(string token, int FactorId)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/CheangToPishFactor/" + FactorId)).Result;
                return await ResponseHandler<FactorDto>(response);

            }
            catch
            {
                return new ResponseModel<FactorDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> deleteFactor(string token, int FactorId)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.DeleteAsync(string.Format(_baseUrl, "api/v1/Accounting/DeleteFactor/" + FactorId)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<FactorAttachment>>> GetFactorAttachment(string token, int FactorId)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/GetFactorAttachment/" + FactorId)).Result;
                return await ResponseHandler<List<FactorAttachment>>(response);

            }
            catch
            {
                return new ResponseModel<List<FactorAttachment>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<ShopReportDto>>> getyearsshop(string token, int year)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/getyearsshop/" + year)).Result;
                return await ResponseHandler<List<ShopReportDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ShopReportDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion
        #region Sanad
        public async Task<ResponseModel<List<AccountingHeading>>> GetHeadingTitles(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/GetHeadingTitles")).Result;
                return await ResponseHandler<List<AccountingHeading>>(response);

            }
            catch
            {
                return new ResponseModel<List<AccountingHeading>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<AccountingHeading>>> GetAccountingHeading(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/GetHeadingSanads")).Result;
                return await ResponseHandler<List<AccountingHeading>>(response);

            }
            catch
            {
                return new ResponseModel<List<AccountingHeading>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<SanadHeadingDto>> GetSanadById(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Accounting/GetSanadById/" + id)).Result;
                return await ResponseHandler<SanadHeadingDto>(response);

            }
            catch
            {
                return new ResponseModel<SanadHeadingDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<SanadHeadingDto>> AddSanad(string token, sanadViewModel dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/AddSanad"), stringContent).Result;
                return await ResponseHandler<SanadHeadingDto>(response);

            }
            catch
            {
                return new ResponseModel<SanadHeadingDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<SanadHeadingDto>> EditSanad(string token, int id, sanadViewModel dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/UpdateSanad/" + id), stringContent).Result;
                return await ResponseHandler<SanadHeadingDto>(response);

            }
            catch
            {
                return new ResponseModel<SanadHeadingDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> DeletSanad(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.DeleteAsync(string.Format(_baseUrl, "api/v1/Accounting/DeletSanad/" + id)).Result;
                return await ResponseHandler<SanadHeadingDto>(response);

            }
            catch
            {
                return new ResponseModel<SanadHeadingDto>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<SanadDto>>> GetClientMoein(string token, ClientMoein dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/ClientMoein"), stringContent).Result;
                return await ResponseHandler<List<SanadDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<SanadDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<SanadDto>>> GetMoein(string token, ClientMoein dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/AccountingHeadingMoein"), stringContent).Result;
                return await ResponseHandler<List<SanadDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<SanadDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<SanadAccountingDto>>> GetAccounting(string token, AcountingReviw dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/Accounting"), stringContent).Result;
                return await ResponseHandler<List<SanadAccountingDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<SanadAccountingDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<ClientStatusDto>>> ClientsAccountingReport(string token, ClientsAccpuntingReports dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/ClientsAccountingReport"), stringContent).Result;
                return await ResponseHandler<List<ClientStatusDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ClientStatusDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<ClientStatusDto>>> GetFactorsReport(string token, ClientsAccpuntingReports dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/ClientsAccountingReport"), stringContent).Result;
                return await ResponseHandler<List<ClientStatusDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ClientStatusDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> AddFactorAttachment(string token,int factorid, IFormFile dto,string filepath)
        {


            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var documentToSend = System.IO.File.ReadAllBytes(filepath);
                using (var multipartFormDataContent = new MultipartFormDataContent())
                {
                    multipartFormDataContent.Add(new ByteArrayContent(documentToSend)
                    {
                        Headers =
                    {
                        ContentLength = dto.Length,
                        ContentType = new MediaTypeHeaderValue(dto.ContentType)
                    },
                    },
                      "file",
                      '"' + "image.jpg" + '"');
                    var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Accounting/AddFactorAttachment/" + factorid), multipartFormDataContent).Result;
                    return await ResponseHandler(response);

                }
            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> DeleteAcctachFile(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.DeleteAsync(string.Format(_baseUrl, "api/v1/Accounting/RemoveFactorAttachment/"+id)).Result;
                return await ResponseHandler<List<ClientStatusDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ClientStatusDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion
        #region Clients
        public async Task<ResponseModel<UserModel>> GetUserBio(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Users/GetCorentUserBio")).Result;
                return await ResponseHandler<UserModel>(response);

            }
            catch
            {
                return new ResponseModel<UserModel>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<TokenResponse>> GetToken(TokenPost request)
        {
            try
            {

                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var formContent = new FormUrlEncodedContent(new[]
                {new KeyValuePair<string, string>("grant_type", "password"),
                 new KeyValuePair<string, string>("username", request.username),
                 new KeyValuePair<string, string>("password", request.password)});
                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Users/Token"), formContent).Result;

                var content = await response.Content.ReadAsStringAsync();
                var s = JsonConvert.DeserializeObject<TokenResponse>(content);
                if (!string.IsNullOrEmpty(s.access_token))
                {
                    return new ResponseModel<TokenResponse>()
                    {
                        Data = s,
                        IsSuccess = true,
                        StatusCode = ResponseStatus.Success
                    };
                }
                return new ResponseModel<TokenResponse>
                {
                    Message = "اطلاعات وارد شده معتبر نمی باشد",
                    StatusCode = ResponseStatus.LogicError
                };
            }
            catch
            {
                return new ResponseModel<TokenResponse>
                {
                    Message = "مشکل در ارتباط با سرور",
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<UserModel>>> GetAllUsers(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Users")).Result;
                return await ResponseHandler<List<UserModel>>(response);

            }
            catch
            {
                return new ResponseModel<List<UserModel>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<UserModel>> addUser(string token, UserDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Users/CreateUser"), stringContent).Result;
                return await ResponseHandler<UserModel> (response);

            }
            catch
            {
                return new ResponseModel<UserModel>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<UserModel>> updateUser(string token,int userid, UserDto dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

                var response = _client.PutAsync(string.Format(_baseUrl, "api/v1/Users/Update/" + userid), stringContent).Result;
                return await ResponseHandler<UserModel>(response);

            }
            catch
            {
                return new ResponseModel<UserModel>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<UserModel>> setnewpasswordUser(string token, int userid, string password)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Users/setPassword/" + userid +"/"+ password)).Result;
                return await ResponseHandler<UserModel>(response);

            }
            catch
            {
                return new ResponseModel<UserModel>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> deletUser(string token, int userid)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.DeleteAsync(string.Format(_baseUrl, "api/v1/Users/Delete/" + userid)).Result;
                return await ResponseHandler<UserModel>(response);

            }
            catch
            {
                return new ResponseModel<UserModel>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<Roll>>> GetRolls(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Users/GeAllRolls")).Result;
                return await ResponseHandler<List<Roll>>(response);

            }
            catch
            {
                return new ResponseModel<List<Roll>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<UserModel>>> GetUsersInRoll(string RollName,string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Users/GetUsersInRoll/"+ RollName)).Result;
                return await ResponseHandler<List<UserModel>>(response);

            }
            catch
            {
                return new ResponseModel<List<UserModel>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> addUserstoRolls(int id,string Roll, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent Rolls = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Roll), Encoding.UTF8, "application/json");

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Users/addUserstoRolls/" + id+"/"+ Roll)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> removeUsersfromRolls(int id, string Roll, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent Rolls = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Roll), Encoding.UTF8, "application/json");

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Users/removeUsersfromRolls/" + id + "/" + Roll)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion
        #region Manufactory
        public async Task<ResponseModel<List<ManufactureDto>>> GetGetManufacture(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Manufactore/GetManufacture")).Result;
                return await ResponseHandler<List<ManufactureDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ManufactureDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<ManufactureDto>>> GetManufacturebytime(string token, ManufactureRequest dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Manufactore/GetManufacturebytime"), stringContent).Result;
                return await ResponseHandler<List<ManufactureDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ManufactureDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> addManufactureHistury(string token , ManufactureHistoryAdd dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Manufactore/AddHistory"), stringContent).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion
        #region Experts
        public async Task<ResponseModel<List<ExpertDto>>> GetExperts(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Expert/GetExpertobs")).Result;
                return await ResponseHandler<List<ExpertDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ExpertDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel<List<ExpertDto>>> GetExpertsbytime(string token, ManufactureRequest dto)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

                var response = _client.PostAsync(string.Format(_baseUrl, "api/v1/Expert/GetExpertbyTime"), stringContent).Result;
                return await ResponseHandler<List<ExpertDto>>(response);

            }
            catch
            {
                return new ResponseModel<List<ExpertDto>>
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> addExpertsHistury(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Expert/CreateExpert/" + id)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> UpdateExperts(string token, int id,int clientid)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.GetAsync(string.Format(_baseUrl, "api/v1/Expert/UpdateExpert/"+ id + "/" + clientid)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        public async Task<ResponseModel> DeleteExperts(string token, int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = _client.DeleteAsync(string.Format(_baseUrl, "api/v1/Expert/DeleteExpert/" + id)).Result;
                return await ResponseHandler(response);

            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.LogicError
                };
            }
        }
        #endregion

        private async Task<ResponseModel<T>> ResponseHandler<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                var respons = JsonConvert.DeserializeObject<ResponseModel<T>>(content);
                return respons;
                //return new ResponseModel<T>
                //{
                //    Data = JsonConvert.DeserializeObject<T>(content),
                //    StatusCode = ResponseStatus.Success
                //};
            }
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new ResponseModel<T>
                {
                    StatusCode = ResponseStatus.BadRequest
                };
            }
            return new ResponseModel<T>
            {
                StatusCode = ResponseStatus.ServerError
            };
        }
        private async Task<ResponseModel> ResponseHandler(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                var respons = JsonConvert.DeserializeObject<ResponseModel>(content);
                return respons;
                //return new ResponseModel
                //{
                //    StatusCode = ResponseStatus.Success
                //};
            }
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new ResponseModel
                {
                    StatusCode = ResponseStatus.BadRequest,
                    Message = await httpResponse.Content.ReadAsStringAsync()

                };
            }
            return new ResponseModel
            {
                StatusCode = ResponseStatus.ServerError
            };
        }
        
    }
}
