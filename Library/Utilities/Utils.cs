using Library.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Library.Utilities
{
    public static class UtilsLibrary
    {
        #region Enum
        public static object GetEnumValue(Enum enumValue)
        {
            object output = null;
            Type type = enumValue.GetType();
            FieldInfo fi = type.GetField(enumValue.ToString());
            Value[] attrs = fi.GetCustomAttributes(typeof(Value), false) as Value[];
            if (attrs != null)
                if (attrs.Length > 0)
                    output = attrs[0].GetValue;
                else
                    output = int.Parse(enumValue.ToString("D"));

            return output;
        }
        public static List<T> GetListEnum<T>()
        {
            List<T> retorno = new List<T>();
            foreach (string campoEnum in Enum.GetNames(typeof(T)))
            {
                retorno.Add((T)Enum.Parse(typeof(T), campoEnum));
            }
            return retorno;
        }

        //e.g. da chamada: var a = UtilsLibrary.GetListEnum(typeof(TabTipoSolicitacao)).OrderBy(p => p.StringTabTipoSolicitacao);
        public static List<ListEnum> GetListEnum(Type value)
        {
            List<ListEnum> retorno = new List<ListEnum>();

            foreach (string campoEnum in Enum.GetNames(value))
            {
                object objValue = GetEnumValue((Enum)Enum.Parse(value, campoEnum));
                FieldInfo fi = value.GetField(campoEnum);
                StringValueAttribute[] attribs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                StringDescriptionAttribute[] attr = fi.GetCustomAttributes(typeof(StringDescriptionAttribute), false) as StringDescriptionAttribute[];
                StringTabTipoSolicitacaoAttribute[] attrTabTipoSolicitacao = fi.GetCustomAttributes(typeof(StringTabTipoSolicitacaoAttribute), false) as StringTabTipoSolicitacaoAttribute[];
                StringTituloAttribute[] attrTitulo = fi.GetCustomAttributes(typeof(StringTituloAttribute), false) as StringTituloAttribute[];
                string strStringValue = string.Empty, strStringDescription = string.Empty, strStringTabTipoSolicitacao = string.Empty, strStringTitulo = string.Empty;

                if (attribs != null && attribs.Length > 0)
                    strStringValue = attribs[0].StringValue;

                if (attr != null && attr.Length > 0)
                    strStringDescription = attr[0].StringDescription;

                if (attrTabTipoSolicitacao != null && attrTabTipoSolicitacao.Length > 0)
                    strStringTabTipoSolicitacao = attrTabTipoSolicitacao[0].StringTabTipoSolicitacao;

                if (attrTitulo != null && attrTitulo.Length > 0)
                    strStringTitulo = attrTitulo[0].StringTitulo;

                retorno.Add(new ListEnum
                {
                    Name = fi.Name,
                    StringValue = strStringValue,
                    Value = objValue.ToString(),
                    StringDescription = strStringDescription,
                    StringTabTipoSolicitacao = strStringTabTipoSolicitacao,
                    StringTitulo = strStringTitulo
                });
            }

            return retorno.OrderBy(entry => entry.Name).ToList();
        }

        /*get [STRINGVALUE]*/
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attrs.Length > 0)
                output = attrs[0].StringValue;
            return output;
        }

        /*get [STRINGTITULO]*/
        public static string GetStringTitulo(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringTituloAttribute[] attrs = fi.GetCustomAttributes(typeof(StringTituloAttribute), false) as StringTituloAttribute[];
            if (attrs.Length > 0)
                output = attrs[0].StringTitulo;
            return output;
        }

        #endregion
    }

    public class Value : Attribute
    {
        private string _value;
        public Value(string value)
        {
            _value = value;
        }
        public string GetValue
        {
            get { return _value; }
        }
    }
}
