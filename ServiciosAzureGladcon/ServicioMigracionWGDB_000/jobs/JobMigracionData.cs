using Quartz;
using ServicioMigracionWGDB_000.dal;
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
        public JobMigracionData()
        {
            accountsDal = new accounts_dal();
            accountMovementsDal = new account_movements_dal();
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
                var lastId = 0;
                var totalAccountMovements = accountMovementsDal.GetTotalAccountMovementsForMigration(0);//reemplazar por el maximo id del servidor centralizado
                var batchCount = (totalAccountMovements + batchSize -1)/batchSize;
                Console.WriteLine("total a migrar " + totalAccountMovements);

                for (int i = 0; i < batchCount; i++)
                {
                    int intentos = 100;
                    var startIndex = i*batchSize;
                    var batch = accountMovementsDal.GetAccountMovementsPaginated(lastId, startIndex, batchSize);
                    //enviar a servidor centralizado
                    Console.WriteLine("Se han enviado " + batch.Count + " registros");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
