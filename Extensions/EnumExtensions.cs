namespace Extensions;

public static class EnumExtensions
{
    public static List<object> GetQuestionTypes<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
                   .Cast<T>()
                   .Select(e => new
                   {
                       Id = Convert.ToInt32(e),
                       Name = e.ToString()
                   })
                   .Cast<object>()
                   .ToList();
    }
}
