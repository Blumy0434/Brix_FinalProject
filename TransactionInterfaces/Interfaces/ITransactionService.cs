﻿using System;
using System.Threading.Tasks;
using Transaction.Share.Models;

namespace Transaction.Share.Interfaces
{
    public interface ITransactionService
    {
        Task<Guid> CreateTransactionAsync(TransactionModel transactionModel);
    }
}
