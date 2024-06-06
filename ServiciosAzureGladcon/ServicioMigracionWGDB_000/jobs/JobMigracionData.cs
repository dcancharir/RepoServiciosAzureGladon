using Newtonsoft.Json;
using Quartz;
using ServicioMigracionWGDB_000.dal;
using ServicioMigracionWGDB_000.models;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.jobs
{
    public class JobMigracionData : IJob
    {
        int batchSize = 1000;
        long maximo = 2000;
        private readonly account_movements_dal accountMovementsDal;
        private readonly account_operations_dal accountOperationsDal;
        private readonly account_promotions_dal accountPromotionsDal;
        private readonly accounts_dal accountsDal;
        private readonly areas_dal areasDal;
        private readonly banks_dal banksDal;
        private readonly cashier_sessions_dal cashierSessionsDal;
        private readonly general_params_dal generalParamsDal;
        private readonly gift_instances_dal giftInstancesDal;
        private readonly gui_users_dal guiUsersDal;
        private readonly mobile_banks_dal mobileBanksDal;
        private readonly play_sessions_dal playSessionsDal;
        private readonly promogames_dal promogamesDal;
        private readonly promotions_dal promotionsDal;
        private readonly terminals_dal terminalsDal;
        private readonly venues_dal venuesDal;
        private readonly string database_name = string.Empty;
        private readonly string url_datawarehouse = string.Empty;
        public JobMigracionData()
        {
            accountMovementsDal = new account_movements_dal();
            accountOperationsDal = new account_operations_dal();
            accountPromotionsDal = new account_promotions_dal();
            accountsDal = new accounts_dal();
            areasDal = new areas_dal();
            banksDal = new banks_dal();
            cashierSessionsDal = new cashier_sessions_dal();
            generalParamsDal = new general_params_dal();
            giftInstancesDal = new gift_instances_dal();
            guiUsersDal = new gui_users_dal();
            mobileBanksDal = new mobile_banks_dal();
            playSessionsDal = new play_sessions_dal();
            promogamesDal = new promogames_dal();
            promotionsDal = new promotions_dal();
            terminalsDal = new terminals_dal();
            venuesDal = new venues_dal();

            database_name = ConfigurationManager.AppSettings["database_name"];
            url_datawarehouse = ConfigurationManager.AppSettings["url_datawarehouse"];
        }
        public Task Execute(IJobExecutionContext context)
        {
            PromoGamesMigration();
            funciones.logueo($"End PromoGamesMigration");

            VenuesMigration();
            funciones.logueo($"End VenuesMigration");

            AreasMigration();
            funciones.logueo($"End AreasMigration");

            BanksMigration();
            funciones.logueo($"End BanksMigration");

            GuiUsersMigration();
            funciones.logueo($"End GuiUsersMigration");

            TerminalsMigration();
            funciones.logueo($"End TerminalsMigration");

            MobileBanksMigration();
            funciones.logueo($"End MobileBanksMigration");

            PromotionsMigration();
            funciones.logueo($"End PromotionsMigration");

            GeneralParamsMigration();
            funciones.logueo($"End GeneralParamsMigration");

            GiftInstancesMigration();
            funciones.logueo($"End GistInstancesMigration");

            AccountsMigration();
            funciones.logueo($"End AccountsMigration");

            CashierSessionsMigration();
            funciones.logueo($"End CashierSessionsMigration");

            AccountPromotionsMigration();
            funciones.logueo($"End AccountPromotionsMigration");

            PlaySessionsMigration();
            funciones.logueo($"End PlaySessionsMigration");

            AccountOperationsMigration();
            funciones.logueo($"End AccountOperationsMigration");

            AccountMovementsMigration();
            funciones.logueo($"End AccountMovementsMigration");

            return Task.CompletedTask;
        }
        #region AccountMovementsMigration
        private void AccountMovementsMigration()
        {
            try
            {
                var lastId = AccountMovementsGetLastId();
                var totalForMigration = accountMovementsDal.GetTotalAccountMovementsForMigration(lastId);
                
                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    AccountMovementsMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = accountMovementsDal.GetAccountMovementsPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = AccountMovementsSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AccountsMigration - {ex.Message}");
            }
        }
        public long AccountMovementsGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountMovementsGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool AccountMovementsSave(List<account_movements> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountMovementsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region AccountOperationsMigration
        private void AccountOperationsMigration()
        {
            try
            {
                var lastId = AccountOperationsGetLastId();
                var totalForMigration = accountOperationsDal.GetTotalAccountOperationsForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    AccountOperationsMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = accountOperationsDal.GetAccountOperationsPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = AccountOperationsSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AccountOperationsMigration - {ex.Message}");
            }
        }
        public long AccountOperationsGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountOperationsGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool AccountOperationsSave(List<account_operations> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountOperationsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region AccountPromotions
        private void AccountPromotionsMigration()
        {
            try
            {
                var lastId = AccountPromotionsGetLastId();
                var totalForMigration = accountPromotionsDal.GetTotalAccountPromotionsForMigration(lastId);
                

                var batchCount = (totalForMigration+ batchSize - 1) / batchSize;
                string logMessage = $@"
    AccountPromotionsMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = accountPromotionsDal.GetAccountPromotionsPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = AccountPromotionsSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AccountPromotionsMigration - {ex.Message}");
            }
        }
        public long AccountPromotionsGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountPromotionsGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool AccountPromotionsSave(List<account_promotions> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountPromotionsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Accounts
        private void AccountsMigration()
        {
            try
            {
                var totalDatawareHouse = AccountsGetTotal();
                var totalForMigration = accountsDal.GetTotalAccountsForMigration();
                

                string logMessage = $@"
    AccountsMigration()
    totalDataWareHouse = {totalDatawareHouse}
    totalForMigration = {totalForMigration}
";
                funciones.logueo(logMessage);
                if(totalDatawareHouse <= totalForMigration)
                {
                    var totalAccounts = accountsDal.GetAllAccounts();
                    var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                    for (int i = 0; i < batchCount; i++)
                    {
                        int intentos = 100;
                        var startIndex = i * batchSize;
                        var batch = totalAccounts.Skip(startIndex).Take(batchSize).ToList();
                        while (intentos > 0)
                        {
                            var respuestaMigracion = AccountsSave(batch);
                            if (respuestaMigracion == true)
                            {
                                intentos = 0;
                            }
                            else
                            {
                                intentos--;
                                Task.Delay(10000).Wait();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AccountsMigration - {ex.Message}");
            }
        }
        public long AccountsGetTotal()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountsGetTotal?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool AccountsSave(List<accounts> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AccountsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Areas
        private void AreasMigration()
        {
            try
            {
                var lastId = AreasGetLastId();
                var totalForMigration = areasDal.GetTotalAreasForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    AreasMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = areasDal.GetAreasPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = AreasSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AreasMigration - {ex.Message}");
            }
        }
        public long AreasGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AreasGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool AreasSave(List<areas> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/AreasSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Banks
        private void BanksMigration()
        {
            try
            {
                var lastId = BanksGetLastId();
                var totalForMigration = banksDal.GetTotalBanksForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    BanksMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = banksDal.GetBanksPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = BanksSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo BanksMigration - {ex.Message}");
            }
        }
        public long BanksGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/BanksGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool BanksSave(List<banks> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/BanksSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region CashierSessions
        private void CashierSessionsMigration()
        {
            try
            {
                var lastId = CashierSessionsGetLastId();
                var totalForMigration = cashierSessionsDal.GetTotalCashierSessionsForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    CashierSessionsMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = cashierSessionsDal.GetAreasPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = CashierSessionsSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo CashierSessionsMigration - {ex.Message}");
            }
        }
        public long CashierSessionsGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/CashierSessionsGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool CashierSessionsSave(List<cashier_sessions> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/CashierSessionsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region GeneralParams
        private void GeneralParamsMigration()
        {
            try
            {
                var totalDatawareHouse = GeneralParamsGetTotal();
                var totalForMigration = generalParamsDal.GetTotalGeneralParamsForMigration();
                

                string logMessage = $@"
    GeneralParamsMigration()
    totalDatawareHouse = {totalDatawareHouse}
    totalForMigration = {totalForMigration}
";
                funciones.logueo(logMessage);
                if (totalDatawareHouse <= totalForMigration)
                {
                    var totalGeneralParams = generalParamsDal.GetAllGeneralParams();

                    var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                    for (int i = 0; i < batchCount; i++)
                    {
                        int intentos = 100;
                        var startIndex = i * batchSize;
                        var batch = totalGeneralParams.Skip(startIndex).Take(batchSize).ToList();
                        while (intentos > 0)
                        {
                            var respuestaMigracion = GeneralParamsSave(batch);
                            if (respuestaMigracion == true)
                            {
                                intentos = 0;
                            }
                            else
                            {
                                intentos--;
                                Task.Delay(10000).Wait();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AccountsMigration - {ex.Message}");
            }
        }
        public long GeneralParamsGetTotal()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/GeneralParamsGetTotal?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool GeneralParamsSave(List<general_params> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/GeneralParamsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region GiftInstances
        private void GiftInstancesMigration()
        {
            try
            {
                var lastId = GiftInstancesGetLastId();
                var totalForMigration = giftInstancesDal.GetTotalGiftInstancesForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    GiftInstancesMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = giftInstancesDal.GetGiftInstancesPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = GiftInstancesSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo GistInstancesMigration - {ex.Message}");
            }
        }
        public long GiftInstancesGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/GiftInstancesGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool GiftInstancesSave(List<gift_instances> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/GiftInstancesSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region GuiUsers
        private void GuiUsersMigration()
        {
            try
            {
                var totalDatawareHouse = GuiUsersGetTotal();
                var totalForMigration = guiUsersDal.GetTotalGuiUsersForMigration();
                

                string logMessage = $@"
    GuiUsersMigration()
    totalDatawareHouse = {totalDatawareHouse}
    totalForMigration = {totalForMigration}
";
                funciones.logueo(logMessage);
                if (totalDatawareHouse <= totalForMigration)
                {
                    var totalGuiUsers = guiUsersDal.GetAllGuiUsers();
                    var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                    for (int i = 0; i < batchCount; i++)
                    {
                        int intentos = 100;
                        var startIndex = i * batchSize;
                        var batch = totalGuiUsers.Skip(startIndex).Take(batchSize).ToList();
                        while (intentos > 0)
                        {
                            var respuestaMigracion = GuiUsersSave(batch);
                            if (respuestaMigracion == true)
                            {
                                intentos = 0;
                            }
                            else
                            {
                                intentos--;
                                Task.Delay(10000).Wait();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AccountsMigration - {ex.Message}");
            }
        }
        public long GuiUsersGetTotal()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/GuiUsersGetTotal?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool GuiUsersSave(List<gui_users> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/GuiUsersSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region MobileBanks
        private void MobileBanksMigration()
        {
            try
            {
                var lastId = MobileBanksGetLastId();
                var totalForMigration = mobileBanksDal.GetTotalMobileBanksForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    MobileBanksMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = mobileBanksDal.GetMobileBanksPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = MobileBanksSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo MobileBanksMigration - {ex.Message}");
            }
        }
        public long MobileBanksGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/MobileBanksGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool MobileBanksSave(List<mobile_banks> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/MobileBanksSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region PlaySessions
        private void PlaySessionsMigration()
        {
            try
            {
                var lastId = PlaySessionsGetLastId();
                var totalForMigration = playSessionsDal.GetTotalPlaySessionsForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    PlaySessionsMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = playSessionsDal.GetPlaySessionsPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = PlaySessionsSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo PlaySessionsMigration - {ex.Message}");
            }
        }
        public long PlaySessionsGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/PlaySessionsGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool PlaySessionsSave(List<play_sessions> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/PlaySessionsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region PromoGames
        private void PromoGamesMigration()
        {
            try
            {
                var lastId = PromoGamesGetLastId();
                var totalForMigration = promogamesDal.GetTotalPromogamesForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    PromoGamesMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = promogamesDal.GetPromogamesPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = PromoGamesSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo PromoGamesMigration - {ex.Message}");
            }
        }
        public long PromoGamesGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/PromoGamesGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool PromoGamesSave(List<promogames> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/PromoGamesSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Promotions
        private void PromotionsMigration()
        {
            try
            {
                var lastId = PromotionsGetLastId();
                var totalForMigration = promotionsDal.GetTotalPromotionsForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    PromotionsGetLastId()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = promotionsDal.GetPromotionsPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = PromotionsSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo PromotionsMigration - {ex.Message}");
            }
        }
        public long PromotionsGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/PromotionsGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool PromotionsSave(List<promotions> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/PromotionsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Terminals
        private void TerminalsMigration()
        {
            try
            {
                var lastId = TerminalsGetLastId();
                var totalForMigration = terminalsDal.GetTotalTerminalsForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    TerminalsMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = terminalsDal.GetTerminalsPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = TerminalsSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo TerminalsMigration - {ex.Message}");
            }
        }
        public long TerminalsGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/TerminalsGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool TerminalsSave(List<terminals> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/TerminalsSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
        #region Venues
        private void VenuesMigration()
        {
            try
            {
                var lastId = VenuesGetLastId();
                var totalForMigration = venuesDal.GetTotalVenuesForMigration(lastId);
                

                var batchCount = (totalForMigration + batchSize - 1) / batchSize;
                string logMessage = $@"
    VenuesMigration()
    lastId = {lastId}
    totalForMigration = {totalForMigration}
    batchCount = {batchCount}
";
                funciones.logueo(logMessage);
                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i * batchSize;
                    var batch = venuesDal.GetVenuesPaginated(lastId, startIndex, batchSize);
                    while (intentos > 0)
                    {
                        var respuestaMigracion = VenuesSave(batch);
                        if (respuestaMigracion == true)
                        {
                            intentos = 0;
                        }
                        else
                        {
                            intentos--;
                            Task.Delay(10000).Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo VenuesMigration - {ex.Message}");
            }
        }
        public long VenuesGetLastId()
        {
            long lastId = 0;
            object oEnvio = new
            {
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/VenuesGetLastId?databaseName={database_name}", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<long>(result);
                        return jsonResult;
                    }
                }
            }
            return lastId;
        }
        public bool VenuesSave(List<venues> items)
        {
            bool response = false;
            object oEnvio = new
            {
                items = items,
                databaseName = database_name
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(oEnvio), Encoding.UTF8, "application/json");
                using (HttpResponseMessage httpResponse = httpClient.PostAsync($"{url_datawarehouse}/Servicio/VenuesSave", stringContent).Result)
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var jsonResult = JsonConvert.DeserializeObject<bool>(result);
                        return jsonResult;
                    }
                }
            }
            return response;
        }
        #endregion
    }
}
