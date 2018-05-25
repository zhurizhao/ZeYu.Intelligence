
using System.Threading;

namespace ZeYu.Intelligence.Monitor
{
    class Program
    {
        static System.Threading.Timer timer;//必须是全局变量，否则将只会执行一次
        static void Main(string[] args)
        {
            Init();

            //分配更新队列信息到数据库线程
            for (int i = 0; i < 5; i++)
            {
                ThreadStart tsCalculate = new ThreadStart(ZeYu.Intelligence.Monitor.Business.ActiveInfo.Calculate);
                Thread threadCalculate = new Thread(tsCalculate);
                threadCalculate.Start();
            }

            //分配更新队列信息到数据库线程
            //先采用单线程,否则在事件更新到数据库时会出现一个事件多行的情况
            for (int i = 0; i < 1; i++)
            {
                ThreadStart tsAnalyze = new ThreadStart(ZeYu.Intelligence.Monitor.Business.EBikeAction.Analyze);
                Thread threadAnalyze = new Thread(tsAnalyze);
                threadAnalyze.Start();
            }

            //写入数据库
            for (int i = 0; i < 3; i++)
            {
                ThreadStart tsImportDB = new ThreadStart(ZeYu.Intelligence.Monitor.Business.EbikeOperatingInfo.ImportToDB);
                Thread threadImportDB = new Thread(tsImportDB);
                threadImportDB.Start();
            }


            //启动定时分析离线计时器
            timer = new System.Threading.Timer(
                    new System.Threading.TimerCallback(Business.ActiveInfo.AnalyzeOffLine), null, 0, 310000
                );

        }


        static void Init()
        {
            Config.RabbitMQConnectString = COCOPASS.Helper.NetCoreHelper.ConfigurationManager.GetConnectString("RabbitMQConnection");
        }
        
    }
}
