using MercatorBankAPIProj.Data;
using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Models.Enums;
using MercatorBankAPIProj.Models.Generics;
using MercatorBankAPIProj.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MercatorBankAPIProj.Services.Implementation
{
    public class CardService : ICardService
    {
        private readonly MyDbContext _db;
        private readonly ICardRepository _cardRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CardService(MyDbContext db, ICardRepository cardRepository, ITransactionRepository transactionRepository)
        {
            _db = db;
            _cardRepository = cardRepository;
            _transactionRepository = transactionRepository;

        }


        public async Task<Result<AddCardResponseDTO>> AddCard(AddCardRequestDTO requestDTO, string userId)
        {
            var result = new Result<AddCardResponseDTO>();            

            try
            {
                var user = _db.MerchantUsers.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                var cardDb = _db.Cards.FirstOrDefault();

                // Generate a unique CardNumber
                var cardNumber = GenerateCardNumber();

                // Generate a unique AccountNumber
                var accountNumber = GenerateAccountNumber();

                // Set initial balance
                decimal initialBalance = 0;
                
                // Create a new card based on requestDTO
                var newCard = new Card
                {
                    MerchantUserId = user.Id,
                    Name = requestDTO.Name,
                    Quantity = requestDTO.Quantity,
                    CardCurrency = requestDTO.CardCurrency,
                    CardType = requestDTO.CardType,
                    CardScheme = requestDTO.CardScheme,
                    Expiry = DateTime.Now.AddYears(2), // Calculate expiry date as 2 years from the registration date
                    CardNumber = cardNumber,
                    AccountNumber = accountNumber,
                    Balance = initialBalance,
                };

                await _cardRepository.AddCardAsync(newCard);
                    
                var cardResponse = new AddCardResponseDTO
                {
                    AddedBy = user.AccountName,
                    Quantity = (int)newCard.Quantity,
                    Id = newCard.Id,
                    Name = newCard.Name,
                    CardCurrency = (CardCurrency)newCard.CardCurrency,
                    CardType = (CardType)newCard.CardType,
                    CardScheme = (CardScheme)newCard.CardScheme,
                    Expiry = (DateTime)newCard.Expiry
                };

                result.IsSuccess = true;
                result.Message = "Card added successfully";
                result.Content = cardResponse;


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }



        public async Task<Result<List<GetCardDTO>>> GetAllCards(string userId)
        {
            var result = new Result<List<GetCardDTO>>();
            try
            {
                var user = _db.MerchantUsers.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                var cards = await _cardRepository.GetCardsByMerchantUser(user.Id);

                if (cards.Count() == 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Cards not found";
                    return result;
                }

                var getCards = new List<GetCardDTO>();

                foreach (var card in cards)
                {
                    
                    var cardDTO = new GetCardDTO
                    {
                        Id = card.Id,
                        Name = card.Name,
                        Balance = (decimal)card.Balance,
                        CardNumber = card.CardNumber,
                        AccountNumber = card.AccountNumber,
                        Expiry = (DateTime)card.Expiry
                    };

                    getCards.Add(cardDTO);
                }

                result.IsSuccess = true;
                result.Message = "Cards retrieved successfully";
                result.Content = getCards;


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result<GetCardTransactionDTO>> GetCardByTransaction(string cardId, string userId)
        {
            var result = new Result<GetCardTransactionDTO>();
            try
            {
                var user = _db.MerchantUsers.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                var card = await _cardRepository.GetCardByTransaction(cardId, user.Id);
                var transaction = await _transactionRepository.GetById(cardId);

                if (card == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Card not found";
                    return result;
                }
              
                var getCard = new GetCardTransactionDTO
                {
                    Id = card.Id,
                    Name = card.Name,                    
                    Balance = (decimal)card.Balance,
                    CardNumber = card.CardNumber,
                    Expiry = (DateTime)card.Expiry,
                    CreatedAt = card.CreatedAt,
                    Recipient = transaction.Recipient,
                    Amount = (decimal)transaction.Amount,
                    Type = (TranType)transaction.Type,
                    Location = transaction.Location,
                    RequestStatus = (RequestStatus)transaction.RequestStatus,

                };

                result.IsSuccess = true;
                result.Message = "Card retrieved successfully";
                result.Content = getCard;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result<List<ReqMagCardDTO>>> GetCardsByRequest(string userId)
        {
            var result = new Result<List<ReqMagCardDTO>>();
            try
            {
                var user = _db.MerchantUsers.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                var cards = await _cardRepository.GetCardsByMerchantUser(user.Id);

                if (cards.Count() == 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Cards not found";
                    return result;
                }

                var getCards = new List<ReqMagCardDTO>();

                foreach (var card in cards)
                {
                    
                    var cardDTO = new ReqMagCardDTO
                    {
                        BatchNumber = card.Id,                        
                        Quantity = (int)card.Quantity,
                        CardType = (CardType)card.CardType,
                        CardScheme = (CardScheme)card.CardScheme,
                        Currency = (CardCurrency)card.CardCurrency,
                        Status = (CardStatus)card.Status,
                        CreatedAt = card.CreatedAt,
                        DeliveredAt = card.DeliveredAt
                    };

                    getCards.Add(cardDTO);
                }

                result.IsSuccess = true;
                result.Message = "Cards retrieved successfully";
                result.Content = getCards;


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        public async Task<Result<object>> DeleteCard(string cardId, string userId)
        {
            var result = new Result<object>();
            try
            {
                var user = _db.MerchantUsers.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                var card = _db.Cards.FirstOrDefault(u => u.Id == cardId);
                if (card == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Card not found";
                    return result;
                }

                if (card.MerchantUserId != user.Id)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "You cannot delete this card";
                    return result;
                }

                await _cardRepository.Delete(card);
                card.DeletedAt = DateTime.Now;

                result.IsSuccess = true;
                result.Message = "Card deleted successfully";
                result.Content = new { DeletedAt = card.DeletedAt };


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result<UpdateCardDTO>> UpdateCard(string cardId, UpdateCardDTO requestDTO, string userId)
        {
            var result = new Result<UpdateCardDTO>();
            try
            {
                var user = _db.MerchantUsers.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                var card = _db.Cards.FirstOrDefault(u => u.Id == cardId);

                if (card == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Card not found";
                    return result;
                }

                card.Quantity = requestDTO.Quantity != null ? requestDTO.Quantity : card.Quantity;
                card.CardType = requestDTO.CardType != null ? requestDTO.CardType : card.CardType;
                card.CardScheme = requestDTO.CardScheme != null ? requestDTO.CardScheme : card.CardScheme;
                card.CardCurrency = requestDTO.CardCurrency != null ? requestDTO.CardCurrency : card.CardCurrency;
                card.Name = requestDTO.Name ?? card.Name;

                var updateCard = await _cardRepository.UpdateCard(card);

                var updateDTO = new UpdateCardDTO
                {
                    Name = updateCard.Name,
                    Quantity = (int)updateCard.Quantity,
                    CardType = (CardType)updateCard.CardType,
                    CardCurrency = (CardCurrency)updateCard.CardCurrency,
                    CardScheme = (CardScheme)updateCard.CardScheme,

                };

                result.IsSuccess = true;
                result.Message = "Card updated successfully";
                result.Content = updateDTO;


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }










        private string GenerateCardNumber()
        {
            var random = new Random();
            var bytes = new byte[8];
            random.NextBytes(bytes);

            var cardNumber = BitConverter.ToInt64(bytes, 0);
            cardNumber = Math.Abs(cardNumber % 90000000000000) + 10000000000000;

            return cardNumber.ToString();
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            var bytes = new byte[5];
            random.NextBytes(bytes);

            var accountNumber = BitConverter.ToInt32(bytes, 0);
            accountNumber = Math.Abs(accountNumber % 900000000) + 100000000;

            return accountNumber.ToString();
        }
    }
}
