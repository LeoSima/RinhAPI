namespace RinhAPI.Exceptions;

public class LimiteInsuficienteException : Exception
{
    public LimiteInsuficienteException() : base()
    {
    }
    
    public LimiteInsuficienteException(string mensagem) : base(mensagem)
    {
    }
}
