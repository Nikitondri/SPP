namespace App.service.formatter;

public interface IStringFormatter
{
    string Format(string template, object target);
}