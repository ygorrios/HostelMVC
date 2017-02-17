using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio
{
    public enum TabList_Type
    {
        [StringValue("1")]
        [StringDescription("BLACKLIST")]
        BLACKLIST = 1,

        [StringValue("2")]
        [StringDescription("VERIFY BOOKING")]
        VERIFY_BOOKING = 2,

        [StringValue("3")]
        [StringDescription("PASSPORT")]
        PASSPORT = 3,
    }

    public enum TabProduct_Type
    {
        [StringValue("1")]
        [StringDescription("AIRLINK")]
        AIRLINK = 1,

        [StringValue("1")]
        [StringDescription("AIR LINK")]
        Air_link = 1,

        [StringValue("2")]
        [StringDescription("ADAPTOR")]
        Adaptor = 2,
        
    }

    public enum TabAction_Type
    {
        [StringValue("1")]
        [StringDescription("Insert")]
        Insert = 1,

        [StringValue("2")]
        [StringDescription("Sold")]
        Sold = 2
    }

    public enum TabTransaction_Type
    {
        [StringValue("1")]
        [StringDescription("Deposit")]
        Deposit = 1,

        [StringValue("2")]
        [StringDescription("Payment")]
        Payment = 2,

        [StringValue("3")]
        [StringDescription("Refund")]
        Refund = 3
    }

    public enum TabUSER_TYPE
    {
        [StringValue("1")]
        [StringDescription("USER")]
        USER = 1,

        [StringValue("2")]
        [StringDescription("ADMIN")]
        ADMIN = 2,

        [StringValue("3")]
        [StringDescription("FINANCIAL")]
        FINANCIAL = 3
    }
    public enum TabCalc_Type
    {
        [StringValue("1")]
        [StringDescription("Cashier")]
        Cashier = 1,

        [StringValue("2")]
        [StringDescription("Envelope")]
        Envelope = 2,

        [StringValue("3")]
        [StringDescription("Envelope Change")]
        Envelope_Change = 3
    }

    public enum TabReport_Type
    {
        [StringValue("1")]
        [StringDescription("Cash")]
        Cash = 1,

        [StringValue("2")]
        [StringDescription("Card")]
        Card = 2,

        [StringValue("3")]
        [StringDescription("Vagner")]
        Vagner = 3,

        [StringValue("4")]
        [StringDescription("Bank")]
        Bank = 4
    }

    public enum TabSHIFT_TYPE
    {
        [StringValue("0")]
        [StringDescription("")]
        DEFAULT = 0,

        [StringValue("1")]
        [StringDescription("10PM-06AM")]
        PRIM = 1,

        [StringValue("2")]
        [StringDescription("06AM-02PM")]
        SEC = 2,

        [StringValue("3")]
        [StringDescription("02PM-06PM")]
        THIR = 3,

        [StringValue("4")]
        [StringDescription("06PM-10PM")]
        FOUR = 4
    }

    public enum TabDialogMessage
    {
        [StringValue("1")]
        [StringDescription("Sucess")]
        Sucess = 1,

        [StringValue("2")]
        [StringDescription("Danger")]
        Danger = 2,

        [StringValue("3")]
        [StringDescription("Info")]
        Info = 3,

        [StringValue("4")]
        [StringDescription("Warning")]
        Warning = 4
    }

    public enum TabPayment_Type
    {
        [StringValue("1")]
        [StringDescription("BACS")]
        BACS = 1,

        [StringValue("2")]
        [StringDescription("Card")]
        Card = 2,

        [StringValue("3")]
        [StringDescription("Cash")]
        Cash = 3,

        [StringValue("4")]
        [StringDescription("Cheque")]
        Cheque = 4,

        [StringValue("5")]
        [StringDescription("Gift Certifiacte")]
        Gift_Certifiacte = 5,

        [StringValue("6")]
        [StringDescription("OTA Paid")]
        OTA_Paid = 6,

        [StringValue("7")]
        [StringDescription("Other")]
        Other = 7,

        [StringValue("8")]
        [StringDescription("Voucher")]
        Voucher = 8
    }

    public enum TabCard_Type
    {
        [StringValue("1")]
        [StringDescription("Visa")]
        Visa = 1,

        [StringValue("2")]
        [StringDescription("MasterCard")]
        MasterCard = 2,

        [StringValue("3")]
        [StringDescription("American Express")]
        American_Express = 3,

        [StringValue("4")]
        [StringDescription("Diners Club")]
        Diners_Club = 4,

        [StringValue("5")]
        [StringDescription("JBC")]
        JBC = 5,

        [StringValue("6")]
        [StringDescription("Visa Debit")]
        Visa_Debit = 6,

        [StringValue("7")]
        [StringDescription("Maestro")]
        Maestro = 7,

        [StringValue("8")]
        [StringDescription("Laser")]
        Laser = 8,

        [StringValue("8")]
        [StringDescription("Discover")]
        Discover = 8
    }

    public enum EMensagem
    {
        Alert,
        Error,
        Success
    }
    
    public enum Modal
    {
        ModalServidorAposentadoria
    }

    public enum Ambiente
    {
        DEV,
        HOM,
        PRD
    }

    public enum ErroRest
    {
        [StringValue("URL Inválida ou não está autorizado a acessar essa função.")]
        NAO_AUTORIZADO = 401,
        [StringValue("URL Não localizada!")]
        NAO_LOCALIZADO = 404,
        [StringValue("Formato inválido de documento.")]
        FORMATO_DO_DTO_INVALIDO = 422,
        [StringValue("Erro Interno do Servidor! Favor tentar mais tarde!")]
        ERRO_INTERNO = 500
    }

    public enum ErroUploadArquivo
    {
        [StringValue("<li> O arquivo <u><b>'{0}'</b></u> já foi enviado.</li>")]
        ARQUIVO_DUPLICADO,
        [StringValue("<li> O arquivo <u><b>'{0}'</b></u> não está no formato PDF.</li>")]
        FORMATO_INVALIDO,
        [StringValue("<li> O nome do arquivo <u><b>'{0}'</b></u> não pode conter ponto(.).")]
        NOME_INVALIDO,
        [StringValue("<li> O arquivo <u><b>'{0}'</b></u> excedeu o tamanho permitido (2MB).</li>")]
        TAMANHO_EXCEDIDO,
        [StringValue("<li> O arquivo <u><b>'{0}'</b></u> pode estar comrrompido ou sem conteúdo, favor verificar.</li>")]
        ARQUIVO_CORROMPIDO,
        [StringValue("<li> Problemas com Identificador do Ato. Contate o Administrador.</li>")]
        PROBLEMAS_IDENTIFICADOR_ATO
    }
}
