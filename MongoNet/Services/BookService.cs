using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoNet.Config;
using MongoNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoNet.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;        

        public BookService(IOptions<MongoConfig> mongoConfigOptions)
        {            
            MongoConfig mongoConfig = mongoConfigOptions.Value;

            MongoClientSettings mongoSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(mongoConfig.Host, mongoConfig.Port),
                Credential = MongoCredential.CreateCredential(mongoConfig.AuthenticationDatabase, mongoConfig.Username, mongoConfig.Password)
            };

            var client = new MongoClient(mongoSettings);
            var database = client.GetDatabase(mongoConfig.DatabaseName);
            _books = database.GetCollection<Book>("Books");
        }

        public async Task<List<Book>> Get()
        {
            var books = await _books.FindAsync(book => true);

            return await books.ToListAsync();
        }

        public async Task<Book> Get(string id)
        {
            var theBook = await _books.FindAsync<Book>(book => book.Id == id);

            return await theBook.FirstOrDefaultAsync();
        }

        public async Task<Book> Create(Book book)
        {
            await _books.InsertOneAsync(book);

            return book;
        }

        public async Task Update(string id, Book bookIn)
        {
            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);
        }

        public async Task Remove(Book bookIn)
        {
            await _books.DeleteOneAsync(book => book.Id == bookIn.Id);
        }

        public async Task Remove(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }
    }
}
