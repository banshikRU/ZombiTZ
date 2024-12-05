using System;

public interface IInput 
{
    public event Action OnShoot;

    public void TakeShoot();
}
