using App.exception;
using App.service.interpolator;
using App.service.validator;
using static App.model.KeyCharacters;

namespace App.service.formatter;

public class StringFormatterImpl : IStringFormatter
{
    private readonly IFormatterStepValidator _validator = new FormatterStepValidatorImpl();
    private readonly IInterpolator _interpolator = new InterpolatorImpl();

    private bool _isText;
    private bool _isScreen;
    private string _tempField = "";

    public string Format(string template, object target)
    {
        var result = "";

        for (var i = 0; i < template.Length; i++)
        {
            CheckValidateStep(i, template);
            if (IsBrackets(template[i]))
            {
                CheckOpenBracket(ref result, template[i]);
                CheckCloseBracket(ref result, template[i], target);
            }
            else
            {
                CheckIsText(ref result, template[i]);
            }
        }

        return result;
    }

    private void CheckValidateStep(int index, string template)
    {
        if (!_validator.ValidateStep(index, template))
        {
            throw new BracketsException();
        }
    }

    private void CheckOpenBracket(ref string result, char currentChar)
    {
        if (currentChar != OpenBracket) return;
        if (_isText)
        {
            result += currentChar;
            _isText = false;
            _isScreen = true;
        }
        else
        {
            _tempField = "";
            _isText = true;
        }
    }

    private void CheckCloseBracket(ref string result, char currentChar, object obj)
    {
        if (currentChar != CloseBracket) return;
        if (_isScreen)
        {
            result += currentChar;
            _isScreen = false;
        }

        if (!_isText) return;

        result += _interpolator.Interpolate(_tempField, obj);
        _isText = false;
    }

    private static bool IsBrackets(char currentChar)
    {
        return currentChar is CloseBracket or OpenBracket;
    }

    private void CheckIsText(ref string result, char currentChar)
    {
        if (!_isText)
        {
            result += currentChar;
        }

        _tempField += currentChar;
    }
}