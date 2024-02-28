using System;
using System.Globalization;
using System.Windows.Data;

namespace FileOrganizer2.Models
{
    public class NumberFormatConverter : IValueConverter
    {
        /// <summary>
        ///     Convert が返却する数値のフォーマットを設定・取得します。
        ///     デフォルトは "D4" に設定されています。
        /// </summary>
        public string Format { get; set; } = "D4";

        /// <summary>
        ///     false に設定されている場合、 Converter に入力された数値が 0 であった時、空文字を返却します。
        ///     デフォルトは true に設定されています。その場合、返却される値は Format に応じて決定されます。
        /// </summary>
        public bool VisibleWhenZero { get; set; } = true;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return (int)value == 0 && !VisibleWhenZero
                    ? string.Empty
                    : ((int)value).ToString(Format);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}