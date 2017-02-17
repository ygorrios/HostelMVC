using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Library
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

            // Caso retorne NULL é porque não encontro o enum
            if (fieldInfo != null)
            {
                // Get the stringvalue attributes
                StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

                // Return the first if there was a match.
                return attribs.Length > 0 ? attribs[0].StringValue : null;
            }
            else
                return string.Empty;
        }

        public static string GetStringDescription(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

            // Caso retorne NULL é porque não encontro o enum
            if (fieldInfo != null)
            {
                // Get the stringdescription attributes
                StringDescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringDescriptionAttribute), false) as StringDescriptionAttribute[];

                // Return the first if there was a match.
                return attribs.Length > 0 ? attribs[0].StringDescription : null;
            }
            else
                return string.Empty;
        }

        public static string GetName(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Return the Enum's name
            return type.GetField(value.ToString()).Name;
        }

        public static object GetValue(this Enum value)
        {
            object output = null;
            int enumValue = 0;
            if (int.TryParse(value.ToString("D"), out enumValue))
                output = enumValue;

            // Return the Enum's value
            return output;
        }

        /// <summary>
        /// Recupera o Enum de acordo com o tipo fornecido pelo StringValue
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="description">Value</param>
        /// <returns></returns>
        /// <example>
        /// campo.GetEnumFromStringValue<Enumerador>()
        /// </example>
        public static T GetEnumFromStringValue<T>(this string value) where T : struct
        {
            Debug.Assert(typeof(T).IsEnum);

            return (T)typeof(T)
                .GetFields()
                .First(f => f.GetCustomAttributes(typeof(StringValueAttribute), false)
                             .Cast<StringValueAttribute>()
                             .Any(a => a.StringValue.Equals(value, StringComparison.OrdinalIgnoreCase))
                )
                .GetValue(null);
        }
    }

    public class StringValueAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
        #endregion
    }

    public class StringDescriptionAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringdescription for a value in an enum.
        /// </summary>
        public string StringDescription { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringDescription Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringDescriptionAttribute(string value)
        {
            this.StringDescription = value;
        }
        #endregion
    }

    public class StringTabTipoSolicitacaoAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringdescription for a value in an enum.
        /// </summary>
        public string StringTabTipoSolicitacao { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringDescription Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringTabTipoSolicitacaoAttribute(string value)
        {
            this.StringTabTipoSolicitacao = value;
        }
        #endregion
    }

    public class StringTituloAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringdescription for a value in an enum.
        /// </summary>
        public string StringTitulo { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringDescription Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringTituloAttribute(string value)
        {
            this.StringTitulo = value;
        }
        #endregion
    }
}
