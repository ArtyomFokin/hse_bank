using System;
using System.Linq;
using HseBank.Domain;
using HseBank.Factories;
using HseBank.Services.Implementations;
using HseBank.Commands;
using HseBank.Services;

namespace FinancialAccounting.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBankAccountService bankAccountService = new BankAccountService();
            ICategoryService categoryService = new CategoryService();
            IOperationService operationService = new OperationService();
            IAnalyticsService analyticsService = new AnalyticsService(operationService);

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в систему учета финансов ВШЭ-Банка!");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать счет");
                Console.WriteLine("2. Создать категорию");
                Console.WriteLine("3. Добавить операцию");
                Console.WriteLine("4. Редактировать счет");
                Console.WriteLine("5. Редактировать категорию");
                Console.WriteLine("6. Редактировать операцию");
                Console.WriteLine("7. Удалить счет");
                Console.WriteLine("8. Удалить категорию");
                Console.WriteLine("9. Удалить операцию");
                Console.WriteLine("10. Показать аналитическую разницу");
                Console.WriteLine("11. Показать все счета");
                Console.WriteLine("12. Показать все категории");
                Console.WriteLine("13. Показать все операции");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите название счета: ");
                            string accountName = Console.ReadLine();
                            var account = AccountFactory.Create(accountName);
                            bankAccountService.CreateAccount(account);
                            Console.WriteLine($"Счет создан. ID: {account.Id}");
                            break;

                        case "2":
                            Console.Write("Введите название категории: ");
                            string categoryName = Console.ReadLine();
                            Console.Write("Выберите тип (0 - Доход, 1 - Расход): ");
                            int catTypeInt = int.Parse(Console.ReadLine());
                            CategoryType catType = (CategoryType)catTypeInt;
                            var category = CategoryFactory.Create(catType, categoryName);
                            categoryService.CreateCategory(category);
                            Console.WriteLine($"Категория создана. ID: {category.Id}");
                            break;

                        case "3":
                            Console.Write("Введите ID счета: ");
                            Guid accId = Guid.Parse(Console.ReadLine());
                            Console.Write("Введите ID категории: ");
                            Guid catId = Guid.Parse(Console.ReadLine());
                            Console.Write("Введите тип операции (0 - Доход, 1 - Расход): ");
                            int opTypeInt = int.Parse(Console.ReadLine());
                            OperationType opType = (OperationType)opTypeInt;
                            Console.Write("Введите сумму операции: ");
                            uint amount = uint.Parse(Console.ReadLine());
                            Console.Write("Введите описание операции: ");
                            string description = Console.ReadLine();
                            var createOpCommand = new CreateOperationCommand(operationService, opType, accId, amount, DateTime.Now, description, catId);
                            IFinancialCommand timedCreateOp = new TimedCommandDecorator(createOpCommand);
                            timedCreateOp.Execute();
                            var accountToUpdate = bankAccountService.GetAccount(accId);
                            var operation = operationService.GetAllOperations().Last();
                            accountToUpdate.RegisterOperation(operation);
                            break;

                        case "4":
                            Console.Write("Введите ID счета для редактирования: ");
                            Guid updateAccId = Guid.Parse(Console.ReadLine());
                            Console.Write("Введите новое имя счета: ");
                            string newAccountName = Console.ReadLine();
                            var updateAccCommand = new UpdateAccountCommand(bankAccountService, updateAccId, newAccountName);
                            new TimedCommandDecorator(updateAccCommand).Execute();
                            break;

                        case "5":
                            Console.Write("Введите ID категории для редактирования: ");
                            Guid updateCatId = Guid.Parse(Console.ReadLine());
                            Console.Write("Введите новое имя категории: ");
                            string newCategoryName = Console.ReadLine();
                            var updateCatCommand = new UpdateCategoryCommand(categoryService, updateCatId, newCategoryName);
                            new TimedCommandDecorator(updateCatCommand).Execute();
                            break;

                        case "6":
                            Console.Write("Введите ID операции для редактирования: ");
                            Guid updateOpId = Guid.Parse(Console.ReadLine());
                            Console.Write("Введите новый тип операции (0 - Доход, 1 - Расход): ");
                            int newOpTypeInt = int.Parse(Console.ReadLine());
                            OperationType newOpType = (OperationType)newOpTypeInt;
                            Console.Write("Введите новую сумму операции: ");
                            uint newAmount = uint.Parse(Console.ReadLine());
                            Console.Write("Введите новое описание операции: ");
                            string newDescription = Console.ReadLine();
                            Console.Write("Введите новый ID категории: ");
                            Guid newCatId = Guid.Parse(Console.ReadLine());
                            var updateOpCommand = new UpdateOperationCommand(operationService, updateOpId, newOpType, newAmount, DateTime.Now, newDescription, newCatId);
                            new TimedCommandDecorator(updateOpCommand).Execute();
                            break;

                        case "7":
                            Console.Write("Введите ID счета для удаления: ");
                            Guid deleteAccId = Guid.Parse(Console.ReadLine());
                            var deleteAccCommand = new DeleteAccountCommand(bankAccountService, deleteAccId);
                            new TimedCommandDecorator(deleteAccCommand).Execute();
                            break;

                        case "8":
                            Console.Write("Введите ID категории для удаления: ");
                            Guid deleteCatId = Guid.Parse(Console.ReadLine());
                            var deleteCatCommand = new DeleteCategoryCommand(categoryService, deleteCatId);
                            new TimedCommandDecorator(deleteCatCommand).Execute();
                            break;

                        case "9":
                            Console.Write("Введите ID операции для удаления: ");
                            Guid deleteOpId = Guid.Parse(Console.ReadLine());
                            var deleteOpCommand = new DeleteOperationCommand(operationService, deleteOpId);
                            new TimedCommandDecorator(deleteOpCommand).Execute();
                            break;

                        case "10":
                            Console.Write("Введите начальную дату (например, 2025-03-23): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Введите конечную дату (например, 2025-03-25): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());
                            int netIncome = analyticsService.CalculateNetIncome(startDate, endDate);
                            Console.WriteLine($"Чистый доход за период: {netIncome}");
                            break;

                        case "11":
                            Console.WriteLine("Счета:");
                            foreach (var acc in bankAccountService.GetAllAccounts())
                            {
                                Console.WriteLine($"ID: {acc.Id}, Название: {acc.Name}, Баланс: {acc.Balance}");
                            }
                            break;

                        case "12":
                            Console.WriteLine("Категории:");
                            foreach (var cat in categoryService.GetAllCategories())
                            {
                                Console.WriteLine($"ID: {cat.Id}, Название: {cat.Name}, Тип: {cat.Type}");
                            }
                            break;

                        case "13":
                            Console.WriteLine("Операции:");
                            foreach (var op in operationService.GetAllOperations())
                            {
                                Console.WriteLine($"ID: {op.Id}, Тип: {op.Type}, Сумма: {op.Amount}, Дата: {op.Date}, Категория: {op.CategoryId}");
                            }
                            break;

                        case "0":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
            Console.WriteLine("До свидания!");
        }
    }
}
