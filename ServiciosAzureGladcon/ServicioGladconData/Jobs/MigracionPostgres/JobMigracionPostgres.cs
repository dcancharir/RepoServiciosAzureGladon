using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGladconData.Jobs.MigracionPostgres
{
    public class JobMigracionPostgres:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Job Iniciado Ahora");
            return Task.CompletedTask;
        }
    }
}
