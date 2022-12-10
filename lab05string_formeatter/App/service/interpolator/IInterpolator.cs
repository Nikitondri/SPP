namespace App.service.interpolator;

public interface IInterpolator
{
    string Interpolate(string value, object obj);
}