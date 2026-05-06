using StudentSimulator.Data.PayloadData;
using StudentSimulator.Works.Barista;
using StudentSimulator.Works.Loader;
using StudentSimulator.Works.FoodCourier;
using StudentSimulator.Works.Consultant;
using StudentSimulator.Works.ConstructionAssistent;
using StudentSimulator.Works;
using StudentSimulator.Works.Repo;
using StudentSimulator.Domain.Core.User;

namespace StudentSimulator.Works
{
    public class JobManager
    {
        private readonly JobsRepository _repository;
        private readonly JobsEngine _engine;
        private readonly User _user;

        public JobManager(JobsRepository repository, JobsEngine engine, User user)
        {
            _repository = repository;
            _engine = engine;
            _user = user;
        }

        public void StartBaristaJob()
        {
            Order baristaOrder = _repository.GetRandomTask<Order>("Barista");

            BaristaSession session = _repository.PrepareBaristaSession(baristaOrder.Id);

            Console.WriteLine($"\n--- ПІДРОБІТКА: БАРИСТА ---");
            Console.WriteLine($"Замовлення №{baristaOrder.Id}, Клієнтів: {baristaOrder.Clients}");

            Reward result = _engine.RunJob<Order, BaristaSession>(
                session,
                baristaOrder,
                BaristaGame.Play,
                BaristaRewardCalculator.Calculate 
            );

            ApplyRewardToPlayer(result);
        }

        public void StartLoaderJob()
        {
            LoadTask loaderTask = _repository.GetRandomTask<LoadTask>("Loader");

            LoaderSession session = new LoaderSession
            {
                ClientName = loaderTask.ClientName,
                Items = _repository.GetItemsForLoading(loaderTask.Id),
                MaxImbalance = 15.0 
            };

            Console.WriteLine($"\n--- ПІДРОБІТКА: ВАНТАЖНИК ---");
            Console.WriteLine($"Клієнт: {loaderTask.ClientName}, Загальна вага: {loaderTask.TotalWeight}кг");

            Reward result = _engine.RunJob<LoadTask, LoaderSession>(
                session,
                loaderTask,
                LoaderGame.Play, 
                LoaderRewardCalculator.Calculate
            );

            ApplyRewardToPlayer(result);
        }

        public void StartFoodCourierJob()
        {
            
            DeliveryOrder order = _repository.GetRandomTask<DeliveryOrder>("FoodCourier");

            
            CourierSession session = _repository.PrepareCourierSession(order);

            Console.WriteLine($"\n--- ПІДРОБІТКА: КУР'ЄР ---");
            Console.WriteLine($"Клієнт: {order.ClientName}, Дистанція: {order.Distance}м");

            
            Reward result = _engine.RunJob<DeliveryOrder, CourierSession>(
                session,
                order,
                FoodCourierGame.Play,
                CourierRewardCalculator.Calculate
            );

            
            ApplyRewardToPlayer(result);
        }

        public void StartConsultantJob()
        {
            ConsultingSession sessionData = _repository.GetRandomTask<ConsultingSession>("Consultant");
            
            string problem = _repository.GetRawProperty("Consultant", sessionData.Id, "Problem");
            string correctAnswer = _repository.GetRawProperty("Consultant", sessionData.Id, "CorrectAnswer");

            ConsultantSession gameSession = _repository.PrepareConsultingSession(sessionData, problem, correctAnswer);
            
            Reward result = _engine.RunJob<ConsultingSession, ConsultantSession>(
                gameSession,
                sessionData,
                ConsultantGame.Play,
                ConsultantRewardCalculator.Calculate
            );

            ApplyRewardToPlayer(result);
        }

        public void StartConstructionJob()
        {
            var taskData = _repository.GetRandomTask<ConstructionTask>("ConstructionAssistant");

            var session = new ConstructionSession
            {
                TaskName = taskData.Name,
                NailsToHit = taskData.Duration * 2,
                SpeedMs = taskData.DifficultyLevel
            };

            Reward result = _engine.RunJob<ConstructionTask, ConstructionSession>(
                session,
                taskData,
                ConstructionAssistantGame.Play,
                ConstructionAssistentRewardCalculator.Calculate
            );

            ApplyRewardToPlayer(result);
        }


        private void ApplyRewardToPlayer(Reward reward)
        {
            if (_user.Stamina.Value < reward.Stamina)
            {
                Console.WriteLine("Ви занадто виснажені!");
                _user.Mood.DecreaseValue(20); 
            }

            if (reward.Stamina > 0)
            {
                _user.Stamina.DecreaseValue(reward.Stamina);
            }

            if (reward.Mood > 0)
            {
                _user.Mood.DecreaseValue(reward.Mood);
            }
            else
            {
                _user.Mood.IncreaseValue(Math.Abs(reward.Mood));
            }

            _user.Money.IncreaseValue(reward.Money);

            Console.WriteLine("\n--- ХАРАКТЕРИСТИКИ ОНОВЛЕНО ---");
            Console.WriteLine($"Гроші: {_user.Money.Value} | Стаміна: {_user.Stamina.Value} | Настрій: {_user.Mood.Value}");
        }
    }
}