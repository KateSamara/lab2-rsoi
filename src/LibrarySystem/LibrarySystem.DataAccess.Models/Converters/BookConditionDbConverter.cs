using LibrarySystem.Domain.Models;

namespace LibrarySystem.DataAccess.Models.Converters;

public static class BookConditionDbConverter
{
    public static BookConditionDb ToDb(this BookCondition bookCondition)
    {
        return bookCondition switch
        {
            BookCondition.EXCELLENT => BookConditionDb.EXCELLENT,
            BookCondition.GOOD => BookConditionDb.GOOD,
            BookCondition.BAD => BookConditionDb.BAD,
            _ => BookConditionDb.EXCELLENT
        };
    }
}