using ServicioAzureIAS.Clases.Enum;
using System.Configuration;

namespace ServicioAzureIAS.DAL {
    public class ConnectionHelperDAL {
        private readonly string _conexionTasklyFlowDyd;
        private readonly string _conexionTasklyFlowHolding;
        private readonly string _conexionSeguridadPj;
        private readonly string _conexionGladconData;

        public ConnectionHelperDAL() {
            _conexionTasklyFlowDyd = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowDyd"].ConnectionString;
            _conexionTasklyFlowHolding = ConfigurationManager.ConnectionStrings["connectionBDTasklyFlowHolding"].ConnectionString;
            _conexionGladconData = ConfigurationManager.ConnectionStrings["connectionBDGladconData"].ConnectionString;
            _conexionSeguridadPj = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
        }

        public string ObtenerConexion(BaseDatosEnum baseDatos) {
            string conexion = string.Empty;
            switch(baseDatos) {
                case BaseDatosEnum.TasklyFlowDyD:
                    conexion = _conexionTasklyFlowDyd;
                    break;
                case BaseDatosEnum.TasklyFlowHolding:
                    conexion = _conexionTasklyFlowHolding;
                    break;
                case BaseDatosEnum.SeguridadPj:
                    conexion = _conexionSeguridadPj;
                    break;
                case BaseDatosEnum.GladconData:
                    conexion = _conexionGladconData;
                    break;
            }
            return conexion;
        }
    }
}
