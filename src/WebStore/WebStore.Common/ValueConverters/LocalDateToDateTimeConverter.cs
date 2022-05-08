using AutoMapper;
using NodaTime;

namespace WebStore.Common.ValueConverters;

public class LocalDateToDateTimeConverter : IValueConverter<LocalDate?, DateTime?>
{
    public DateTime? Convert(LocalDate? sourceMember, ResolutionContext context)
    {
        return sourceMember?.ToDateTimeUnspecified();
    }
}