using System;
using System.Text.RegularExpressions;

namespace CustomCexWrapper.Helpers
{
    public static class StringExtensions
    {
        public static string ValidateSymbol(this string symbol, string messagePrefix = "", string messageSuffix = "")
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}Symbol is not provided{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

            // symbol = symbol.ToLower(CultureInfo.InvariantCulture);
            if (!Regex.IsMatch(symbol, "^(([a-z]|[A-Z]|-|[0-9]){4,})$"))
                throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}{symbol} is not a valid Okex Symbol. Should be [QuoteCurrency]-[BaseCurrency], e.g. ETH-BTC{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

            return symbol;
        }
    }
}