using AutoMapper;
using NodaTime;

namespace WebStore.Common.ValueConverters;

public class DateTimeToLocalDateConverter : IValueConverter<DateTime?, LocalDate?>
{
    public LocalDate? Convert(DateTime? sourceMember, ResolutionContext context)
    {
        return sourceMember.HasValue ? LocalDate.FromDateTime(sourceMember.Value) : null;
    }
}