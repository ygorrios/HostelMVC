using Camada.Dominio.Contratos.Repositorio;
using Camada.Negocio.Contratos.Repositorio;
using Library.Ioc;

  
namespace Camada.Aplicacao.Fachada
{
    public static class Repositorio
    {
        private static ICALC _iCALC = null;
        public static ICALC CALC
        {
            get { return _iCALC ?? (_iCALC = new CALC()); }
        }

        private static IMONEY_COUNT _iMONEY_COUNT = null;
        public static IMONEY_COUNT MONEY_COUNT
        {
            get { return _iMONEY_COUNT ?? (_iMONEY_COUNT = new MONEY_COUNT()); }
        }

        private static ITRANSACTIONS _iTRANSACTIONS = null;
        public static ITRANSACTIONS TRANSACTIONS
        {
            get { return _iTRANSACTIONS ?? (_iTRANSACTIONS = new TRANSACTIONS()); }
        }

        private static ITOTAL_TRANSACTIONS _iTOTAL_TRANSACTIONS = null;
        public static ITOTAL_TRANSACTIONS TOTAL_TRANSACTIONS
        {
            get { return _iTOTAL_TRANSACTIONS ?? (_iTOTAL_TRANSACTIONS = new TOTAL_TRANSACTIONS()); }
        }

        private static IUSER _iUSER = null;
        public static IUSER USER
        {
            get { return _iUSER ?? (_iUSER = new USER()); }
        }

        private static ISTOCK _iSTOCK = null;
        public static ISTOCK STOCK
        {
            get { return _iSTOCK ?? (_iSTOCK = new STOCK()); }
        }

        private static IEGALI_PASSWORDS _iEGALI_PASSWORDS = null;
        public static IEGALI_PASSWORDS EGALI_PASSWORDS
        {
            get { return _iEGALI_PASSWORDS ?? (_iEGALI_PASSWORDS = new EGALI_PASSWORDS()); }
        }

        private static ILISTS _iLISTS = null;
        public static ILISTS LIST
        {
            get { return _iLISTS ?? (_iLISTS = new LIST()); }
        }
    }
}