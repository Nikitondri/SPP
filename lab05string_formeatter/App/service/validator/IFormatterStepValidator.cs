namespace App.service.validator;

public interface IFormatterStepValidator
{
    bool ValidateStep(int index, string value);
}