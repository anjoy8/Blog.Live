using System;
using System.Threading;

namespace DesignPattern
{
     /// <summary>
     /// ����һ��������
     /// </summary>
     public class WeatherForecast
     {
        // ����һ����̬�������������Ψһʵ��
        private static WeatherForecast uniqueInstance;
        // ����һ��������ֹ���߳�
        private static readonly object locker = new object();

        // ����˽�й��캯����ʹ��粻�ܴ�������ʵ��
        private WeatherForecast()
        {
            Thread.Sleep(1000);
        }
        /// <summary>
        /// ��̬������������Ψһʵ��
        /// ������ڣ��򷵻�
        /// </summary>
        /// <returns></returns>
        public static WeatherForecast GetInstance()
        {
            // ������ʵ���������򴴽�������ֱ�ӷ���
            //if (uniqueInstance == null)
            //{
            //    uniqueInstance = new WeatherForecast();
            //}

            // ����һ���߳�ִ�е�ʱ�򣬻��locker���� "����"��
            // �������߳�ִ�е�ʱ�򣬻�ȴ� locker ִ�������

            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // ������ʵ���������򴴽�������ֱ�ӷ���
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new WeatherForecast();
                    }
                }
            }

            return uniqueInstance;
        }
        public DateTime Date { get; set; } = DateTime.Now;

         public int TemperatureC { get; set; }

         public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

         public string Summary { get; set; }
     }
}
