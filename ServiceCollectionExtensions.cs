using stackup_quiz_api.Filter;

namespace stackup_quiz_api;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddFluentValidationAsyncAutoValidation(this IMvcBuilder builder)
    {
        return builder.AddMvcOptions(o =>
        {
            o.Filters.Add<AsyncAutoValidation>(AsyncAutoValidation.OrderLowerThanModelStateInvalidFilter);
        });
    }
}
