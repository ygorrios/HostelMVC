using Camada.Dominio.Contratos.Negocio;
using Camada.Negocio.Contratos.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camada.Aplicacao.Fachada
{
    public static class Negocio
    {
        private static IManterCALC _manterCALC = null;
        public static IManterCALC ManterCALC
        {
            get { return _manterCALC ?? (_manterCALC = new ManterCALC()); }
        }

        private static IManterMONEY_COUNT _manterMONEY_COUNT = null;
        public static IManterMONEY_COUNT ManterMONEY_COUNT
        {
            get { return _manterMONEY_COUNT ?? (_manterMONEY_COUNT = new ManterMONEY_COUNT()); }
        }

        private static IManterTRANSACTIONS _manterTRANSACTIONS = null;
        public static IManterTRANSACTIONS ManterTRANSACTIONS
        {
            get { return _manterTRANSACTIONS ?? (_manterTRANSACTIONS = new ManterTRANSACTIONS()); }
        }

        private static IManterTOTAL_TRANSACTIONS _manterTOTAL_TRANSACTIONS = null;
        public static IManterTOTAL_TRANSACTIONS ManterTOTAL_TRANSACTIONS
        {
            get { return _manterTOTAL_TRANSACTIONS ?? (_manterTOTAL_TRANSACTIONS = new ManterTOTAL_TRANSACTIONS()); }
        }

        private static IManterHOUSE_KEEPING _manterHOUSE_KEEPING = null;
        public static IManterHOUSE_KEEPING ManterHOUSE_KEEPING
        {
            get { return _manterHOUSE_KEEPING ?? (_manterHOUSE_KEEPING = new ManterHOUSE_KEEPING()); }
        }

        private static IManterSTOCK _manterSTOCK = null;
        public static IManterSTOCK ManterSTOCK
        {
            get { return _manterSTOCK ?? (_manterSTOCK = new ManterSTOCK()); }
        }

        private static IManterLIST _manterLIST = null;
        public static IManterLIST ManterLIST
        {
            get { return _manterLIST ?? (_manterLIST = new ManterLIST()); }
        }

        private static IManterEGALI_PASSWORDS _manterEGALI_PASSWORDS = null;
        public static IManterEGALI_PASSWORDS ManterEGALI_PASSWORDS
        {
            get { return _manterEGALI_PASSWORDS ?? (_manterEGALI_PASSWORDS = new ManterEGALI_PASSWORDS()); }
        }

        private static IManterEGALI_PASSWORDS_HISTORY _manterEGALI_PASSWORDS_HISTORY = null;
        public static IManterEGALI_PASSWORDS_HISTORY ManterEGALI_PASSWORDS_HISTORY
        {
            get { return _manterEGALI_PASSWORDS_HISTORY ?? (_manterEGALI_PASSWORDS_HISTORY = new ManterEGALI_PASSWORDS_HISTORY()); }
        }

        private static IManterDOCUMENT _manterDOCUMENT = null;
        public static IManterDOCUMENT ManterDOCUMENT
        {
            get { return _manterDOCUMENT ?? (_manterDOCUMENT = new ManterDOCUMENT()); }
        }

        private static IManterUSER _manterUSER = null;
        public static IManterUSER ManterUSER
        {
            get { return _manterUSER ?? (_manterUSER = new ManterUSER()); }
        }
    }
}