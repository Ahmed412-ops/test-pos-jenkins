using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Helper.Extensions
{
    public static class SettingExtensions
    {
        public static Result<T> ConvertValue<T>(string value, string typeString)
        {
            var targetType = typeof(T);

            if (targetType == typeof(string)) return Result<T>.Success((T)(object)value);

            if (targetType == typeof(int) && int.TryParse(value, out var intVal)) return Result<T>.Success((T)(object)intVal);
            if (targetType == typeof(decimal) && decimal.TryParse(value, out var decVal)) return Result<T>.Success((T)(object)decVal);
            if (targetType == typeof(double) && double.TryParse(value, out var dblVal)) return Result<T>.Success((T)(object)dblVal);
            if (targetType == typeof(bool) && bool.TryParse(value, out var boolVal)) return Result<T>.Success((T)(object)boolVal);
            if (targetType == typeof(DateTime) && DateTime.TryParse(value, out var dtVal)) return Result<T>.Success((T)(object)dtVal);

            return Result<T>.Fail(Messages.InvalidConversion);
        }

        public static Result<object> ConvertValue(string value, string typeString, Type targetType)
        {
            if (targetType == typeof(string)) return Result<object>.Success(value);

            if (targetType == typeof(int) && int.TryParse(value, out var intVal)) return Result<object>.Success(intVal);
            if (targetType == typeof(decimal) && decimal.TryParse(value, out var decVal)) return Result<object>.Success(decVal);
            if (targetType == typeof(double) && double.TryParse(value, out var dblVal)) return Result<object>.Success(dblVal);
            if (targetType == typeof(bool) && bool.TryParse(value, out var boolVal)) return Result<object>.Success(boolVal);
            if (targetType == typeof(DateTime) && DateTime.TryParse(value, out var dtVal)) return Result<object>.Success(dtVal);

            return Result<object>.Fail(Messages.InvalidConversion);
        }

        public static Type GetClrTypeFromSettingType(SettingType type) => type switch
        {
            SettingType.String => typeof(string),
            SettingType.Integer => typeof(int),
            SettingType.Decimal => typeof(decimal),
            SettingType.Boolean => typeof(bool),
            _ => typeof(string)
        };

    }
}
