//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
//using University.User.Application.IServices;
//using University.User.Model.IRepository;

//namespace University.User.ApplicationService.Services
//{
//    public class CacheInitializationService : IHostedService
//    {
//        private readonly IRedisService _redisService;
//        private readonly IUserRepository _userRepository;

//        public CacheInitializationService(IRedisService redisService, IUserRepository userRepository)
//        {
//            _redisService = redisService;
//            _userRepository = userRepository;
//        }

//        public async Task StartAsync(CancellationToken cancellationToken)
//        {
//            Console.WriteLine("Starting cache initialization...");

//            // Fetch all users from the database
//            var allUsers = await _userRepository.GetAllUsers();

//            if (allUsers != null)
//            {
//                // Cache the users in Redis with a key
//                await _redisService.SetAsync("users:all", allUsers, TimeSpan.FromHours(1));
//                Console.WriteLine("Cache for all users initialized successfully.");
//            }
//            else
//            {
//                Console.WriteLine("No users found to cache.");
//            }
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            Console.WriteLine("Stopping cache initialization service...");
//            return Task.CompletedTask;
//        }
//    }
//}
