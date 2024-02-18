using MercatorBankAPIProj.Data;
using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Models.Enums;
using MercatorBankAPIProj.Models.Generics;
using MercatorBankAPIProj.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace MercatorBankAPIProj.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly MyDbContext _db;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(MyDbContext db, ITransactionRepository transactionRepository)
        {
            _db = db;
            _transactionRepository = transactionRepository;
        }


        public async Task<Result<AddTransactionResponseDTO>> AddTransaction(AddTransactionRequestDTO requestDTO, string cardId)
        {
            var result = new Result<AddTransactionResponseDTO>();

            try
            {
                var card = _db.Cards.Include(c => c.MerchantUser).FirstOrDefault(c => c.Id == cardId);
                if (card == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Card not found";
                    return result;
                }

                // Determine the transaction status
                var transactionStatus = TranStatus.Failed; // Default to Failed
                if (_db.Transactions.Any(t => t.CardId == cardId))
                {
                    transactionStatus = TranStatus.Approved; // Set to Approved if transaction exists in the database
                }

                // Generate TerminalId
                var terminalId = "#" + Guid.NewGuid().ToString().Substring(0, 9);

                // Generate Reference Number
                var referenceNumber = "Ref" + new Random().Next(100000, 999999).ToString();

                // Create a new transaction based on requestDTO
                var newTransaction = new MercatorBankAPIProj.Models.Entities.Transaction
                {
                    CardId = card.Id,
                    MerchantUserId = card.MerchantUserId, // Assuming MerchantUserId is the merchant's user ID
                    TerminalId = terminalId,
                    Card = card,
                    Amount = requestDTO.Amount,
                    RefNumber = referenceNumber,
                    Type = requestDTO.Type,
                    Status = transactionStatus, // Set the Status based on the determined transaction status
                    CreatedAt = DateTime.Now // Set CreatedAt to the current DateTime
                };

                await _transactionRepository.AddTransactionAsync(newTransaction);

                var transactionResponse = new AddTransactionResponseDTO
                {
                    TerminalId = newTransaction.TerminalId,
                    CardNumber = requestDTO.CardNumber, // Assuming requestDTO.CardNumber is provided
                    Amount = (decimal)newTransaction.Amount,
                    RefNumber = newTransaction.RefNumber,
                    CreatedAt = (DateTime)newTransaction.CreatedAt,
                    MerchantInfo = card.MerchantUser?.AccountName, // Set MerchantInfo to the current user's AccountName
                    Status = (TranStatus)newTransaction.Status
                };

                result.IsSuccess = true;
                result.Message = "Transaction added successfully";
                result.Content = transactionResponse;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }



        public async Task<Result<List<GetTransactionsDTO>>> GetAllTransactions(string cardId)
        {
            var result = new Result<List<GetTransactionsDTO>>();
            try
            {
                var card = _db.Cards.Include(c => c.MerchantUser).FirstOrDefault(c => c.Id == cardId);
                if (card == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Card not found";
                    return result;
                }

                // Query transactions associated with the provided cardId
                var transactions = await _transactionRepository.GetTransactionsByCardId(cardId);

                if (transactions == null || !transactions.Any())
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Transactions not found";
                    return result;
                }

                var getTransactions = new List<GetTransactionsDTO>();

                foreach (var transaction in transactions)
                {
                    var transactionDTO = new GetTransactionsDTO
                    {
                        Id = transaction.Id,
                        ReferenceNumber = transaction.RefNumber,
                        Amount = (decimal)transaction.Amount,
                        CardNumber = transaction.Card.CardNumber,
                        AccountNumber = transaction.Card.AccountNumber,
                        Type = (TranType)transaction.Type,
                        CreatedAt = transaction.CreatedAt,
                    };

                    getTransactions.Add(transactionDTO);
                }

                result.IsSuccess = true;
                result.Message = "Transactions retrieved successfully";
                result.Content = getTransactions;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
