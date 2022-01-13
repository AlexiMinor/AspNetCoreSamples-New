using System.ComponentModel.DataAnnotations;

namespace FirstMvcApp.ValidationAttributes;

public class RegisterLoginAttribute : ValidationAttribute
{
    private readonly string[] _blackList;

    public RegisterLoginAttribute(string[] blackList)
    {
        _blackList = blackList;
    }

    public override bool IsValid(object? value)
    {
        if (value != null)
        {
            foreach (var blackListItem in _blackList)
            {
                if (value.ToString()!.Contains(blackListItem))
                    return false;
            }
            return true;
        }

        return false;
    }
}