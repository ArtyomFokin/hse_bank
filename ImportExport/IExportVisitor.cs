using HseBank.Domain;

namespace HseBank.ImportExport
{
    public interface IExportVisitor
    {
        void Visit(Account account);
        void Visit(Category category);
        void Visit(Operation operation);
    }
}
