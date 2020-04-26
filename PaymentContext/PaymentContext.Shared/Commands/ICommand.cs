

namespace PaymentContext.Shared.Commands
{
    /// <summary>
    /// Command de entrada/parâmetro
    /// </summary>
    public interface ICommand
    {
        void Validate();
    }
}
