using static App.model.KeyCharacters;

namespace App.service.validator;

public class FormatterStepValidatorImpl : IFormatterStepValidator
{
    private int _bracketsCount;

    public bool ValidateStep(int index, string value)
    {
        if (!IsCorrectedStep(index, value))
        {
            return false;
        }

        CheckAddBrackets(value[index]);
        CheckSubtractBrackets(value[index]);
        return IsBalanceBracketsAtEnd(index, value);
    }

    private void CheckAddBrackets(char currentChar)
    {
        if (currentChar == OpenBracket)
        {
            _bracketsCount++;
        }
    }

    private void CheckSubtractBrackets(char currentChar)
    {
        if (currentChar == CloseBracket)
        {
            _bracketsCount--;
        }
    }

    private bool IsCorrectedStep(int i, string value)
    {
        switch (_bracketsCount)
        {
            case > 2:
            case < 0:
            case 1 when value[i] == OpenBracket && value[i - 1] != OpenBracket:
            case 2 when value[i] == CloseBracket && (i + 1 > value.Length || value[i + 1] != CloseBracket):
                return false;
            default:
                return true;
        }
    }

    private bool IsBalanceBracketsAtEnd(int i, string value)
    {
        return i != value.Length - 1 || _bracketsCount == 0;
    }
}