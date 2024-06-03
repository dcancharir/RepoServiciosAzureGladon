using Quartz;
using ServicioMigracionWGDB_000.dal;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.jobs
{
    public class JobMigracionData : IJob
    {
        int batchSize = 10;
        private readonly accounts_dal accountsDal;
        private readonly account_movements_dal accountMovementsDal;
        private readonly areas_dal areasDal;
        public JobMigracionData()
        {
            accountsDal = new accounts_dal();
            accountMovementsDal = new account_movements_dal();
            areasDal = new areas_dal();
        }
        public Task Execute(IJobExecutionContext context)
        {
            AccountsMigration();
            return Task.CompletedTask;
        }
        private void AccountsMigration()
        {
            try
            {
                //var lastIdInserted = accountsDal.GetLastIdInserted();
                //var totalAccount = accountsDal.GetTotalAccountsForMigration(lastIdInserted);
                //var batchCount = (totalAccount + batchSize -1)/batchSize;
                //Console.WriteLine("total a migrar " + totalAccount);

                //for (int i = 0; i < batchCount; i++)
                //{
                //    int intentos = 100;
                //    var startIndex = i*batchSize;
                //    var batch = accountsDal.GetAccountsPaginated(lastIdInserted, startIndex, batchSize);
                //    foreach(var item in batch)
                //    {
                //        var inserted = accountsDal.SaveAccounts(item);
                //    }
                //    Console.WriteLine("Se han enviado " + batch.Count + " registros");
                //}
                //var lastIdInserted = areasDal.GetLastIdInserted();
                //var totalAccount = areasDal.GetTotalAreasForMigration(0);
                //var batchCount = (totalAccount + batchSize - 1) / batchSize;
                //funciones.logueo("total a migrar " + totalAccount);

                //for (int i = 0; i < batchCount; i++)
                //{
                //    int intentos = 100;
                //    var startIndex = i * batchSize;
                //    var batch = areasDal.GetAreasPaginated(lastIdInserted, startIndex, batchSize);
                //    //foreach (var item in batch)
                //    //{
                //    //    var inserted = areasDal.SaveAreas(item);
                //    //}
                //    funciones.logueo("Se han enviado " + batch.Count + " registros");
                //}
                var batch = accountMovementsDal.GetAccountMovementsPaginated(0, 0, 1000);
                funciones.logueo($"registros encontrados : " + batch.Count);
            }
            catch (Exception ex)
            {
                funciones.logueo($"Error en metodo AccountsMigration - {ex.Message}");
            }
        }
    }
}
