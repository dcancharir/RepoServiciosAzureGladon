using Quartz;
using ServicioGladconData.DAL;
using ServicioGladconData.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGladconData.Jobs.MigracionPostgres
{
    public class JobMigracionPostgres:IJob
    {
        private salaDAL _salaDAL = new salaDAL();
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Job Iniciado Ahora");
            var salas = _salaDAL.ListarSala();
            foreach(var sala in salas)
            {
                funciones.logueo(sala.nombre_sala);
            }
            return Task.CompletedTask;
        }
    }
}
