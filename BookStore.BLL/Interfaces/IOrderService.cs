﻿using BookStore.BLL.DTO;
using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Interface
{
    public interface IOrderService
    {
       
        IEnumerable<CategoryDTO> GetCategories();


        Task AddToCart(BookDTO cartItem,string Id);
        Task<List<CartItemDTO>> GetAllCartItems(string Id);
        void RemoveFromCart(int id, string CartId);
        Task<decimal> GetTotal(string CartId);
        Task EmptyCart(string CartId);
        Task CreateNewOrder(OrderDTO orderDTO, string CartId);
        Task MigrateCart(string userName, string CartId);
        void Dispose();
    }
}
