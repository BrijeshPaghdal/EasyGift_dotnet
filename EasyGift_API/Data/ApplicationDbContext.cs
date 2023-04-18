using EasyGift_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyGift_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<BankDetails> BankDetails { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerLogin> CustomerLogin { get; set; }
        public DbSet<ForgotPassword> ForgotPassword { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderComplete> OrderComplete { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductSuggestion> ProductSuggestion { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SellerBankDetails> SellerBankDetails { get; set; }
        public DbSet<SellerForgotPassword> SellerForgotPassword { get; set; }
        public DbSet<SellerLogin> SellerLogin { get; set; }
        public DbSet<SellerOnline> SellerOnline { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Suggestion> Suggestion { get; set; }
        public DbSet<UserOnline> UserOnline { get; set; }
    }
}
