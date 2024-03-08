namespace RinhAPI.Exceptions;

public class OperacaoInvalidaException : Exception
{
    public OperacaoInvalidaException() : base()
    {
    }
    
    public OperacaoInvalidaException(string mensagem) : base(mensagem)
    {
    }
}
